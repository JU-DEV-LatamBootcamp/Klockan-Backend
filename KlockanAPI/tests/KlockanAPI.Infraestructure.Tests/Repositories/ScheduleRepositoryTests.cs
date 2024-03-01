using Microsoft.EntityFrameworkCore;
using FluentAssertions;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;
using NSubstitute;

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
        _context.Database.EnsureDeleted();
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 2)]
    public async Task GetSchedulesByClassroomIdAsync_ShouldReturnAListOfSchedules_WhenClassroomIdIsProvided(int classroomId, int schedulesCount)
    {
        // Arrage
        var schedulesRepository = GetRepositoryInstance();

        var program = new Program() { Id = 1 };
        var course = new Course() { Id = 1 };
        var weekday = new Weekday() { Id = 1 };
        var classroom = new Classroom()
        {
            Id = 2,
            ProgramId = program.Id,
            CourseId = course.Id,
        };
        var schedules = new List<Schedule>() {
            new Schedule
            {
                Id = 1,
                ClassroomId = classroom.Id,
                WeekdayId = weekday.Id,
                StartTime = new TimeOnly(16, 30, 0),
            },
            new Schedule
            {
                Id = 2,
                ClassroomId = classroom.Id,
                WeekdayId = weekday.Id,
                StartTime = new TimeOnly(16, 30, 0),
            },
        };

        _context.Programs.Add(program);
        _context.Courses.Add(course);
        _context.Weekdays.Add(weekday);
        _context.Classrooms.Add(classroom);
        _context.Schedules.AddRange(schedules);

        await _context.SaveChangesAsync();

        // Act 
        var result = await schedulesRepository.GetSchedulesByClassroomIdAsync(classroomId);

        // Assert
        result.Count().Should().Be(schedulesCount);
    }
}
