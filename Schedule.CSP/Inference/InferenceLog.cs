using Schedule.CSP.CSP;

namespace Schedule.CSP.Inference
{
    public interface IInferenceLog<Var,Val> where Var : Variable
    {
        bool IsEmpty();
        bool IsConsistencyFound();
        void Undo(CSP<Var, Val> csp);
    }
}