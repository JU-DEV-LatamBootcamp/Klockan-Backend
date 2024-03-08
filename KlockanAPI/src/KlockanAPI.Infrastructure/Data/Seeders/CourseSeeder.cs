using KlockanAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockanAPI.Infrastructure.Data.Seeders
{
    internal static class CourseSeeder
    {
        public static ModelBuilder SeedCourses(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Name = "Frontend Development",
                    Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
                    Sessions = 10,
                    SessionDuration = 60,
                    CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
                },
                new Course
                {
                    Id = 2,
                    Name = "Backend Development",
                    Description = "Course on server side programming, databases, and API construction.",
                    Sessions = 12,
                    SessionDuration = 75,
                    CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
                },
                new Course
                {
                    Id = 3,
                    Name = "Full Stack Development",
                    Description = "Comprehensive course covering both frontend and backend development to build complete applications.",
                    Sessions = 15,
                    SessionDuration = 90,
                    CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
                }
            );
            return modelBuilder;
        }
    }
}
