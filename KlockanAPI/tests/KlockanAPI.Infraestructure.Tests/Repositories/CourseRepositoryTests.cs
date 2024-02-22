using Microsoft.EntityFrameworkCore;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;

namespace KlockanAPI.Infraestructure.Tests.Repositories;

public class CourseRepositoryTests : IDisposable
{
    private readonly KlockanContext _context;

    public CourseRepositoryTests()
    {
        DbContextOptionsBuilder<KlockanContext> dbContextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new KlockanContext(dbContextOptions.Options);
    }

    // Implement IDisposable to destroy the context after each test case
    public void Dispose()
    {
        // Make sure that the in-memory database is deleted at the end of all tests.
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnCourses()
    {
        // Arrange

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

        _context.Courses.AddRange(courses);
        await _context.SaveChangesAsync();

        var repository = new CourseRepository(_context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(courses.Count, result.Count());
    }

    [Fact]
    public async Task DeleteCourseAsync_ShouldReturnDeletedCourse()
    {
        // Arrange
        var course = new Course
        {
            Id = 1,
            Name = "Frontend Development",
            Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
            Sessions = 10,
            SessionDuration = 60,
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var repository = new CourseRepository(_context);

        // Act
        var result = await repository.DeleteCourseAsync(course);

        // Assert
        Assert.Equal(course, result);
    }

    [Fact]
    public async Task CreateCourseAsync_ShouldAddCourse_WhenCalled()
    {
        // Arrange
        var repository = new CourseRepository(_context);

        var newCourse = new Course { };

        // Act
        var result = await repository.CreateAsync(newCourse);

        // Assert
        Assert.NotNull(result);
        var courseInDb = await _context.Courses.FindAsync(result.Id);
        Assert.NotNull(courseInDb);

    }

    [Fact]
    public async Task UpdateCourseAsync_ShouldReturnUpdatedCourse()
    {
        // Arrange

        var initialCourse = new Course
        {
            Id = 1,
            Name = "Initial Course",
        };

        _context.Courses.Add(initialCourse);
        await _context.SaveChangesAsync();

        var updatedCourse = new Course
        {
            Id = 1,
            Name = "Updated Course",
        };

        var repository = new CourseRepository(_context);

        // Act
        var result = await repository.UpdateCourseAsync(updatedCourse);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedCourse.Id, result.Id);
        Assert.Equal(updatedCourse.Name, result.Name);
        // Add assertions for other properties as needed
    }

    [Fact]
    public async Task GetCourseByIdAsync_ShouldReturnCourseIfExists()
    {
        // Arrange

        var course = new Course
        {
            Id = 1,
            Name = "Frontend Development",
            Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
            Sessions = 10,
            SessionDuration = 60,
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var repository = new CourseRepository(_context);

        // Act
        var result = await repository.GetCourseByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(course.Id, result!.Id);
    }

    [Fact]
    public async Task GetCourseByIdAsync_ShouldReturnNullIfNotExists()
    {
        // Arrange

        var repository = new CourseRepository(_context);

        // Act
        var result = await repository.GetCourseByIdAsync(999); // Non-existent ID

        // Assert
        Assert.Null(result);
    }

}
