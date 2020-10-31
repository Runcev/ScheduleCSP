using System.Collections.Generic;
using Schedule.DAL.Entities.Common;
using Schedule.DAL.Enums;

namespace Schedule.DAL.Entities
{
    public class DayTime : Entity
    {
        public Day Day { get; set; }
        public int Number { get; set; }
        
        public List<Class> Classes { get; set; }
    }
}