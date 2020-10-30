using Schedule.CSP.CSP;

namespace Schedule.CSP.Inference
{
    public interface IInferenceLog<Var,Val> where Var : Variable
    {
        bool IsEmpty();
        bool IsConsistencyFound();
        void Undo(CSP<Var, Val> csp);

        static IInferenceLog<Var, Val> EmptyLog<Var, Val>() where Var : Variable
        {
            return new 
            public bool IsEmpty()
            {
                return true;
            }

            public bool IsConsistencyFound()
            {
                return false;
            }   
            
            public void Undo(CSP<Var, Val> csp){ }

        }
    }
}