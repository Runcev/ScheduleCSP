using System.Collections.Generic;
using Schedule.DAL.Entities.Common;

namespace Schedule.DAL.Entities
{
    public class Auditory : Entity
    {
        public int Number { get; set; }
        public int Capacity { get; set; }
        
        public List<Class> Classes { get; set; }
    }
}