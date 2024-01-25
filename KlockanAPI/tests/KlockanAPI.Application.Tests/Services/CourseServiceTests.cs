using NSubstitute;
using FluentAssertions;
using MapsterMapper;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Application.Services;

namespace KlockanAPI.Application.Tests.Services;

public class CourseServiceTests
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public CourseServiceTests()
    {
        _courseRepository = Substitute.For<ICourseRepository>();
        _mapper = new Mapper();
    }

    private CourseService GetServiceInstance() => new(_courseRepository, _mapper);

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
}

