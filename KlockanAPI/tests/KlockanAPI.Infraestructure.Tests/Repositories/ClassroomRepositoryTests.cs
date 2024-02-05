using Microsoft.EntityFrameworkCore;

using FluentAssertions;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;

namespace KlockanAPI.Infraestructure.Tests.Repositories;

public class ClassroomRepositoryTests
{
    private readonly KlockanContext _context;

    public ClassroomRepositoryTests()
    {
        DbContextOptionsBuilder<KlockanContext> dbContextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new KlockanContext(dbContextOptions.Options);
    }

    private ClassroomRepository GetRepositoryInstance() => new(_context);

    [Fact]
    public async Task GetAllClassroomsAsync_ShouldReturnClassroomDTOs()
    {
        // Arrange
        var classroomsSample = new List<Classroom>
        {
            new Classroom
            {
                Id = 1,
                CourseId = 1,
                ProgramId = 1,
                StartDate = new DateOnly(2024, 2, 23),
                Meetings = [],
                Schedule = [],
                ClassroomUsers = [],
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Classroom
            {
                Id = 2,
                CourseId = 2,
                ProgramId = 1,
                StartDate = new DateOnly(2024, 2, 23),
                Meetings = [],
                Schedule = [],
                ClassroomUsers = [],
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Classroom
            {
                Id = 3,
                CourseId = 1,
                ProgramId = 2,
                StartDate = new DateOnly(2024, 2, 23),
                Meetings = [],
                Schedule = [],
                ClassroomUsers = [],
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            }
        };
        var programsSample = new List<Program> {
            new Program
            {
                Id = 1,
                Name = "Bootcamp Developers 01",
                Description = "Program covering concepts in software development.",
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Program
            {
                Id = 2,
                Name = "Advanced Bootcamp Developers 01",
                Description = "Program focused on advanced software design and development techniques.",
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            }
        };
        var coursesSample = new List<Course> {
            new Course
            {
                Id = 1,
                Name = "Frontend Development",
                Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
                Sessions = 10,
                SessionDuration = 60,
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Course
            {
                Id = 2,
                Name = "Backend Development",
                Description = "Course on server side programming, databases, and API construction.",
                Sessions = 12,
                SessionDuration = 75,
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
        };

        _context.Classrooms.AddRange(classroomsSample);
        _context.Programs.AddRange(programsSample);
        _context.Courses.AddRange(coursesSample);
        await _context.SaveChangesAsync();

        var repository = GetRepositoryInstance();
        // Act
        var result = await repository.GetAllClassroomsAsync();

        // Assert
        result.Should().BeEquivalentTo(classroomsSample);
        result.Should().HaveCount(classroomsSample.Count);
        result.Should().Equal(classroomsSample);
        result.First().Id.Should().Be(classroomsSample.First().Id);
        result.First().Course.Should().NotBeNull();
        result.First().Course!.Id.Should().Be(classroomsSample.First().Course!.Id);
    }

    [Fact]
    public async Task GetClassroomsByCourseIdAsync_ShouldReturnClassroomsByCourseId()
    {
        // Arrange
        var classrooms = new List<Classroom>
        {
            new Classroom { Id = 1, StartDate = new DateOnly(2024, 1, 23), CourseId = 1, ProgramId = 1 },
            new Classroom { Id = 2, StartDate = new DateOnly(2024, 1, 30), CourseId = 1, ProgramId = 1 },
            new Classroom { Id = 3, StartDate = new DateOnly(2024, 2, 6), CourseId = 2, ProgramId = 1 }
        };

        _context.Classrooms.AddRange(classrooms);
        await _context.SaveChangesAsync();

        var repository = GetRepositoryInstance();

        // Act
        var result = await repository.GetClassroomsByCourseIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result!.Count());
    }

    [Fact]
    public async Task GetClassroomsByProgramIdAsync_ShouldReturnClassroomsByProgramId()
    {
        // Arrange
        var classrooms = new List<Classroom>
        {
            new Classroom { Id = 1, StartDate = new DateOnly(2024, 1, 23), CourseId = 1, ProgramId = 1 },
            new Classroom { Id = 2, StartDate = new DateOnly(2024, 1, 30), CourseId = 1, ProgramId = 1 },
            new Classroom { Id = 3, StartDate = new DateOnly(2024, 2, 6), CourseId = 2, ProgramId = 1 }
        };

        _context.Classrooms.AddRange(classrooms);
        await _context.SaveChangesAsync();

        var repository = GetRepositoryInstance();

        // Act
        var result = await repository.GetClassroomsByProgramIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result!.Count());
    }
}
