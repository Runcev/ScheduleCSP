using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Schedule.CSP.CSP;

namespace Schedule.CSP.Inference
{
    public class DomainLog<Var, Val> : IInferenceLog<Var, Val> where Var : Variable
    {
        private readonly List<KeyValuePair<Var, Domain<Val>>> _savedDomain;
        private HashSet<Var> _affectedVariables;
        private bool _emptyDomainObserved;

        public DomainLog()
        {
            _savedDomain = new List<KeyValuePair<Var, Domain<Val>>>();
            _affectedVariables = new HashSet<Var>();
        }

        public void Clear()
        {
            _savedDomain.Clear();
            _affectedVariables.Clear();
        }

        public void StoreDomainFor(Var var, Domain<Val> domain)
        {
            if (!_affectedVariables.Contains(var))
            {
                _savedDomain.Add(new KeyValuePair<Var, Domain<Val>>(var,domain));
                _affectedVariables.Add(var);
            }
        }

        public void SetEmptyDomainFound(bool b)
        {
            _emptyDomainObserved = b;
        }

        public DomainLog<Var, Val> Compactify()
        {
            _affectedVariables = null;
            return this;
        }

        public bool IsEmpty()
        {
            return !_savedDomain.Any();
        }

        public void Undo(CSP<Var, Val> csp)
        {
            _savedDomain.ForEach(pair => csp.SetDomain(pair.Key, pair.Value));
        }

        public bool IsConsistencyFound()
        {
            return _emptyDomainObserved;
        }
    }
}