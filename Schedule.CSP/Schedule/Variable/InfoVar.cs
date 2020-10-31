namespace Schedule.CSP.Schedule.Variable
{
    public class InfoVar : CSP.Variable
    {
        public Info Info { get; }
        
        public InfoVar(Info info) : base($"{info.Teacher.Name} {info.Group.Count} {info.Class.Subject.Name}")
        {
            Info = info;
        }
    }
}