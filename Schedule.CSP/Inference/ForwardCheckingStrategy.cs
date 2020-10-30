using System.Data;
using System.Linq;
using Schedule.CSP.CSP;

namespace Schedule.CSP.Inference
{
    public class ForwardCheckingStrategy<Var,Val> : IInferenceStrategy<Var, Val> where Var : Variable
    {
        public IInferenceLog<Var, Val> Apply(CSP<Var, Val> csp)
        {
            return new EmptyLog<Var, Val>();
        }

        public IInferenceLog<Var, Val> Apply(CSP<Var, Val> csp, Assignment<Var, Val> assignment, Var var)
        {
            var log = new DomainLog<Var, Val>();
            foreach (var constraint in csp.GetConstraints(var))
            {
                Var neighbor = csp.GetNeighbor(var, constraint);
                if (neighbor != null && !assignment.Contains(neighbor))
                {
                    if (Revise(neighbor, constraint, assignment, csp, log))
                    {
                        if (!csp.GetDomain(neighbor).Any())
                        {
                            log.SetEmptyDomainFound(true);
                            return log;
                        }
                    }
                }
            }
            return log;
        }


        private bool Revise(Var var, IConstraint<Var, Val> constraint, Assignment<Var, Val> assignment,
            CSP<Var, Val> csp, DomainLog<Var, Val> log)
        {
            bool revised = false;
            foreach (var value in csp.GetDomain(var))
            {
                assignment.Add(var, value);
                if (!constraint.IsSatisfiedWith(assignment))
                {
                    log.StoreDomainFor(var, csp.GetDomain(var));
                    csp.RemoveValueFromDomain(var , value);
                    revised = true;
                }
                assignment.Remove(var);
            }

            return revised;
        }
    }
    
}