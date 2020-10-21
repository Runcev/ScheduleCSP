using System.Collections;
using System.Collections.Generic;

namespace Schedule.Genetic.Genetic
{
    public class Individual<A>
    {
        public List<A> Representation { get; }
        
        public Individual(List<A> representation)
        {
            Representation = representation;
        }

        public int Length()
        {
            return Representation.Count;
        }
    }
}