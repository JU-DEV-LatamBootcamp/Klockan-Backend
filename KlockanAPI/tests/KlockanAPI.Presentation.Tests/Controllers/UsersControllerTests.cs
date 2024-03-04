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
    private readonly Mock<IKeycloakUserService> _mockKeycloakUserService;
    private readonly Mock<IUserService> _mockUserService;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _userService = Substitute.For<IUserService>();
        _keycloakUserService = Substitute.For<IKeycloakUserService>();
        _mockUserService = new Mock<IUserService>();
        _mockKeycloakUserService = new Mock<IKeycloakUserService>();
        _controller = new UsersController(_mockUserService.Object, _mockKeycloakUserService.Object);
    }

    private UsersController GetControllerInstance() => new(_userService, _keycloakUserService);

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
        var createUserDTO = new CreateUserDTO { /* Populate required properties */ };
        var createdUserDTO = new UserDto { /* Populate with expected result */ };
        _mockUserService.Setup(service => service.CreateUserAsync(createUserDTO))
                           .ReturnsAsync(createdUserDTO);

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
        // Arrange
        controller.ModelState.AddModelError("Error", "Sample error");

        // Act
        var result = await controller.CreateUser(new CreateUserDTO());

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, actionResult.StatusCode);
    }

    [Fact]
    public async Task CreateUser_HandlesException_WithInternalServerError()
    {
        var controller = GetControllerInstance();
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
}
