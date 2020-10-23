using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.DAL.Context;
using Schedule.DAL.Entities;
using Schedule.Genetic.Genetic;
using Schedule.Genetic.Schedule;

namespace Schedule.Blazor.Services
{
    public class ScheduleService
    {
        private static readonly Random Rng = new Random();

        public async Task<IEnumerable<Class>> Classes()
        {
            var alphabet = new List<(int auditoryId, int dayTimeId)>();

            foreach (var dayTime in _context.DayTimes)
            {
                foreach (var auditory in _context.Auditories)
                {
                    alphabet.Add((auditory.Id, dayTime.Id));
                }
            }

            var individualLength = _context.Classes.Count();

            var genetic = new GeneticAlgorithm<(int auditoryId, int dayTimeId)>(individualLength, alphabet, 0.05);
            var fitnessFn = new ScheduleFitnessFn(_context);

            var schedule = genetic.Algorithm(RandomInitialPopulation(100, individualLength), fitnessFn,
                fitnessFn.GoalTest);

            var classes = fitnessFn.Infos.Select(i => i.Class).ToArray();

            for (int i = 0; i < classes.Count(); i++)
            {
                classes[i].AuditoryId = schedule.Representation[i].auditoryId;
                classes[i].DayTimeId = schedule.Representation[i].dayTimeId;
            }

            await _context.SaveChangesAsync();


            return _context.Classes
                .Include(c => c.Auditory)
                .Include(c => c.Group)
                .Include(c => c.Teacher)
                .Include(c => c.DayTime)
                .Include(c => c.Subject)
                .ThenInclude(s => s.Specialty)
                .OrderBy(c => c.DayTime.Day).ThenBy(c => c.DayTime.Number);
        }

        private readonly ScheduleContext _context;

        public ScheduleService(ScheduleContext context)
        {
            _context = context;
        }

        private List<Individual<(int auditoryId, int dayTimeId)>> RandomInitialPopulation(int count,
            int individualLength)
        {
            var auditoryIds = _context.Auditories.Select(a => a.Id).ToArray();
            var dayTimeIds = _context.DayTimes.Select(dt => dt.Id).ToArray();

            (int auditoryId, int dayTimeId) GetRandomPair() => (auditoryIds[Rng.Next(auditoryIds.Length)],
                dayTimeIds[Rng.Next(dayTimeIds.Length)]);

            var population = new List<Individual<(int auditoryId, int dayTimeId)>>();

            for (int i = 0; i < count; i++)
            {
                var individuals = new List<(int auditoryId, int dayTimeId)>();

                for (int j = 0; j < individualLength; j++)
                {
                    individuals.Add(GetRandomPair());
                }

                population.Add(new Individual<(int auditoryId, int dayTimeId)>(individuals));
            }

            return population;
        }
    }
}