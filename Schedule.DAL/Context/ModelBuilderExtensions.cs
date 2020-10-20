using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Schedule.DAL.Entities;

namespace Schedule.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var student1 = new Student
            {
                Id = 1,
                Name = "V",
                SpecialtyId = 1
            };

            var specialty1 = new Specialty
            {
                Id = 1,
                Name = "CS-1"
            };

            modelBuilder.Entity<Student>().HasData(student1);
            modelBuilder.Entity<Student>().HasData(specialty1);
        }
    }
}