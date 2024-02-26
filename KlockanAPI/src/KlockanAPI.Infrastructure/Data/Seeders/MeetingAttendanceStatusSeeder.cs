using Microsoft.EntityFrameworkCore;

using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Data.Seeders;

public static class MeetingAttendanceStatusSeeder
{
    public static ModelBuilder SeedMeetingAttendanceStatus(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MeetingAttendanceStatus>().HasData(
            new MeetingAttendanceStatus {Id= 1 , Name= "Present" },
            new MeetingAttendanceStatus {Id= 2 , Name= "Absent" }
        );

        return modelBuilder;
    }
}
