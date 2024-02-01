using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infraestructure.Tests.Repositories;


public class MeetingRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllCourses()
    {
        var options = new DbContextOptionsBuilder<KlockanContext>()
          .UseInMemoryDatabase(databaseName: "TestDb")
          .Options;

        using var context = new KlockanContext(options);

        var meetings = new List<Meeting> {
            new Meeting{
            Id = 1,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Meeting{
            Id = 2,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Meeting{
            Id = 3,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Meeting{
            Id = 4,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
        };

        context.Meetings.AddRange(meetings);
        await context.SaveChangesAsync();
        var repository = new MeetingRepository(context);

        //Act
        var result = await repository.GetAllAsync();

        //Assert 
        Assert.Equal(meetings.Count, result.Count());
    }
}