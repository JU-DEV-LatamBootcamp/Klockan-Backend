using KlockanAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockanAPI.Infrastructure.Data.Seeders
{
    internal static class WeekdaySeeder
    {
        internal static ModelBuilder SeedWeekdays(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weekday>().HasData(
            new Weekday
                {
                    Id = 1,
                    Name = "Sunday"
                },
                new Weekday
                {
                    Id = 2,
                    Name = "Monday"
                },
                new Weekday
                {
                    Id = 3,
                    Name = "Tuesday"
                },
                new Weekday
                {
                    Id = 4,
                    Name = "Wednesday"
                },
                new Weekday
                {
                    Id = 5,
                    Name = "Thursday"
                },
                new Weekday
                {
                    Id = 6,
                    Name = "Friday"
                },
                new Weekday
                {
                    Id = 7,
                    Name = "Saturday"
                }
            );
            return modelBuilder;
        }
    }
}
