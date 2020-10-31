using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.CSP.CSP;
using Schedule.CSP.Schedule;
using Schedule.CSP.Schedule.Variable;
using Schedule.DAL.Context;
using Schedule.DAL.Entities;
using Info = Schedule.CSP.Schedule.Variable.Info;

namespace Schedule.Blazor.Services
{
    public class ScheduleService
    {
        private static readonly Random Rng = new Random();

        private InfoVar[] GetInfos()
        {
            return _context.Classes
                .Include(c => c.Group)
                .Include(c => c.Teacher)
                .Include(c => c.Subject)
                .Select(c => new InfoVar(new Info(c.Teacher, c.Group, c))).ToArray();
        }
        
        public async Task<IEnumerable<Class>> Classes()
        {
            var csp = new ScheduleCSP(GetInfos(), _context);

            var scheduleAssignment = new BacktrackingSolver<InfoVar, (int auditoryId, int dayTimeId)>().Solve(csp);

            var result = scheduleAssignment.GetVariableAndValues().ToArray();
            
            var classes = result.Select(c => c.variable.Info.Class).ToArray();

            for (int i = 0; i < classes.Count(); i++)
            {
                classes[i].AuditoryId = result[i].value.auditoryId;
                classes[i].DayTimeId = result[i].value.dayTimeId;
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
    }
}