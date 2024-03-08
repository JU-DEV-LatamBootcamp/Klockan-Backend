using KlockanAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infrastructure.Data.Seeders
{
    internal static class ScheduleSeeder
    {
        internal static ModelBuilder SeedSchedules(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>().HasData(
            new Schedule
            {
                Id = 1,
                ClassroomId = 1,
                WeekdayId = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                StartTime = new TimeOnly(15, 30, 0),
            },
            new Schedule
            {
                Id = 2,
                ClassroomId = 1,
                WeekdayId = 3,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                StartTime = new TimeOnly(15, 30, 0),
            },
            new Schedule
            {
                Id = 3,
                ClassroomId = 1,
                WeekdayId = 5,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                StartTime = new TimeOnly(15, 30, 0),
            },
            new Schedule
            {
                Id = 4,
                ClassroomId = 2,
                WeekdayId = 7,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                StartTime = new TimeOnly(15, 30, 0),
            });
            return modelBuilder;
        }
    }
}
