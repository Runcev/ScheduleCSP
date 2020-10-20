using Schedule.DAL.Entities.Common;

namespace Schedule.DAL.Entities
{
    public class StudentClass : Entity
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        
        public Class Class { get; set; }
        public Student Student { get; set; }
    }
}