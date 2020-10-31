using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Schedule.CSP.CSP
{
    public class CSPHeuristics
    {
        public interface IVariableSelectionStrategy<Var, Val> where Var : Variable
        {
            IEnumerable<Var> Apply(CSP<Var, Val> csp, IEnumerable<Var> vars);
        }
        
        public interface IValueOrderingStrategy<Var, Val> where Var : Variable
        {
            IEnumerable<Val> Apply(CSP<Var, Val> csp, Assignment<Var, Val> assignment, Var var);
        }

        public static IVariableSelectionStrategy<Var, Val> Mrv<Var, Val>() where Var : Variable
        {
            return new MrvHeuristic<Var, Val>();
        }
        
        public static IVariableSelectionStrategy<Var, Val> Deg<Var, Val>() where Var : Variable
        {
            return new DegHeuristic<Var, Val>();
        }
        
        public static IVariableSelectionStrategy<Var, Val> MrvDeg<Var, Val>(CSP<Var,Val> csp, List<Var> vars) where Var : Variable
        {
            return new MrvDegHeuristic<Var, Val>();
        }
        
        public static IValueOrderingStrategy<Var, Val> Lcv<Var, Val>() where Var : Variable
        {
            return new LcvHeuristic<Var, Val>();
        }

        public class MrvDegHeuristic<Var, Val> : MrvHeuristic<Var, Val> where Var : Variable
        {
            private readonly DegHeuristic<Var, Val> _deg = new DegHeuristic<Var, Val>();
            private readonly MrvHeuristic<Var, Val> _mrv = new MrvHeuristic<Var, Val>();
            
            public override IEnumerable<Var> Apply(CSP<Var, Val> csp, IEnumerable<Var> vars)
            {
                return _deg.Apply(csp, _mrv.Apply(csp, vars));
            }
        }
        

        public class MrvHeuristic<Var, Val> : IVariableSelectionStrategy<Var, Val> where Var : Variable
        {
            public virtual IEnumerable<Var> Apply(CSP<Var, Val> csp, IEnumerable<Var> vars)
            {
                var result = new List<Var>();
                
                var minValues = int.MaxValue;
                foreach (var var in vars)
                {
                    var values = csp.GetDomain(var).Count();
                    if (values < minValues)
                    {
                        result.Clear();
                        minValues = values;
                    }

                    if (values == minValues)
                    {
                        result.Add(var);
                    }
                }

                return result;
            }
        }
        
        public class DegHeuristic<Var, Val> : IVariableSelectionStrategy<Var, Val> where Var : Variable
        {
            public IEnumerable<Var> Apply(CSP<Var, Val> csp, IEnumerable<Var> vars)
            {
                var result = new List<Var>();

                var maxDegree = -1;
                foreach (var var in vars)
                {
                    var degree = csp.GetConstraints(var).Count();
                    if (degree > maxDegree)
                    {
                        result.Clear();
                        maxDegree = degree;
                    }

                    if (degree == maxDegree)
                    {
                        result.Add(var);
                    }
                }

                return result;
            }
        }

        public class LcvHeuristic<Var, Val> : IValueOrderingStrategy<Var, Val> where Var : Variable
        {
            public IEnumerable<Val> Apply(CSP<Var, Val> csp, Assignment<Var, Val> assignment, Var var)
            {
                var pairs = new List<KeyValuePair<Val, int>>();
                foreach (Val value in csp.GetDomain(var))
                {
                    var num = CountLostValues(csp, assignment, var, value);
                    pairs.Add(new KeyValuePair<Val, int>(value, num));
                }

                return pairs.OrderBy(p => p.Value).Select(p => p.Key);
            }
            
            private static int CountLostValues(CSP<Var, Val> csp, Assignment<Var, Val> assignment, Var var, Val value) {
                var result = 0;
                var assign = new Assignment<Var,Val>();
                assign.Add(var, value);
                foreach (var constraint in csp.GetConstraints(var))
                {
                    if (constraint.GetScope().Count() == 2) {
                        Var neighbor = csp.GetNeighbor(var, constraint);
                        if (!assignment.Contains(neighbor))
                            foreach (var val in csp.GetDomain(neighbor))
                            {
                                assign.Add(neighbor, val);
                                if (!constraint.IsSatisfiedWith(assign))
                                {
                                    ++result;
                                }
                            }
                    }
                }
                return result;
            }
        }
    }
}