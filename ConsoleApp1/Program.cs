using System;
using System.Linq;
using Schedule.CSP.CSP;
using Schedule.CSP.Inference;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var csp = new MapCSP();

            var solver = new FlexibleBacktrackingSolver<Variable, string>
            {
                InferenceStrategy = new ForwardCheckingStrategy<Variable, string>(),
                ValueOrderingStrategy = new CspHeuristics.LcvHeuristic<Variable, string>(),
                VariableSelectionStrategy = new CspHeuristics.MrvDegHeuristic<Variable, string>()
            };

            var result = solver.Solve(csp);

            Console.WriteLine(string.Join("\n", result.GetVariableAndValues()));
        }
    }
}