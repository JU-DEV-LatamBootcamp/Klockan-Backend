using FluentAssertions;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infraestructure.Tests.Repositories;


public class MeetingRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllMeetingsIncludingClassrooms()
    {
        // Arrange
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        // Seed the database
        using (var context = new KlockanContext(options))
        {
            var classrooms = new List<Classroom>
            {
                new Classroom { Id = 1},
                new Classroom { Id = 2}
            };

            var meetings = new List<Meeting>
            {
                new Meeting { Id = 1, Classroom = classrooms[0], Date = new DateOnly(2024, 1, 23), Time = new TimeOnly(15, 30) },
                new Meeting { Id = 2, Classroom = classrooms[1], Date = new DateOnly(2024, 1, 24), Time = new TimeOnly(16, 30) }
            };

            context.Classrooms.AddRange(classrooms);
            context.Meetings.AddRange(meetings);
            await context.SaveChangesAsync();
        }

        using (var context = new KlockanContext(options))
        {
            var repository = new MeetingRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, meeting => Assert.NotNull(meeting.Classroom));
        }
    }

    [Fact]
    public async Task CreateSingleMeeting_ShouldCreateMeeting()
    {
        // Arrange
        var contextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(databaseName: "CreateSingleMeeting_ShouldCreateMeeting_WhenTrainerExists")
            .Options;

        using (var context = new KlockanContext(contextOptions))
        {
            // Add a user trainer to the in-memory database
            var userTrainer = new User { Id = 1, FirstName = "Trainer" };
            await context.Users.AddAsync(userTrainer);
            await context.SaveChangesAsync();
        }

        using (var context = new KlockanContext(contextOptions))
        {
            var meetingRepository = new MeetingRepository(context);
            var meeting = new Meeting
            {
                SessionNumber = 1,
                Date = new DateOnly(2024, 2, 22),
                Time = new TimeOnly(14, 0, 0),
                ClassroomId = 1,
                TrainerId = 1 // Assuming the trainer exists in the database
            };

            // Act
            var createdMeeting = await meetingRepository.CreateSingleMeeting(meeting);

            // Assert
            createdMeeting.Should().NotBeNull();
            context.Meetings.Should().Contain(m => m.Id == createdMeeting.Id);
        }
    }

    [Fact]
    public async Task AssignStudents_ShouldAssignStudentsToMeeting()
    {
        // Arrange
        var contextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(databaseName: "AssignStudents_ShouldAssignStudentsToMeeting")
            .Options;

        using (var context = new KlockanContext(contextOptions))
        {
            var classroomUser1 = new ClassroomUser { Id = 1, UserId = 1, ClassroomId = 1 };
            var classroomUser2 = new ClassroomUser { Id = 2, UserId = 2, ClassroomId = 1 };

            await context.ClassroomUsers.AddRangeAsync(new[] { classroomUser1, classroomUser2 });

            var meeting = new Meeting { Id = 1, ClassroomId = 1 };
            await context.Meetings.AddAsync(meeting);

            await context.SaveChangesAsync();
        }

        // Act
        using (var context = new KlockanContext(contextOptions))
        {
            var meetingRepository = new MeetingRepository(context);

            var meetingAttendance = new List<MeetingAttendance>
        {
            new MeetingAttendance { MeetingId = 1, ClassroomUserId = 1 },
            new MeetingAttendance { MeetingId = 1, ClassroomUserId = 2 }
        };

            await meetingRepository.AssignStudents(meetingAttendance, 1);
        }

        // Assert
        using (var context = new KlockanContext(contextOptions))
        {
            var meetingAttendances = await context.MeetingAttendances.ToListAsync();
            meetingAttendances.Should().HaveCount(2);
        }
    }
}
