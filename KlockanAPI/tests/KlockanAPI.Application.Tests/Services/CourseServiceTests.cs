using NSubstitute;
using FluentAssertions;
using MapsterMapper;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Application.Services;
using KlockanAPI.Application.CrossCutting;

namespace KlockanAPI.Application.Tests.Services;

public class CourseServiceTests
{
    private readonly ICourseRepository _courseRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;

    public CourseServiceTests()
    {
        _courseRepository = Substitute.For<ICourseRepository>();
        _classroomRepository = Substitute.For<IClassroomRepository>();
        _mapper = new Mapper();
    }

    private CourseService GetServiceInstance() => new(_courseRepository, _classroomRepository, _mapper);

    [Fact]
    public async Task GetAllCoursesAsync_ShouldReturnCourseDTOs()
    {
        // Arrange
        var courseService = GetServiceInstance();

        // Define some sample courses from the repository
        List<Course> sampleCourses = new List<Course>{
            new Course
            {
                Id = 1,
                Name = "Frontend Development",
                Code = "FE",
                Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
                Sessions = 10,
                SessionDuration = 60,
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Course
            {
                Id = 2,
                Name = "Backend Development",
                Code = "BE",
                Description = "Course on server side programming, databases, and API construction.",
                Sessions = 12,
                SessionDuration = 75,
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Course
            {
                Id = 3,
                Name = "Full Stack Development",
                Code = "FS",
                Description = "Comprehensive course covering both frontend and backend development to build complete applications.",
                Sessions = 15,
                SessionDuration = 90,
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        _courseRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<Course>>(sampleCourses));

        // Act
        var result = await courseService.GetAllCoursesAsync();

        // Assert
        result.Should().NotBeNull();

        result.Should().BeEquivalentTo(sampleCourses.Select(course => _mapper.Map<CourseDto>(course)));

        result.Should().HaveCount(sampleCourses.Count);

        result.Should().ContainItemsAssignableTo<CourseDto>();
    }

    [Fact]
    public async Task DeleteCourseAsync_ShouldReturnDeletedCourseDTO()
    {
        // Arrange
        CourseService courseService = GetServiceInstance();

        // Define a sample course from the repository
        Course sampleCourse = new Course
        {
            Id = 1,
            Name = "Frontend Development",
            Code = "FE",
            Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
            Sessions = 10,
            SessionDuration = 60,
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        };

        _courseRepository.GetCourseByIdAsync(1).Returns(Task.FromResult<Course?>(sampleCourse));
        _classroomRepository.GetClassroomsByCourseIdAsync(1).Returns(Task.FromResult<IEnumerable<Classroom>?>(null));

        _courseRepository.DeleteCourseAsync(sampleCourse).Returns(Task.FromResult(sampleCourse));

        // Act
        var result = await courseService.DeleteCourseAsync(1);

        // Assert
        result.Should().NotBeNull();

        result.Should().BeEquivalentTo(_mapper.Map<CourseDto>(sampleCourse));
    }

    [Fact]
    public async Task DeleteCourseAsync_ShouldThrowNotFoundException_WhenCourseNotFound()
    {
        // Arrange
        CourseService courseService = GetServiceInstance();

        _courseRepository.GetCourseByIdAsync(1).Returns(Task.FromResult<Course?>(null));

        // Act
        Func<Task> act = async () => await courseService.DeleteCourseAsync(1);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Course with id 1 not found");
    }

    [Fact]
    public async Task DeleteCourseAsync_ShoulThrowFoundException_WhenCourseIsUsedInClassroom()
    {
        // Arrange
        CourseService courseService = GetServiceInstance();

        Course sampleCourse = new Course
        {
            Id = 1,
            Name = "Frontend Development",
            Code = "FE",
            Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
            Sessions = 10,
            SessionDuration = 60,
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        };

        Classroom sampleClassroom = new Classroom
        {
            Id = 1,
            StartDate = new DateOnly(2024, 1, 23),
            CourseId = 1,
            ProgramId = 1,
        };

        _courseRepository.GetCourseByIdAsync(1).Returns(Task.FromResult<Course?>(sampleCourse));
        _classroomRepository.GetClassroomsByCourseIdAsync(1).Returns(Task.FromResult<IEnumerable<Classroom>?>(new List<Classroom> { sampleClassroom }));

        // Act
        Func<Task> act = async () => await courseService.DeleteCourseAsync(1);

        // Assert
        await act.Should().ThrowAsync<FoundException>().WithMessage("Course with id 1 is used in a classroom");
    }
}

