using System.Collections.Generic;
using System.Linq;
using Schedule.CSP.CSP;
using Schedule.CSP.Schedule.Variable;
using Schedule.DAL.Context;

namespace Schedule.CSP.Schedule.Constraints
{
    public class AuditoriesUnique : IConstraint<InfoVar, (int auditoryId, int dayTimeId)>
    {
        private readonly IEnumerable<InfoVar> _scope;
        private readonly ScheduleContext _context;

        public AuditoriesUnique(InfoVar var1, InfoVar var2, ScheduleContext context)
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
            var value1 = assignment.GetValue(_scope.First());
            var value2 = assignment.GetValue(_scope.Last());

            return value1 == default || value2 == default || value1.dayTimeId != value2.dayTimeId ||
                   value1.auditoryId != value2.auditoryId;
        }
    }
}