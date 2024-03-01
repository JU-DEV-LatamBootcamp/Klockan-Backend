using KlockanAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infrastructure.Data.Seeders
{
    internal static class ProgramSeeder
    {
        public static ModelBuilder SeedPrograms(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Program>().HasData(
                new Program
                {
                    Id = 1,
                    Name = "Bootcamp Developers 01",
                    Description = "Program covering concepts in software development.",
                    CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
                },
                new Program
                {
                    Id = 2,
                    Name = "Advanced Bootcamp Developers 01",
                    Description = "Program focused on advanced software design and development techniques.",
                    CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
                },
                new Program
                {
                    Id = 3,
                    Name = "Bootcamp Data Science and Analytics 01",
                    Description = "Program designed to teach the fundamentals of data analysis, machine learning, and statistical modeling.",
                    CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
                },
                new Program
                {
                    Id = 4,
                    Name = "Bootcamp Developers 02",
                    Description = "Program covering concepts in software development.",
                    CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            return modelBuilder;
        }
    }
}
