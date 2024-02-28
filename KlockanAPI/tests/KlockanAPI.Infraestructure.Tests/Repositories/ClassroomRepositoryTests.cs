using Microsoft.EntityFrameworkCore;
using FluentAssertions;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;

namespace KlockanAPI.Infraestructure.Tests.Repositories;

public class ClassroomRepositoryTests : IDisposable
{
    private readonly KlockanContext _context;

    public ClassroomRepositoryTests()
    {
        DbContextOptionsBuilder<KlockanContext> dbContextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new KlockanContext(dbContextOptions.Options);
    }

    private ClassroomRepository GetRepositoryInstance() => new(_context);

    // Implement IDisposable to destroy the context after each test case
    public void Dispose()
    {
        // Make sure that the in-memory database is deleted at the end of all tests.
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task GetAllClassroomsAsync_ShouldReturnClassroomDTOs()
    {
        // Arrange
        var programsSample = new List<Program> {
            new Program { Id = 1, Name = "First Program" }
        };

        var coursesSample = new List<Course> {
            new Course { Id = 1, Name = "First Course" }
        };

        var classroomsSample = new List<Classroom>
        {
            new Classroom { Id = 1, StartDate = new DateOnly(2024, 1, 23), CourseId = 1, ProgramId = 1 },
            new Classroom { Id = 2, StartDate = new DateOnly(2024, 1, 30), CourseId = 1, ProgramId = 1 },
            new Classroom { Id = 3, StartDate = new DateOnly(2024, 2, 6), CourseId = 1, ProgramId = 1 }
        };

        _context.Programs.AddRange(programsSample);
        _context.Courses.AddRange(coursesSample);
        _context.Classrooms.AddRange(classroomsSample);

        await _context.SaveChangesAsync();

        var repository = GetRepositoryInstance();

        // Act
        var result = await repository.GetAllClassroomsAsync();

        // Assert
        result.Should().BeEquivalentTo(classroomsSample);
        result.Should().HaveCount(classroomsSample.Count);
        result.Should().Equal(classroomsSample);
        result.First().Id.Should().Be(classroomsSample.First().Id);
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

    [Fact]
    public async Task DeleteClassroomAsync_ShouldReturnDeletedClassroom()
    {
        var classroom = new Classroom
        {
            Id = 1,
            StartDate = new DateOnly(2024, 1, 23),
            CourseId = 1,
            ProgramId = 1,
        };

        _context.Classrooms.Add(classroom);
        await _context.SaveChangesAsync();

        var repository = new ClassroomRepository(_context);

        var result = await repository.DeleteClassroomAsync(classroom);

        Assert.Equal(classroom, result);
    }

    [Fact]
    public async Task GetClassroomByIdAsync_ShouldReturnClassroomIfExists()
    {
        // Arrange
        var classroom = new Classroom
        {
            Id = 1,
            StartDate = new DateOnly(2024, 1, 23),
            CourseId = 1,
            ProgramId = 1,
        };

        _context.Classrooms.Add(classroom);
        await _context.SaveChangesAsync();

        var repository = new ClassroomRepository(_context);

        // Act
        var result = await repository.GetClassroomByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(classroom.Id, result!.Id);
    }

    [Fact]
    public async Task GetClassroomByIdAsync_ShouldReturnNullIfNotExists()
    {
        // Arrange
        var repository = new ClassroomRepository(_context);

        // Act
        var result = await repository.GetClassroomByIdAsync(999); // Non-existent ID

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteClassroomAsync_ShouldDeleteClassroomAndReturnIt()
    {
        // Arrange
        var classroom = new Classroom
        {
            Id = 1,
            StartDate = new DateOnly(2024, 1, 23),
            CourseId = 1,
            ProgramId = 1,
        };

        _context.Classrooms.Add(classroom);
        await _context.SaveChangesAsync();

        var repository = new ClassroomRepository(_context);

        // Act
        var result = await repository.DeleteClassroomAsync(classroom);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(classroom, result);

        // Ensure that the classroom has been removed from the context
        var deletedClassroom = await _context.Classrooms.FindAsync(classroom.Id);
        Assert.Null(deletedClassroom);
    }

    [Fact]
    public async Task GetClassroomsByCourseIdAsync_ReturnsNullWhenNoClassroomsFoundForCourseId()
    {
        // Arrange
        var courseIdWithoutClassrooms = 999; // Assuming there are no classrooms associated with courseId 999
        var repository = GetRepositoryInstance();

        // Act
        var result = await repository.GetClassroomsByCourseIdAsync(courseIdWithoutClassrooms);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetClassroomsByProgramIdAsync_ReturnsNullWhenNoClassroomsFoundForProgramId()
    {
        // Arrange
        var programIdWithoutClassrooms = 999; // Assuming there are no classrooms associated with programId 999
        var repository = GetRepositoryInstance();

        // Act
        var result = await repository.GetClassroomsByProgramIdAsync(programIdWithoutClassrooms);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateClassroomAsync_ShouldAddClassroom_WhenCalled()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(databaseName: "ClassroomRepositoryDB")
            .Options;

        using (var context = new KlockanContext(options))
        {
            var repository = new ClassroomRepository(context);

            var newClassroom = new Classroom
            {
                // Configura las propiedades necesarias de tu modelo
            };

            // Act
            var result = await repository.CreateClassroomAsync(newClassroom);

            // Assert
            Assert.NotNull(result);
            var classroomInDb = await context.Classrooms.FindAsync(result.Id);
            Assert.NotNull(classroomInDb);
        }
    }

    [Fact]
    public async Task UpdateClassroomAsync_ShouldUpdateClassroom_WhenClassroomChange()
    {
        // Arrange
        var classroomRepository = GetRepositoryInstance();

        var program = new Program()
        {
            Id = 1,
            Name = "Program 1",
            Description = "Program 1",
        };

        var course = new Course()
        {
            Id = 1,
            Name = "Course 1",
            Description = "Course 1",
            Sessions = 10,
            SessionDuration = 60,
        };

        var weekday = new Weekday() { Id = 1, Name = "Monday" };

        var classroomId = 5;
        var classroom = new Classroom()
        {
            Id = classroomId,
            ProgramId = program.Id,
            CourseId = course.Id,
            StartDate = new DateOnly(2024, 1, 1),
        };

        var updatedClassroom = new Classroom()
        {
            Id = classroom.Id,
            ProgramId = program.Id,
            CourseId = course.Id,
            StartDate = new DateOnly(2024, 7, 7),
        };

        _context.Programs.Add(program);
        _context.Courses.Add(course);
        _context.Weekdays.Add(weekday);
        _context.Classrooms.Add(classroom);

        // Act
        var result = await classroomRepository.UpdateClassroomAsync(updatedClassroom);

        // Assert
        result.Should().NotBeNull();
        result.StartDate.Should().Be(updatedClassroom.StartDate);
    }

    [Fact]
    public async Task UpdateClassroomAsync_ShouldUpdateNestedSchedules_WhenSchedulesChange()
    {
        // Arrange
        var classroomRepository = GetRepositoryInstance();

        var program = new Program()
        {
            Id = 1,
            Name = "Program 1",
            Description = "Program 1",
        };

        var course = new Course()
        {
            Id = 1,
            Name = "Course 1",
            Description = "Course 1",
            Sessions = 10,
            SessionDuration = 60,
        };

        var weekday = new Weekday() { Id = 1, Name = "Monday" };

        var classroomId = 5;
        var classroom = new Classroom()
        {
            Id = classroomId,
            ProgramId = program.Id,
            CourseId = course.Id,
            StartDate = new DateOnly(2024, 1, 1),
            Schedule = new List<Schedule>() {
                new Schedule
                {
                    Id = 1,
                    ClassroomId = classroomId,
                    WeekdayId = weekday.Id,
                    StartTime = new TimeOnly(15, 30, 0),
                },
                new Schedule
                {
                    Id = 2,
                    ClassroomId = classroomId,
                    WeekdayId = weekday.Id,
                    StartTime = new TimeOnly(15, 30, 0),
                },
            }
        };

        var updatedClassroom = new Classroom()
        {
            Id = classroom.Id,
            ProgramId = program.Id,
            CourseId = course.Id,
            StartDate = new DateOnly(2024, 7, 7),
            Schedule = new List<Schedule>() {
                new Schedule
                {
                    Id = 2,
                    ClassroomId = classroom.Id,
                    WeekdayId = weekday.Id,
                    StartTime = new TimeOnly(16, 30, 0),
                },
                new Schedule
                {
                    Id = 3,
                    ClassroomId = classroom.Id,
                    WeekdayId = weekday.Id,
                    StartTime = new TimeOnly(16, 30, 0),
                },
            }
        };

        _context.Programs.Add(program);
        _context.Courses.Add(course);
        _context.Weekdays.Add(weekday);
        _context.Classrooms.Add(classroom);

        // Act
        var result = await classroomRepository.UpdateClassroomAsync(updatedClassroom);

        // Assert
        result.Should().NotBeNull();
        result.Schedule.Where(s => s.Id == 1).Count().Should().Be(0); // schedule with id 1 should be removed
        result.Schedule.Where(s => s.Id == 2).First().StartTime.Should().Be(
            updatedClassroom.Schedule.Where(s => s.Id == 2).First().StartTime
        ); // schedule with id 2 should be modified
        result.Schedule.Where(s => s.Id == 3).Count().Should().Be(1); // schedule with id 3 should be added
    }
}
