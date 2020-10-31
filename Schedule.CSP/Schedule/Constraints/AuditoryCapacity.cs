using System.Collections.Generic;
using System.Linq;
using Schedule.CSP.CSP;
using Schedule.CSP.Schedule.Variable;
using Schedule.DAL.Context;

namespace Schedule.CSP.Schedule.Constraints
{
    public class AuditoryCapacity : IConstraint<InfoVar, (int auditoryId, int dayTimeId)>
    {
        private readonly InfoVar _var;
        private readonly ScheduleContext _context;

        public AuditoryCapacity(InfoVar var, ScheduleContext context)
        {
            _var = var;
            _scope = new[] {var};
            _context = context;
        }


        private readonly IEnumerable<InfoVar> _scope;

        public IEnumerable<InfoVar> GetScope()
        {
            return _scope;
        }

        public bool IsSatisfiedWith(Assignment<InfoVar, (int auditoryId, int dayTimeId)> assignment)
        {
            var (auditoryId, _) = assignment.GetValue(_var);

            return auditoryId == default || _var.Info.Group.Count <= _context.Auditories.Find(auditoryId).Capacity;
        }
    }
}