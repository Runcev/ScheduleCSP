using Schedule.DAL.Entities.Common;

namespace Schedule.DAL.Entities
{
    public class Group : Entity
    {
        public int Number { get; set; }
        public int Count { get; set; }
        
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}