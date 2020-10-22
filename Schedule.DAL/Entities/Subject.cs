using System.Collections.Generic;
using System.Linq;
using Schedule.DAL.Entities.Common;
using Schedule.DAL.Enums;

namespace Schedule.DAL.Entities
{
    public class Subject : Entity
    {
        public string Name { get; set; }

        public List<Class> Classes { get; set; } = new List<Class>();

        public void AddClass(Class @class)
        {
            Classes.Add(@class);

            var practices = Classes.Where(c => c.Type == ClassType.Practice).ToArray();

            if (@class.Type == ClassType.Practice)
            {
                int studentCount = Specialty.StudentCount;
                int studentsInOneGroup = studentCount / practices.Length;
                int studentsRemains = studentCount % practices.Length;

                foreach (var group in practices.Select(c => c.Group))
                {
                    group.Count = studentsInOneGroup;
                }

                practices[0].Group.Count += studentsRemains;
            }
            else
            {
                @class.Group.Count = Specialty.StudentCount;
            }
        }
        
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}