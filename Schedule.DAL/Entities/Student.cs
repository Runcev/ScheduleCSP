using Schedule.DAL.Entities.Common;

namespace Schedule.DAL.Entities
{
    public class Student : Entity
    {
        public string Name { get; set; }
        
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}