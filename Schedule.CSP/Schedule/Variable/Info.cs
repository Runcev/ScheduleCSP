using Schedule.DAL.Entities;

namespace Schedule.CSP.Schedule.Variable
{
    public class Info
    {
        public Teacher Teacher { get; }
        public Group Group { get; }
        public Class Class { get; }

        public Info(Teacher teacher, Group group, Class @class)
        {
            Teacher = teacher;
            Group = group;
            Class = @class;
        }
    }
}