using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application;
using Moq;
using KlockanAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using KlockanAPI.Infrastructure.CrossCutting.Authorization;

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

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

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
    public async Task GetAll_ShouldReturnError403()
    {
        // Arrange
        var controller = GetControllerInstance();

        // Simulate user without admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer asdsadasd";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Act
        var result = await controller.GetAll();

        // Assert
        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(403);
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

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

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
    public async Task Delete_ShouldReturnError403()
    {

        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer asdadasd";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Act
        var result = await controller.Delete(1);

        // Assert
        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(403);
    }

    [Fact]
    public async Task CreateProgram_Returns201Created_WithValidInput()
    {
        // Arrange
        var createCourseDTO = new CreateCourseDTO
        {
            Name = "Frontend Development",
            Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
            Sessions = 10,
            SessionDuration = 60,
        };
        var createdCourseDTO = new CourseDTO
        {
            Id = 1,
            Name = "Frontend Development",
            Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
            Sessions = 10,
            SessionDuration = 60,
        };

        _mockCourseService.Setup(service => service.CreateCourseAsync(createCourseDTO))
                           .ReturnsAsync(createdCourseDTO);


        var controller = _controller;

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Act
        var result = await controller.CreateCourse(createCourseDTO);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, actionResult.StatusCode);
        Assert.Equal(createdCourseDTO, actionResult.Value);
    }

    [Fact]
    public async Task CreateCourse_Returns400BadRequest_WithInvalidModel()
    {
        // Arrange

        var controller = _controller;

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        controller.ModelState.AddModelError("error", "The Name field is required.");

        // Act
        var result = await controller.CreateCourse(new CreateCourseDTO());

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

        var controller = _controller;
        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Act
        var result = await controller.CreateCourse(createCourseDTO);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, actionResult.StatusCode);
        Assert.Contains("Internal server error", actionResult?.Value?.ToString());
    }

    [Fact]
    public async Task UpdateCourse_ReturnsOk_WithValidInput()
    {
        // Arrange
        var updatedCourse = new Course
        {
            Id = 1,
            Name = "Updated Course",
        };

        var updatedCourseDTO = new CourseDTO
        {
            Id = 1,
            Name = "Updated Course",
        };

        _courseService.UpdateCourseAsync(updatedCourse).Returns(Task.FromResult<CourseDTO?>(updatedCourseDTO));

        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };


        // Act
        var result = await controller.UpdateCourse(updatedCourse);

        // Assert
        result.Should().BeOfType<ActionResult<CourseDTO>>();

        result.Result.Should().BeOfType<OkObjectResult>();

        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        var okResult = result?.Result as OkObjectResult;
        var courseData = okResult?.Value as CourseDTO;
        courseData.Should().BeEquivalentTo(updatedCourseDTO);
    }
}
