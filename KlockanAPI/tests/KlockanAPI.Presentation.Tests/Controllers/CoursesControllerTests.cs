using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application;
using Moq;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class CoursesControllerTests
{
    private readonly ICourseService _courseService;
    private readonly Mock<ICourseService> _mockCourseService;
    private readonly CoursesController _controller;

    public CoursesControllerTests()
    {
        _courseService = Substitute.For<ICourseService>();
        _mockCourseService = new Mock<ICourseService>();
        _controller = new CoursesController(_mockCourseService.Object);
    }

    private CoursesController GetControllerInstance() => new(_courseService);

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        // Arrange
        var sampleCourses = new List<CourseDTO>{
             new CourseDTO
            {
                Id = 1,
                Name = "Frontend Development",
                Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
                Sessions = 10,
                SessionDuration = 60,
            },
            new CourseDTO
            {
                Id = 2,
                Name = "Backend Development",
                Description = "Course on server side programming, databases, and API construction.",
                Sessions = 12,
                SessionDuration = 75,
            },
            new CourseDTO
            {
                Id = 3,
                Name = "Full Stack Development",
                Description = "Comprehensive course covering both frontend and backend development to build complete applications.",
                Sessions = 15,
                SessionDuration = 90,
            }
        };

        _courseService.GetAllCoursesAsync().Returns(Task.FromResult<IEnumerable<CourseDTO>>(sampleCourses));
        var controller = GetControllerInstance();

        // Act
        var result = await controller.GetAll();

        // Assert
        result.Should().BeOfType<ActionResult<IEnumerable<CourseDTO>>>();

        result.Result.Should().BeOfType<OkObjectResult>();

        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        var okResult = result?.Result as OkObjectResult;
        var coursesData = okResult?.Value as IEnumerable<CourseDTO>;
        coursesData.Should().BeEquivalentTo(sampleCourses);
    }

    [Fact]
    public async Task Delete_ShouldReturnOk()
    {
        // Arrange
        CourseDTO sampleCourse = new CourseDTO
        {
            Id = 1,
            Name = "Frontend Development",
            Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
            Sessions = 10,
            SessionDuration = 60,
        };

        _courseService.DeleteCourseAsync(1).Returns(Task.FromResult<CourseDTO?>(sampleCourse));
        var controller = GetControllerInstance();

        // Act
        var result = await controller.Delete(1);

        // Assert
        result.Should().BeOfType<ActionResult<CourseDTO>>();

        result.Result.Should().BeOfType<OkObjectResult>();

        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        var okResult = result?.Result as OkObjectResult;
        var courseData = okResult?.Value as CourseDTO;
        courseData.Should().BeEquivalentTo(sampleCourse);
    }

    [Fact]
    public async Task CreateProgram_Returns201Created_WithValidInput()
    {
        // Arrange
        var createCourseDTO = new CreateCourseDTO { /* Populate required properties */ };
        var createdCourseDTO = new CourseDTO { /* Populate with expected result */ };
        _mockCourseService.Setup(service => service.CreateCourseAsync(createCourseDTO))
                           .ReturnsAsync(createdCourseDTO);

        // Act
        var result = await _controller.CreateCourse(createCourseDTO);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, actionResult.StatusCode);
        Assert.Equal(createdCourseDTO, actionResult.Value);
    }

    [Fact]
    public async Task CreateCourse_Returns400BadRequest_WithInvalidModel()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Sample error");

        // Act
        var result = await _controller.CreateCourse(new CreateCourseDTO());

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, actionResult.StatusCode);
    }

    [Fact]
    public async Task CreateCourse_HandlesException_WithInternalServerError()
    {
        // Arrange
        var createCourseDTO = new CreateCourseDTO { /* Populate required properties */ };
        _mockCourseService.Setup(service => service.CreateCourseAsync(createCourseDTO))
                           .ThrowsAsync(new System.Exception("Test exception"));

        // Act
        var result = await _controller.CreateCourse(createCourseDTO);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, actionResult.StatusCode);
        Assert.Contains("Internal server error", actionResult.Value.ToString());
    }    
}
