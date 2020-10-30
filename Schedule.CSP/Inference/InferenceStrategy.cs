using Schedule.CSP.CSP;

namespace Schedule.CSP.Inference
{
    public interface IInferenceStrategy<Var,Val> where Var : Variable
    {
        IInferenceLog<Var, Val> Apply(CSP<Var, Val> csp);

        IInferenceLog<Var, Val> Apply(CSP<Var, Val> csp, Assignment<Var, Val> assignment, Var var);
    }
}