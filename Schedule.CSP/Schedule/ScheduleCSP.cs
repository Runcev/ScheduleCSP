using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.CSP.CSP;
using Schedule.CSP.Schedule.Constraints;
using Schedule.CSP.Schedule.Variable;
using Schedule.DAL.Context;
using Schedule.DAL.Enums;

namespace Schedule.CSP.Schedule
{
    public class ScheduleCSP : CSP<InfoVar, (int auditoryId, int dayTimeId)>
    {
        private readonly ScheduleContext _context;

        public ScheduleCSP(IEnumerable<InfoVar> vars, ScheduleContext context) : base(vars)
        {
            _context = context;
            var domain = GenerateDomain();
            foreach (var var in Variables)
            {
                SetDomain(var, domain);
            }

            AddAuditoryCapacityConstraints();
            AddAuditoriesUniqueConstraints();
            AddTeacherUniqueDayTimes();
            AddLectionsPracticesDontIntersect();
        }
        
        private static Random rng = new Random();  

        public static void Shuffle<T>(IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }
        }

        private Domain<(int auditoryId, int dayTimeId)> GenerateDomain()
        {
            var domain = new List<(int auditoryId, int dayTimeId)>();

            foreach (var dayTime in _context.DayTimes)
            {
                foreach (var auditory in _context.Auditories)
                {
                    domain.Add((auditory.Id, dayTime.Id));
                }
            }
            
            Shuffle(domain);

            return new Domain<(int auditoryId, int dayTimeId)>(domain.ToArray());
        }

        private void AddAuditoryCapacityConstraints()
        {
            foreach (var infoVar in Variables)
            {
                AddConstraint(new AuditoryCapacity(infoVar, _context));
            }
        }

        private void AddAuditoriesUniqueConstraints()
        {
            foreach (var infoVar1 in Variables)
            {
                foreach (var infoVar2 in Variables)
                {
                    if (infoVar1 != infoVar2)
                    {
                        AddConstraint(new AuditoriesUnique(infoVar1, infoVar2, _context));
                    }
                }
            }
        }

        private void AddTeacherUniqueDayTimes()
        {
            foreach (var teacherInfos in
                Variables
                    .GroupBy(info => info.Info.Teacher.Id)
                    .Select(g => g.ToArray()))
            {
                foreach (var (first, second) in teacherInfos.Zip(teacherInfos.Skip(1)))
                {
                    AddConstraint(new TeacherUniqueDayTimes(first, second, _context));
                }
            }
        }

        private void AddLectionsPracticesDontIntersect()
        {
            foreach (var classesInfos in 
                Variables
                    .GroupBy(info => info.Info.Class.SubjectId)
                    .Select(g => g.ToArray()))
            {
                foreach (var lection in classesInfos.Where(var => var.Info.Class.Type == ClassType.Lection))
                {
                    foreach (var practice in classesInfos.Where(var => var.Info.Class.Type == ClassType.Practice))
                    {
                        AddConstraint(new LectionsPracticesDontIntersect(lection, practice, _context));
                    }
                }
            }
        }
    }
}