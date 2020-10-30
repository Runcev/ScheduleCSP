using System.Collections.Generic;
using Schedule.CSP.CSP;

namespace ConsoleApp1
{
    public class MapCSP : CSP<Variable, string>
    {
        public static readonly Variable NSW = new Variable("NSW");
        public static readonly Variable NT = new Variable("NT");
        public static readonly Variable Q = new Variable("Q");
        public static readonly Variable SA = new Variable("SA");
        public static readonly Variable T = new Variable("T");
        public static readonly Variable V = new Variable("V");
        public static readonly Variable WA = new Variable("WA");

        public static readonly string RED = "RED";
        public static readonly string GREEN = "GREEN";
        public static readonly string BLUE = "BLUE";

        /**
	 * Constructs a map CSP for the principal states and territories of
	 * Australia, with the colors Red, Green, and Blue.
	 */
        public MapCSP() : base(new[] {NSW, WA, NT, Q, SA, V, T})
        {
            var colors = new Domain<string>(RED, GREEN, BLUE);
            foreach (var var in Variables)
            {
                SetDomain(var, colors);
            }

            AddConstraint(new NotEqualConstraint<Variable, string>(WA, NT));
            AddConstraint(new NotEqualConstraint<Variable, string>(WA, SA));
            AddConstraint(new NotEqualConstraint<Variable, string>(NT, SA));
            AddConstraint(new NotEqualConstraint<Variable, string>(NT, Q));
            AddConstraint(new NotEqualConstraint<Variable, string>(SA, Q));
            AddConstraint(new NotEqualConstraint<Variable, string>(SA, NSW));
            AddConstraint(new NotEqualConstraint<Variable, string>(SA, V));
            AddConstraint(new NotEqualConstraint<Variable, string>(Q, NSW));
            AddConstraint(new NotEqualConstraint<Variable, string>(NSW, V));
        }
    }

    public class NotEqualConstraint<VAR, VAL> : IConstraint<VAR, VAL> where VAR : Variable
    {
        private VAR var1;
        private VAR var2;
        private List<VAR> scope;

        public NotEqualConstraint(VAR var1, VAR var2)
        {
            this.var1 = var1;
            this.var2 = var2;
            scope = new List<VAR>(2);
            scope.Add(var1);
            scope.Add(var2);
        }

        public IEnumerable<VAR> GetScope()
        {
            return scope;
        }

        public bool IsSatisfiedWith(Assignment<VAR, VAL> assignment)
        {
            VAL value1 = assignment.GetValue(var1);
            return value1 == null || !value1.Equals(assignment.GetValue(var2));
        }
    }
}