using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;

using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Program;
using KlockanAPI.Presentation.Controllers;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class ProgramsControllerTests
{
    private readonly IProgramService _programService;
    private readonly Mock<IProgramService> _mockProgramService;
    private readonly ProgramsController _controller;

    public ProgramsControllerTests()
    {
        _programService = Substitute.For<IProgramService>();
        _mockProgramService = new Mock<IProgramService>();
        _controller = new ProgramsController(_mockProgramService.Object);
    }
    private ProgramsController GetControllerInstance() => new(_programService);

    [Fact]
    public async Task GetAllPrograms_ShouldReturnOk()
    {
        // Arrange
        _programService.GetAllProgramsAsync().Returns(new List<ProgramDTO>());
        var controller = GetControllerInstance();

        // Act
        var result = await controller.GetAllPrograms();

        // Verify that the result is an ActionResult<IEnumerable<ProgramDTO>>
        result.Should().BeOfType<ActionResult<IEnumerable<ProgramDTO>>>();

        // Verify that the Result of the ActionResult is an OkObjectResult
        result.Result.Should().BeOfType<OkObjectResult>();

        // Verify the status code
        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task CreateProgram_Returns201Created_WithValidInput()
    {
        // Arrange
        var createProgramDTO = new CreateProgramDTO { /* Populate required properties */ };
        var createdProgramDTO = new ProgramDTO { /* Populate with expected result */ };
        _mockProgramService.Setup(service => service.CreateProgramAsync(createProgramDTO))
                           .ReturnsAsync(createdProgramDTO);

        // Act
        var result = await _controller.CreateProgram(createProgramDTO);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, actionResult.StatusCode);
        Assert.Equal(createdProgramDTO, actionResult.Value);
    }

    [Fact]
    public async Task CreateProgram_Returns400BadRequest_WithInvalidModel()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Sample error");

        // Act
        var result = await _controller.CreateProgram(new CreateProgramDTO());

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, actionResult.StatusCode);
    }

    [Fact]
    public async Task CreateProgram_HandlesException_WithInternalServerError()
    {
        // Arrange
        var createProgramDTO = new CreateProgramDTO { /* Populate required properties */ };
        _mockProgramService.Setup(service => service.CreateProgramAsync(createProgramDTO))
                           .ThrowsAsync(new System.Exception("Test exception"));

        // Act
        var result = await _controller.CreateProgram(createProgramDTO);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, actionResult.StatusCode);
        Assert.Contains("Internal server error", actionResult.Value.ToString());
    }
}
