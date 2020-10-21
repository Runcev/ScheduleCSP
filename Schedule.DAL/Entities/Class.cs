using System.Collections.Generic;
using System.Linq;
using Schedule.DAL.Entities.Common;
using Schedule.DAL.Enums;

namespace Schedule.DAL.Entities
{
    public class Class : Entity
    {
        public Class()
        {
            
        }
        
        public ClassType Type { get; set; }
        
        public int DayTimeId { get; set; }
        public DayTime DayTime { get; set; }
        
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        
        public int AuditoryId { get; set; }
        public Auditory Auditory { get; set; }  
        
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        private List<Group> _groups = new List<Group>();
        
        public List<Group> Groups
        {
            get => _groups;
            set
            {
                var groups = value;
                
                int studentCount = Subject.Specialty.StudentCount;
                int studentsInOneGroup = studentCount / groups.Count;
                int studentsRemains = studentCount % groups.Count;

                foreach (var group in groups)
                {
                    group.Count = studentsInOneGroup;
                }

                groups.First().Count += studentsRemains;

                _groups = groups;
            }
        }
    }
}