using Schedule.CSP.CSP;

namespace Schedule.CSP.Inference
{
    public class InfLog : IInferenceLog<Var, Val> 
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