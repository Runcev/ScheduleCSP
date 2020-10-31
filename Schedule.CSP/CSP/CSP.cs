using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Schedule.CSP.CSP
{
    public class CSP<Var, Val> where Var : Variable
    {
        public HashSet<Var> Variables { get; } = new HashSet<Var>();
        public List<Domain<Val>> Domains { get; } = new List<Domain<Val>>();
        public List<IConstraint<Var, Val>> Constraints { get; } = new List<IConstraint<Var, Val>>();

        private readonly Dictionary<Var, Domain<Val>> _varToDomains = new Dictionary<Var, Domain<Val>>();

        private readonly Dictionary<Var, List<IConstraint<Var, Val>>> _varToConstraints =
            new Dictionary<Var, List<IConstraint<Var, Val>>>();

        public CSP(IEnumerable<Var> vars)
        {
            foreach (Var @var in vars)
            {
                Variables.Add(@var);
                Domains.Add(new Domain<Val>());
                _varToConstraints.TryAdd(@var, new List<IConstraint<Var, Val>>());
            }
        }

        public void SetDomain(Var var, Domain<Val> domain)
        {
            if (var != null)
            {
                _varToDomains.TryAdd(var, domain);
            }
        }

        public Domain<Val> GetDomain(Var var) => _varToDomains[var];

        public bool RemoveValueFromDomain(Var var, Val val)
        {
            var values = GetDomain(var)?.Where(v => !v.Equals(val)).ToArray();
            if (values == null || values.SequenceEqual(GetDomain(var)))
            {
                return false;
            }

            SetDomain(var, new Domain<Val>(values));
            return true;
        }

        public void AddConstraint(IConstraint<Var, Val> constraint)
        {
            Constraints.Add(constraint);
            foreach (Var var in constraint.GetScope())
            {
                if (_varToConstraints.TryGetValue(var, out var constraints))
                {
                    constraints.Add(constraint);
                }
            }
        }

        public IEnumerable<IConstraint<Var, Val>> GetConstraints(Var var)
        {
            if (var != null)
            {
                _varToConstraints.TryGetValue(var, out var constraints);
                return constraints ?? Enumerable.Empty<IConstraint<Var, Val>>();
            }

            return Enumerable.Empty<IConstraint<Var, Val>>();
        }

        public Var GetNeighbor(Var var, IConstraint<Var, Val> constraint)
        {
            var scope = constraint.GetScope().ToArray();

            if (scope.Length == 2)
            {
                if (var == scope.First())
                {
                    return scope.Last();
                }

                if (var == scope.Last())
                {
                    return scope.First();
                }
            }

            return null;
        }
    }
}