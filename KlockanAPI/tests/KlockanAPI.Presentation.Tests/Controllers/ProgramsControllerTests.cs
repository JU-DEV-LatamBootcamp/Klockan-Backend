using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Program;
using KlockanAPI.Presentation.Controllers;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class ProgramsControllerTests
{
    private readonly IProgramService _programService;

    public ProgramsControllerTests()
    {
        _programService = Substitute.For<IProgramService>();
    }
    private ProgramController GetControllerInstance() => new(_programService);

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
}
