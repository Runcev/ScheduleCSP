using System.Collections.Generic;
using Schedule.DAL.Entities.Common;

namespace Schedule.DAL.Entities
{
    public class Subject : Entity
    {
        public string Name { get; set; }
        
        public List<Class> Classes { get; set; }
        
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}