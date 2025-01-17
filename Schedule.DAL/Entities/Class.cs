using Schedule.DAL.Entities.Common;
using Schedule.DAL.Enums;

namespace Schedule.DAL.Entities
{
    public class Class : Entity
    {
        public ClassType Type { get; set; }
        
        public int DayTimeId { get; set; }
        public DayTime DayTime { get; set; }
        
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        
        public int AuditoryId { get; set; }
        public Auditory Auditory { get; set; }  
        
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        
        public Group Group { get; set; }
    }
}