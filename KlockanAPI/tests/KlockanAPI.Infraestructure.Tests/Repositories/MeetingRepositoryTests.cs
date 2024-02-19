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
}
