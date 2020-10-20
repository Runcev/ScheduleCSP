using Microsoft.EntityFrameworkCore;
using Schedule.DAL.Entities;

namespace Schedule.DAL.Context
{
    public class ScheduleContext : DbContext
    {
        public DbSet<Auditory> Auditories { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}