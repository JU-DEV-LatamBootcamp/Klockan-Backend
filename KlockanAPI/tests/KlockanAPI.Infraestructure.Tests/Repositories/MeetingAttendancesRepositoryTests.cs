using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KlockanAPI.Domain.Models;
using KlockanAPI.Domain.Models.Webex;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infraestructure.Tests.Repositories;

public class MeetingAttendancesRepositoryTests : IDisposable
{
    private readonly KlockanContext _context;
    public MeetingAttendancesRepositoryTests()
    {
        DbContextOptionsBuilder<KlockanContext> dbContextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new KlockanContext(dbContextOptions.Options);
    }
    public void Dispose()
    {
        // Make sure that the in-memory database is deleted at the end of all tests.
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task CreateMeetingAttendance_ShouldCreateMeetingAttendance()
    {
        // Arrange
        Domain.Models.Meeting meeting = new Domain.Models.Meeting
        {
            Id = 1,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30),
            ThirdPartyId = "123asbnonnullvalue"
        };
        MeetingReport meetingReport = new MeetingReport
        {
            items = new List<ParticipantReport>
            {
                new ParticipantReport
                {
                    id = "123asbnonnullvalue_I_285871559454799338_a7f3083c-63f2-31e0-8f25-76a634ce1228",
                    host = false,
                    coHost = false,
                    email = "correo@gmail.com",
                    displayName = "correo@gmail.com",
                    invitee = true,
                    muted = false,
                    state = "end",
                    joinedTime = new DateTime(2024, 02, 19, 10,07,25),
                    leftTime = new DateTime(2024, 02, 19, 10, 40, 00),
                    meetingStartTime = new DateTime(2024, 02, 19, 10 ,06, 20),
                    DurationInMinutes = 32
                }
            }
        };
        MeetingAttendanceStatus meetingAttendanceStatus = new MeetingAttendanceStatus
        {
            Id = 1,
            Name = "Present"
        };
        User user = new User { Id = 6, Email = "correo@gmail.com", RoleId = 3 };
        ClassroomUser classroomUser = new ClassroomUser { Id = 2, ClassroomId=meeting.Id, RoleId = 3, UserId=user.Id };
        Classroom classroom = new Classroom { Id = 1};
        _context.MeetingAttendanceStatuses.Add(meetingAttendanceStatus);
        _context.Classrooms.Add(classroom);
        _context.Users.Add(user);
        _context.Meetings.Add(meeting);
        _context.ClassroomUsers.Add(classroomUser);
        await _context.SaveChangesAsync();

        // Act
        var repository = new MeetingAttendancesRepository(_context);
        var meetReportResult = await repository.CreateMeetingAttendance(meetingReport, meeting.Id);
        
        //Assert
        Assert.NotNull(meetReportResult);
        meetReportResult.Should().BeOfType<MeetingReport>();
        _context.MeetingAttendances.Should().HaveCount(1);        
    }
}
