using NSubstitute;
using FluentAssertions;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infraestructure.Tests.Repositories;

public class ScheduleRepositoryTests : IDisposable
{
    private readonly KlockanContext _context;

    public ScheduleRepositoryTests()
    {
        DbContextOptionsBuilder<KlockanContext> dbContextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new KlockanContext(dbContextOptions.Options);
    }
    private ScheduleRepository GetRepositoryInstance() => new(_context);

    public void Dispose()
    {
        // Make sure that the in-memory database is deleted at the end of all tests.
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task GetAllSchedulesAsync_ShouldReturnSchedules()
    {
        // Arrange
        var schedules = new List<Schedule>
        {
            new Schedule
            {
                Id = 1,
                WeekdayId = 1,
                ClassroomId = 1,
                StartTime = new TimeOnly(18,00)
            },
            new Schedule
            {
                Id = 2,
                WeekdayId = 2,
                ClassroomId = 2,
                StartTime = new TimeOnly(19,00)
            },
            new Schedule
            {
                Id = 3,
                WeekdayId = 3,
                ClassroomId = 3,
                StartTime = new TimeOnly(20,00)
            }
        };

        _context.Schedules.AddRange(schedules);
        await _context.SaveChangesAsync();

        var scheduleRepository = GetRepositoryInstance();
        // Act
        var result = await scheduleRepository.GetAllSchedulesAsync();

        // Assert
        result.Should().BeEquivalentTo(schedules);
        result.Should().HaveCount(schedules.Count);
        result.Should().Equal(schedules);
        result.First().WeekdayId.Should().Be(schedules.First().WeekdayId);
        result.First().ClassroomId.Should().Be(schedules.First().ClassroomId);
        result.First().StartTime.Should().Be(schedules.First().StartTime);

    }

    [Fact]
    public async Task CreateScheduleAsync_ShouldAddSchedule_WhenCalled()
    {
        // Arrange

        var repository = new ScheduleRepository(_context);

        var newSchedule = new Schedule { };

        // Act
        var result = await repository.CreateScheduleAsync(newSchedule);

        // Assert
        Assert.NotNull(result);
        var scheduleInDb = await _context.Schedules.FindAsync(result.Id);
        Assert.NotNull(scheduleInDb);

    }

    
}
