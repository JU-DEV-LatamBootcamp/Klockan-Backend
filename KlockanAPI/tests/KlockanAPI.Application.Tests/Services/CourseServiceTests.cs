using NSubstitute;
using FluentAssertions;
using MapsterMapper;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Application.Services;
using KlockanAPI.Application.CrossCutting;
using Moq;

namespace KlockanAPI.Application.Tests.Services;

public class CourseServiceTests
{
    private readonly ICourseRepository _courseRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;
    private readonly Mock<ICourseRepository> _courseRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

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

        _courseRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<Course>>(sampleCourses));

        // Act
        var result = await courseService.GetAllCoursesAsync();

        // Assert
        result.Should().NotBeNull();

        result.Should().BeEquivalentTo(sampleCourses.Select(course => _mapper.Map<CourseDTO>(course)));

        result.Should().HaveCount(sampleCourses.Count);

        result.Should().ContainItemsAssignableTo<CourseDTO>();
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

        result.Should().BeEquivalentTo(_mapper.Map<CourseDTO>(sampleCourse));
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

    [Fact]
    public async Task CreateProgramAsync_ShouldReturnProgramDTO_WhenCreateIsSuccessful()
    {
        // Arrange
        var createCourseDTO = new CreateCourseDTO
        {
            Name = "Create Course DTO Test",
            Description = "Create Course DTO Test Description.",
        };

        var course = new Course
        {
            Id = 1,
            Name = "Course Test",
            Description = "Course Test Description.",
            Sessions = 10,
            SessionDuration = 60,
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        };

        var courseDTO = new CourseDTO
        {
            Id = 2,
            Name = "Course DTO Test",
            Description = "Course DTO Test Description.",
        };

        _mapperMock.Setup(m => m.Map<Course>(It.IsAny<CreateCourseDTO>())).Returns(course);
        _courseRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Course>())).ReturnsAsync(course);
        _mapperMock.Setup(m => m.Map<CourseDTO>(It.IsAny<Course>())).Returns(courseDTO);

        var service = new CourseService(_courseRepositoryMock.Object, _classroomRepository, _mapperMock.Object);

        // Act
        var result = await service.CreateCourseAsync(createCourseDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(courseDTO, result);
        Assert.Equal(courseDTO.Name, result.Name);
        Assert.Equal(courseDTO.Description, result.Description);
        _courseRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Course>()), Times.Once);
        _mapperMock.Verify(m => m.Map<CourseDTO>(It.IsAny<Course>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCourseAsync_ShouldReturnUpdatedCourseDTO()
    {
        // Arrange
        var courseService = GetServiceInstance();

        var initialCourse = new Course
        {
            Id = 1,
            Name = "Initial Course",
        };

        _courseRepository.GetCourseByIdAsync(1).Returns(Task.FromResult<Course?>(initialCourse));

        var updatedCourse = new Course
        {
            Id = 1,
            Name = "Updated Course",
        };

        _courseRepository
            .UpdateCourseAsync(Arg.Any<Course>())
            .Returns(callInfo =>
            {
                var courseToUpdate = callInfo.ArgAt<Course>(0);

                initialCourse.Name = courseToUpdate.Name;
                initialCourse.UpdatedAt = DateTime.UtcNow;

                return Task.FromResult(initialCourse);
            });

        // Act
        var result = await courseService.UpdateCourseAsync(updatedCourse);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.Map<CourseDTO>(updatedCourse));
    }

    [Fact]
    public async Task UpdateCourseAsync_ShouldThrowNotFoundException_WhenCourseNotFound()
    {
        // Arrange
        var courseService = GetServiceInstance();

        _courseRepository.GetCourseByIdAsync(1).Returns(Task.FromResult<Course?>(null));

        var updatedCourse = new Course
        {
            Id = 1,
            Name = "Updated Course",
            // Set other properties with updated values
        };

        // Act
        Func<Task> act = async () => await courseService.UpdateCourseAsync(updatedCourse);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Course with id 1 not found");
    }
}

