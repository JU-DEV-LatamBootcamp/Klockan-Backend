using FluentAssertions;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infraestructure.Tests.Repositories;


public class MeetingRepositoryTests : IDisposable
{
    private readonly KlockanContext _context;
    public MeetingRepositoryTests()
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
    public async Task GetAllAsync_ReturnsAllMeetingsIncludingClassrooms()
    {
        // Arrange
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        // Seed the database

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

        _context.Classrooms.AddRange(classrooms);
        _context.Meetings.AddRange(meetings);
        await _context.SaveChangesAsync();

        var repository = new MeetingRepository(_context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, meeting => Assert.NotNull(meeting.Classroom));
    }

    [Fact]
    public async Task GetMeetingsByClassroomIdAsync_ReturnsMeetingsForGivenClassroomId()
    {
        // Arrange
        // Seed the database

        var classrooms = new List<Classroom>
                {
                    new Classroom { Id = 1},
                    new Classroom { Id = 2}
                };

        var meetings = new List<Meeting>
                {
                    new Meeting { Id = 1, ClassroomId = 1, Date = new DateOnly(2024, 1, 23), Time = new TimeOnly(15, 30) },
                    new Meeting { Id = 2, ClassroomId = 1, Date = new DateOnly(2024, 1, 24), Time = new TimeOnly(16, 30) },
                    new Meeting { Id = 3, ClassroomId = 2, Date = new DateOnly(2024, 1, 25), Time = new TimeOnly(10, 0) }
                };

        _context.Classrooms.AddRange(classrooms);
        _context.Meetings.AddRange(meetings);
        await _context.SaveChangesAsync();


        var repository = new MeetingRepository(_context);
        var classroomIdToRetrieve = 1;

        // Act
        var result = await repository.GetMeetingsByClassroomIdAsync(classroomIdToRetrieve);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<Meeting>>();
        result.Should().HaveCount(2);
        result.Should().OnlyContain(meeting => meeting.ClassroomId == classroomIdToRetrieve);
    }

    [Fact]
    public async Task GetMeetingsByClassroomIdAsync_ReturnsNullWhenNoMeetingsFound()
    {
        // Arrange

        var classrooms = new List<Classroom>
        {
            new Classroom { Id = 1},
            new Classroom { Id = 2}
        };

        _context.Classrooms.AddRange(classrooms);
        await _context.SaveChangesAsync();


        var repository = new MeetingRepository(_context);
        var nonExistentClassroomId = 3; // Assuming there are no meetings associated with classroomId 3

        // Act
        var result = await repository.GetMeetingsByClassroomIdAsync(nonExistentClassroomId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateSingleMeeting_ShouldCreateMeeting()
    {
        // Arrange
        var contextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(databaseName: "CreateSingleMeeting_ShouldCreateMeeting")
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
            createdMeeting.Id.Should().NotBe(0); // Assuming Id is auto-generated and not default to 0
            context.Meetings.Should().Contain(m => m.Id == createdMeeting.Id);
        }
    }

    [Fact]
    public async Task GetSessionNumber_ShouldReturnMaxSessionNumber()
    {
        // Arrange
        var classroomId = 1;
        var meetings = new List<Meeting>
    {
        new Meeting { SessionNumber = 1, ClassroomId = classroomId },
        new Meeting { SessionNumber = 2, ClassroomId = classroomId },
        new Meeting { SessionNumber = 3, ClassroomId = classroomId }
    };

        var options = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(databaseName: "GetSessionNumber_ShouldReturnMaxSessionNumber")
            .Options;

        using (var context = new KlockanContext(options))
        {
            context.Meetings.AddRange(meetings);
            await context.SaveChangesAsync();
        }

        int result;

        // Act
        using (var context = new KlockanContext(options))
        {
            var repository = new MeetingRepository(context);
            result = await repository.GetSessionNumber(classroomId);
        }

        // Assert
        result.Should().Be(3);
    }

    [Fact]
    public async Task AddUserToClassroomAsync_ShouldAddUserToClassroom()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(databaseName: "AddUserToClassroomAsync_ShouldAddUserToClassroom")
            .Options;

        using (var context = new KlockanContext(options))
        {
            var user = new User { Id = 1, RoleId = 1 };
            context.Users.Add(user);

            var classroom = new Classroom { Id = 1 };
            context.Classrooms.Add(classroom);

            await context.SaveChangesAsync();
        }

        // Act
        int? result;
        using (var context = new KlockanContext(options))
        {
            var repository = new MeetingRepository(context);
            result = await repository.AddUserToClassroomAsync(userId: 1, classroomId: 1);
        }

        // Assert
        using (var context = new KlockanContext(options))
        {
            var classroomUser = await context.ClassroomUsers.FirstOrDefaultAsync(cu => cu.UserId == 1 && cu.ClassroomId == 1);
            Assert.NotNull(classroomUser);
            Assert.Equal(1, classroomUser.UserId);
            Assert.Equal(1, classroomUser.RoleId);
            Assert.Equal(1, result);
        }
    }

    [Fact]
    public async Task GetMeetingByIdAsync_ShouldReturnMeeting()
    {
        // Arrange
        Meeting meeting = new Meeting
        {
            Id = 1,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30),
            ThirdPartyId = "123asbnonnullvalue"
        };        
        
        _context.Meetings.Add(meeting);
        await _context.SaveChangesAsync();

        var repository = new MeetingRepository(_context);
        var meetingIdToRetrieve = 1;

        // Act
        var result = await repository.GetMeetingByIdAsync(meetingIdToRetrieve);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Meeting>();                
    }
}
