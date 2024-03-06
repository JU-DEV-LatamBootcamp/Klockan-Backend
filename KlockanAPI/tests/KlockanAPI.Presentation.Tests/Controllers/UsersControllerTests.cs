using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Moq;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.KeycloakAPI.Interfaces;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class UsersControllerTests
{
    private readonly IUserService _userService;
    private readonly IKeycloakUserService _keycloakUserService;
    private readonly IKeycloakAuthService _keycloakAuthService;
    private readonly Mock<IKeycloakUserService> _mockKeycloakUserService;
    private readonly Mock<IKeycloakAuthService> _mockKeycloakAuthService;
    private readonly Mock<IUserService> _mockUserService;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _userService = Substitute.For<IUserService>();
        _mockUserService = new Mock<IUserService>();
        _mockKeycloakUserService = new Mock<IKeycloakUserService>();
        _keycloakUserService = Substitute.For<IKeycloakUserService>();
        _mockKeycloakAuthService = new Mock<IKeycloakAuthService>();
        _keycloakAuthService = Substitute.For<IKeycloakAuthService>();
        _controller = new UsersController(_mockUserService.Object, _mockKeycloakUserService.Object, _mockKeycloakAuthService.Object);
    }

    private UsersController GetControllerInstance() => new(_userService, _keycloakUserService, _keycloakAuthService);

    [Fact]
    public async Task GetAllUsers_ShouldReturnOk()
    {
        // Arrange
        int pageSize = 10;
        int pageNumber = 1;

        _userService.GetAllUsersAsync(pageSize, pageNumber).Returns(new List<UserDto>());
        var controller = GetControllerInstance();

        // Act
        var result = await controller.GetAllUsers();

        // Verify that the result is an ActionResult<IEnumerable<UserDto>>
        result.Should().BeOfType<ActionResult<IEnumerable<UserDto>>>();

        // Verify that the Result of the ActionResult is an OkObjectResult
        result.Result.Should().BeOfType<OkObjectResult>();

        // Verify the status code
        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnInternalServerError()
    {
        int pageSize = 10;
        var pageNumber = 1;
        // Arrange
        _userService.GetAllUsersAsync(pageSize, pageNumber).Throws(new Exception("An error occurred"));
        var controller = GetControllerInstance();

        // Act
        var result = await controller.GetAllUsers();

        // Verify that the result is an ActionResult<IEnumerable<UserDto>>
        result.Should().BeOfType<ActionResult<IEnumerable<UserDto>>>();

        // Verify that the Result of the ActionResult is an ObjectResult
        result.Result.Should().BeOfType<ObjectResult>();

        // Verify the status code
        (result?.Result as ObjectResult)?.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task CreateUser_Returns201Created_WithValidInput()
    {
        // Arrange
        var createUserDTO = new CreateUserDTO
        {
            FirstName = "John",
            LastName = "Doe",
            Avatar = "https://example.com/avatar.jpg",
            Email = "john.doe@example.com",
            Birthdate = new DateOnly(1990, 5, 15),
            CityId = 123,
            RoleId = 1
        };
        var createdUserDTO = new UserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Avatar = "https://example.com/avatar.jpg",
            Email = "john.doe@example.com",
            Birthdate = new DateOnly(1990, 5, 15),
            RoleId = 1
        };

        _mockUserService.Setup(service => service.CreateUserAsync(createUserDTO))
                               .ReturnsAsync(createdUserDTO);


        var controller = _controller;

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };


        // Act
        var result = await _controller.CreateUser(createUserDTO);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, actionResult.StatusCode);
        Assert.Equal(createdUserDTO, actionResult.Value);
    }

    [Fact]
    public async Task CreateUser_Returns400BadRequest_WithInvalidModel()
    {
        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };


        // Arrange
        controller.ModelState.AddModelError("Error", "Sample error");

        // Act
        var result = await controller.CreateUser(new CreateUserDTO());

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, actionResult.StatusCode);
    }

    [Fact]
    public async Task CreateUser_ReturnsError403()
    {
        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer asdasd";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };


        // Act
        var result = await controller.CreateUser(new CreateUserDTO());

        // Assert
        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(403);
    }

    [Fact]
    public async Task CreateUser_HandlesException_WithInternalServerError()
    {
        var controller = GetControllerInstance();
        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Arrange
        var createUserDTO = new CreateUserDTO { /* Populate required properties */ };
        _mockUserService.Setup(service => service.CreateUserAsync(createUserDTO))
                           .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await controller.CreateUser(createUserDTO);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, actionResult.StatusCode);
        Assert.Contains("Internal server error", actionResult?.Value?.ToString());
    }

    [Fact]
    public async Task UpdateUser_ReturnsOk_WithValidInput()
    {
        // Arrange
        var updatedUser = new UserDto
        {
            FirstName = "Updated User",
            Email = "martin.lopez@jala.university"
        };

        var updatedUserDTO = new UserDto
        {
            FirstName = "Updated User",
            Email = "martin.lopez@jala.university"
        };

        _userService.UpdateUserAsync(updatedUser).Returns(Task.FromResult(updatedUserDTO));

        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };


        // Act
        var result = await controller.UpdateUser(updatedUser);

        // Assert
        result.Should().BeOfType<ActionResult<UserDto>>();

        result.Result.Should().BeOfType<OkObjectResult>();

        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        var okResult = result?.Result as OkObjectResult;
        var courseData = okResult?.Value as UserDto;
        courseData.Should().BeEquivalentTo(updatedUserDTO);
    }
}
