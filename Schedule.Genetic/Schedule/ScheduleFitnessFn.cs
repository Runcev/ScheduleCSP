using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.DAL.Context;
using Schedule.DAL.Entities;
using Schedule.DAL.Enums;
using Schedule.Genetic.Genetic;

namespace Schedule.Genetic.Schedule
{
    public class ScheduleFitnessFn : IFitnessFn<(int auditoryId, int dayTimeId)>
    {
        private readonly IList<Info> _infos;
        private readonly ScheduleContext _context;

        public ScheduleFitnessFn(IList<Info> info, ScheduleContext context)
        {
            _infos = info;
            _context = context;
        }

        public double Apply(Individual<(int auditoryId, int dayTimeId)> individual)
        {
            return AuditoryCapacityCoef * AuditoryCapacityCheck(individual)
                   + TeacherUniqueDayTimesCoef * TeacherUniqueDayTimesCheck(individual)
                   + AuditoriesUniqueCoef * AuditoriesUniqueCheck(individual)
                   + LectionsPracticesDontIntersectCoef * LectionsPracticesDontIntersectCheck(individual);
        }

        private const double AuditoryCapacityCoef = 0.2;
        private const double TeacherUniqueDayTimesCoef = 0.3;
        private const double AuditoriesUniqueCoef = 0.3;
        private const double LectionsPracticesDontIntersectCoef = 0.2;

        private double AuditoryCapacityCheck(Individual<(int auditoryId, int dayTimeId)> individual)
        {
            var auditoryCapacities = individual.Representation
                .Select(p => p.auditoryId)
                .Select(id => _context.Auditories.Find(id).Capacity).ToArray();

            int validCount = 0;

            for (int i = 0; i < auditoryCapacities.Length; i++)
            {
                if (_infos[i].Group.Count < auditoryCapacities[i])
                {
                    validCount++;
                }
            }

            return Convert.ToDouble(individual.Length()) / validCount;
        }

        private double TeacherUniqueDayTimesCheck(Individual<(int auditoryId, int dayTimeId)> individual)
        {
            var timesTeachers = new HashSet<(int dayTimeId, int teacherId)>();

            var validCount = 0;

            foreach (var dayTimeTeacherPair in individual.Representation
                .Select(p => p.dayTimeId)
                .Zip(_infos.Select(info => info.Teacher.Id)))
            {
                if (timesTeachers.Add(dayTimeTeacherPair))
                {
                    validCount++;
                }
                else
                {
                    validCount--;
                }
            }

            return Convert.ToDouble(individual.Length()) / validCount;
        }

        private double AuditoriesUniqueCheck(Individual<(int auditoryId, int dayTimeId)> individual)
        {
            var timesAuditories = new HashSet<(int auditoryId, int dayTimeId)>();

            var validCount = 0;

            foreach (var auditoryTime in individual.Representation)
            {
                if (timesAuditories.Add(auditoryTime))
                {
                    validCount++;
                }
                else
                {
                    validCount--;
                }
            }

            return Convert.ToDouble(individual.Length()) / validCount;
        }

        private double LectionsPracticesDontIntersectCheck(Individual<(int auditoryId, int dayTimeId)> individual)
        {
            var subjectTimes = new HashSet<(int subjectId, int dayTimeId)>();

            var classes = _infos.Select(i => i.Class).ToArray();

            var validCount = 0;

            for (int i = 0; i < classes.Count(); i++)
            {
                var @class = classes[i];

                if (@class.Type == ClassType.Lection)
                {
                    if (!subjectTimes.Add((@class.SubjectId, individual.Representation[i].dayTimeId)))
                    {
                        validCount++;
                    }
                    else
                    {
                        validCount--;
                    }
                }
                else
                {
                    if (!subjectTimes.Contains((@class.SubjectId, individual.Representation[i].dayTimeId)))
                    {
                        validCount++;
                    }
                    else
                    {
                        validCount++;
                    }
                }
            }

            return Convert.ToDouble(individual.Length()) / validCount;
        }
    }
}