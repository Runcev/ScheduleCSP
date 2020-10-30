using System.Collections.Generic;
using System.Linq;

namespace Schedule.CSP.CSP
{
    public class Variable
    {
        public string Name { get; }

        public Variable(string name)
        {
            Name = name;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            return
                obj != null
                && obj.GetType() == GetType()
                && ((Variable) obj).Name == Name;
        }

        public override int GetHashCode() => Name.GetHashCode();

        public override string ToString() => Name;
    }
}