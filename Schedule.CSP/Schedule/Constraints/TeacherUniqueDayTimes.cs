using System.Collections.Generic;
using System.Linq;
using Schedule.CSP.CSP;
using Schedule.CSP.Schedule.Variable;
using Schedule.DAL.Context;

namespace Schedule.CSP.Schedule.Constraints
{
    public class TeacherUniqueDayTimes : IConstraint<InfoVar, (int auditoryId, int dayTimeId)>
    {
        private readonly IEnumerable<InfoVar> _scope;
        private readonly ScheduleContext _context;

        public TeacherUniqueDayTimes(InfoVar var1, InfoVar var2, ScheduleContext context)
        {
            _scope = new[] {var1, var2};
            _context = context;
        }

        public IEnumerable<InfoVar> GetScope()
        {
            return _scope;
        }

        public bool IsSatisfiedWith(Assignment<InfoVar, (int auditoryId, int dayTimeId)> assignment)
        {
            var (_, dayTimeId1) = assignment.GetValue(_scope.First());
            var (_, dayTimeId2) = assignment.GetValue(_scope.Last());

            return dayTimeId1 == default || dayTimeId2 == default || dayTimeId1 != dayTimeId2;
        }
    }
}