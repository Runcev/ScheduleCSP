using Schedule.CSP.CSP;

namespace Schedule.CSP.Inference
{
    public class EmptyLog<Var, Val> : IInferenceLog<Var, Val> where Var : Variable
    {
        public bool IsEmpty()
        {
            return true;
        }

        public bool IsConsistencyFound()
        {
            return false;
        }

        public void Undo(CSP<Var, Val> csp)
        {
            
        }
    }
}