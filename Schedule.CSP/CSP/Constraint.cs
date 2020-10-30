using System.Collections.Generic;

namespace Schedule.CSP.CSP
{
    public interface IConstraint<Var, Val> where Var : Variable
    {
        IEnumerable<Var> GetScope();

        bool isSatisfiedWith(Assignment<Var, Val> assignment);
    }
}