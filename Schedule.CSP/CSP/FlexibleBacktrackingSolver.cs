using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Schedule.CSP.Inference;

namespace Schedule.CSP.CSP
{
    public class FlexibleBacktrackingSolver<Var, Val> where Var : Variable
    {
        public CspHeuristics.IVariableSelectionStrategy<Var, Val> VariableSelectionStrategy { get; set; }
        public CspHeuristics.IValueOrderingStrategy<Var, Val> ValueOrderingStrategy { get; set; }
        public IInferenceStrategy<Var, Val> InferenceStrategy { get; set; }

        public Assignment<Var, Val> Solve(CSP<Var, Val> csp)
        {
            if (InferenceStrategy != null)
            {
                var log = InferenceStrategy.Apply(csp);
                if (!log.IsEmpty())
                {
                    if (log.IsConsistencyFound())
                    {
                        return null;
                    }
                }
            }

            return Backtrack(csp, new Assignment<Var, Val>());
        }

        private Assignment<Var, Val> Backtrack(CSP<Var, Val> csp, Assignment<Var, Val> assignment)
        {
            if (assignment.IsComplete(csp.Variables))
            {
                return assignment;
            }
            
            Assignment<Var, Val> result = null;

            var var = SelectUnassignedVariable(csp, assignment);

            foreach (var value in OrderDomainValues(csp, assignment, var))
            {
                assignment.Add(var, value);
                if (assignment.IsConsistent(csp.GetConstraints(var)))
                {
                    var log = Inference(csp, assignment, var);
                    if (!log.IsConsistencyFound())
                    {
                        result = Backtrack(csp, assignment);
                        if (result != null)
                        {
                            break;
                        }
                    }
                    log.Undo(csp);
                }
                assignment.Remove(var);
            }

            return result;
        }

        private Var SelectUnassignedVariable(CSP<Var, Val> csp, Assignment<Var, Val> assigment)
        {
            var vars = csp.Variables.Where(v => !assigment.Contains(v));
            if (VariableSelectionStrategy != null)
            {
                vars = VariableSelectionStrategy.Apply(csp, vars);
            }
            return vars.ElementAt(0);
        }

        private IEnumerable<Val> OrderDomainValues(CSP<Var, Val> csp, Assignment<Var, Val> assignment, Var var)
        {
            return (ValueOrderingStrategy != null)
                ? ValueOrderingStrategy.Apply(csp, assignment, var)
                : csp.GetDomain(var);
        }

        private IInferenceLog<Var, Val> Inference(CSP<Var, Val> csp, Assignment<Var, Val> assignment, Var var)
        {
            return (InferenceStrategy != null)
                ? InferenceStrategy.Apply(csp, assignment, var)
                : new EmptyLog<Var, Val>();
        }
        
    }

}