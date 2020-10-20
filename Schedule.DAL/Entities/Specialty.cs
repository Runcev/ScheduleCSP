using System.Collections.Generic;
using Schedule.DAL.Entities.Common;

namespace Schedule.DAL.Entities
{
    public class Specialty : Entity
    {
        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}