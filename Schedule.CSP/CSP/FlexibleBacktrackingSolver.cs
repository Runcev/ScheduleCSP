using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Schedule.CSP.Inference;

namespace Schedule.CSP.CSP
{
    public class FlexibleBacktrackingSolver<Var, Val> : AbstractBacktrackingSolver<Var, Val> where Var : Variable
    {
        private CspHeuristics.IVariableSelectionStrategy<Var, Val> VariableSelectionStrategy { get; set; }
        private CspHeuristics.IValueOrderingStrategy<Var, Val> ValueOrderingStrategy { get; set; }
        private IInferenceStrategy<Var, Val> InferenceStrategy { get; set; }



        public Assignment<Var, Val> Solve(CSP<Var, Val> csp)
        {
            if (InferenceStrategy != null)
            {
                csp = csp.CopyDomains();
                InferenceLog log = InferenceStrategy.Apply(csp);
                if (!log.IsEmpty())
                {
                    fireStateChanged(csp, null, null);
                    if (log.InConsistencyFound())
                    {
                        return Empty();
                    }
                }
            }

            return Solve(csp);
        }

        public Var SelectUnassignedVariable(CSP<Var, Val> csp, Assignment<Var, Val> assigment)
        {
            IEnumerable<Var> vars = csp.GetVariables.Where(v => !assigment.Contains(v));
            if (VariableSelectionStrategy != null)
            {
                vars = VariableSelectionStrategy.Apply(csp, vars);
            }
            return vars.ElementAt(0);
        }

        protected IEnumerable<Val> OrderDomainValues(CSP<Var, Val> csp, Assignment<Var, Val> assignment, Var var)
        {
            return (ValueOrderingStrategy != null)
                ? ValueOrderingStrategy.Apply(csp, assignment, var)
                : csp.GetDomain(var);
        }

        protected IInferenceLog<Var, Val> Inference(CSP<Var, Val> csp, Assignment<Var, Val> assignment, Var var)
        {
            return (InferenceStrategy != null)
                ? InferenceStrategy.Apply(csp, assignment, var)
                : IInferenceLog.EmptyLog();
        }
        
    }

}