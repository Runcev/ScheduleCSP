using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Schedule.CSP.CSP
{
    public class Assignment<Var, Val> where Var : Variable
    {
        private readonly Dictionary<Var, Val> _variableToValueMap = new Dictionary<Var, Val>();

        public IEnumerable<Var> GetVariables()
        {
            return _variableToValueMap.Keys;
        }

        public Val GetValue(Var var)
        {
            _variableToValueMap.TryGetValue(var, out var val);
            return val;
        }

        public void Add(Var var, Val value)
        {
            if (value != null)
            {
                _variableToValueMap.TryAdd(var, value);
            }
        }

        public void Remove(Var var)
        {
            _variableToValueMap.Remove(var);
        }

        public bool Contains(Var var)
        {
            return _variableToValueMap.ContainsKey(var);
        }

        public bool IsConsistent(IEnumerable<IConstraint<Var, Val>> constraints)
        {
            return constraints.All(c => c.IsSatisfiedWith(this));
        }

        public bool IsComplete(IEnumerable<Var> vars)
        {
            return vars.All(Contains);
        }

        public bool IsSolution(CSP<Var, Val> csp)
        {
            return IsConsistent(csp.Constraints) && IsComplete(csp.Variables);
        }
    }
}