using Schedule.CSP.CSP;

namespace Schedule.CSP.Inference
{
    public class ForwardCheckingStrategy<Var,Val> : IInferenceStrategy<Var, Val> 
    {
        public IInferenceLog<Var, Val> Apply(CSP csp)
        {
            return IInferenceLog<Var, Val>.EmptyLog<Var, Val>();
        }

        public IInferenceLog<Var, Val> Apply(CSP<Var, Val> csp, Assignment<Var, Val> assignment, Var var)
        {
            DomainLog
        }
    }
}