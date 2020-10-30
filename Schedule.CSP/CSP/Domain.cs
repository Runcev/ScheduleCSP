using System.Collections;
using System.Collections.Generic;

namespace Schedule.CSP.CSP
{
    public class Domain<Val> : IEnumerable<Val>
    {
        private readonly IEnumerable<Val> _values;

        public Domain(params Val[] values)
        {
            _values = values;
        }
        
        public IEnumerator<Val> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}