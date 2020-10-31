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
        
        public static bool operator ==(Variable var1, Variable var2)
        {
            return var1?.Name == var2?.Name;
        }
        
        public static bool operator !=(Variable var1, Variable var2)
        {
            return !(var1 == var2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
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