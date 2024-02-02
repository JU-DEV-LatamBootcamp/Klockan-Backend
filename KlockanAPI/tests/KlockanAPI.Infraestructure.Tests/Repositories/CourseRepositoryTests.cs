using Microsoft.EntityFrameworkCore;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;

namespace KlockanAPI.Infraestructure.Tests.Repositories;

public class CourseRepositoryTests : IDisposable
{
    private readonly DbContextOptions<KlockanContext> _options;
    public CourseRepositoryTests()
    {
        // Configure the options of the context of the in memory database
        _options = new DbContextOptionsBuilder<KlockanContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;
    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnCourses()
    {
        // Arrange
        using var context = new KlockanContext(_options);

        var courses = new List<Course>
        {
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
            new Course
            {
                Id = 3,
                Name = "Full Stack Development",
                Description = "Comprehensive course covering both frontend and backend development to build complete applications.",
                Sessions = 15,
                SessionDuration = 90,
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        context.Courses.AddRange(courses);
        await context.SaveChangesAsync();

        var repository = new CourseRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(courses.Count, result.Count());
    }

    [Fact]
    public async Task DeleteCourseAsync_ShouldReturnDeletedCourse()
    {
        // Arrange
        using var context = new KlockanContext(_options);

        var course = new Course
        {
            Id = 1,
            Name = "Frontend Development",
            Code = "FE",
            Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
            Sessions = 10,
            SessionDuration = 60,
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        };

        context.Courses.Add(course);
        await context.SaveChangesAsync();

        var repository = new CourseRepository(context);

        // Act
        var result = await repository.DeleteCourseAsync(course);

        // Assert
        Assert.Equal(course, result);
    }

    // Implement IDisposable to destroy the context after each test case
    public void Dispose()
    {
        using var context = new KlockanContext(_options);
        // Make sure that the in-memory database is deleted at the end of all tests.
        context.Database.EnsureDeleted();
    }
}
