using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Presentation.Controllers;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class CoursesControllerTests
{
    private readonly ICourseService _courseService;

    public CoursesControllerTests()
    {
        _courseService = Substitute.For<ICourseService>();
    }

    private CoursesController GetControllerInstance() => new(_courseService);

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        // Arrange
        var sampleCourses = new List<CourseDto>{
             new CourseDto
            {
                Id = 1,
                Name = "Frontend Development",
                Code = "FE",
                Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
                Sessions = 10,
                SessionDuration = 60,
            },
            new CourseDto
            {
                Id = 2,
                Name = "Backend Development",
                Code = "BE",
                Description = "Course on server side programming, databases, and API construction.",
                Sessions = 12,
                SessionDuration = 75,
            },
            new CourseDto
            {
                Id = 3,
                Name = "Full Stack Development",
                Code = "FS",
                Description = "Comprehensive course covering both frontend and backend development to build complete applications.",
                Sessions = 15,
                SessionDuration = 90,
            }
        };

        _courseService.GetAllCoursesAsync().Returns(Task.FromResult<IEnumerable<CourseDto>>(sampleCourses));
        var controller = GetControllerInstance();

        // Act
        var result = await controller.GetAll();

        // Assert
        result.Should().BeOfType<ActionResult<IEnumerable<CourseDto>>>();

        result.Result.Should().BeOfType<OkObjectResult>();

        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        var okResult = result?.Result as OkObjectResult;
        var coursesData = okResult?.Value as IEnumerable<CourseDto>;
        coursesData.Should().BeEquivalentTo(sampleCourses);
    }
}