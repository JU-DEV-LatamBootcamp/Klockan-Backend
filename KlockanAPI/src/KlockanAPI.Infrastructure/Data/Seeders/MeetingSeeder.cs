using KlockanAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KlockanAPI.Infrastructure.Data.Seeders
{
    internal static class MeetingSeeder
    {
        internal static ModelBuilder SeedMeetings(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>().HasData(
                new Meeting
                {
                    Id = 1,
                    SessionNumber = 3,
                    ClassroomId = 1,
                    Date = new DateOnly(2024, 1, 23),
                    Time = new TimeOnly(15, 30, 0),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),

                },
                new Meeting
                {
                    Id = 2,
                    SessionNumber = 3,
                    ClassroomId = 1,
                    Date = new DateOnly(2024, 1, 23),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Time = new TimeOnly(15, 30, 0),
                },
                new Meeting
                {
                    Id = 3,
                    SessionNumber = 3,
                    ClassroomId = 1,
                    Date = new DateOnly(2024, 1, 23),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Time = new TimeOnly(15, 30, 0),

                },
                new Meeting
                {
                    Id = 4,
                    SessionNumber = 3,
                    ClassroomId = 1,
                    Date = new DateOnly(2024, 1, 23),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Time = new TimeOnly(15, 30, 0),
                }
            );
            return modelBuilder;
        }
    }
}
