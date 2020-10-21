using System.Collections.Generic;
using Schedule.DAL.Entities.Common;

namespace Schedule.DAL.Entities
{
    public class Specialty : Entity
    {
        public string Name { get; set; }
        public int StudentCount { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}