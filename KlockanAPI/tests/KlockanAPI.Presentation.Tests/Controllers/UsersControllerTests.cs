using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Moq;
using NSubstitute;
using FluentAssertions;

using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.User;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class UsersControllerTests
{
    private readonly IUserService _userService;
    private readonly Mock<IUserService> _mockUserService;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _userService = Substitute.For<IUserService>();
        _mockUserService = new Mock<IUserService>();
        _controller = new UsersController(_mockUserService.Object);
    }

    private UsersController GetControllerInstance() => new(_userService);

    [Fact]
    public async Task GetAllUsers_ShouldReturnOk()
    {
        // Arrange
        _userService.GetAllUsersAsync().Returns(new List<UserDto>());
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
}
