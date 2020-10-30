using System;
using System.Collections.Generic;

namespace Schedule.CSP.CSP
{
    public class Assignment<Var, Val> where Var : Variable
    {
        private readonly Dictionary<Var, Val> _variableToValueMap = new Dictionary<Var, Val>();

        public IEnumerable<Var> GetVariables()
        {
            throw new NotImplementedException();
        }
    }
}