using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Moq;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.User;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class UsersControllerTests
{
    private readonly IUserService _userService;
    private readonly Mock<IUserService> _mockUserService;

    public UsersControllerTests()
    {
        _userService = Substitute.For<IUserService>();
        _mockUserService = new Mock<IUserService>();
    }

    private UsersController GetControllerInstance() => new(_userService);

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
}
