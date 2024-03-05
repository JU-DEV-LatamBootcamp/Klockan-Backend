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

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
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
    public async Task GetAllPrograms_ShouldReturnError403()
    {
        // Arrange
        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer asdasd";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
        // Act
        var result = await controller.GetAllPrograms();

        // Verify the status code
        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(403);
    }


    [Fact]
    public async Task CreateProgram_Returns201Created_WithValidInput()
    {
        // Arrange
        var createProgramDTO = new CreateProgramDTO { /* Populate required properties */ };
        var createdProgramDTO = new ProgramDTO { /* Populate with expected result */ };
        _mockProgramService.Setup(service => service.CreateProgramAsync(createProgramDTO))
                           .ReturnsAsync(createdProgramDTO);

        var controller = _controller;

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };


        // Act
        var result = await controller.CreateProgram(createProgramDTO);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, actionResult.StatusCode);
        Assert.Equal(createdProgramDTO, actionResult.Value);
    }

    [Fact]
    public async Task CreateProgram_Returns400BadRequest_WithInvalidModel()
    {
        var controller = _controller;

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Arrange
        controller.ModelState.AddModelError("Error", "Sample error");

        // Act
        var result = await controller.CreateProgram(new CreateProgramDTO());

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

        var controller = _controller;
        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };


        // Act
        var result = await controller.CreateProgram(createProgramDTO);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, actionResult.StatusCode);
        Assert.Contains("Internal server error", actionResult?.Value?.ToString());
    }

    [Fact]
    public async Task DeleteProgram_ShouldReturnOk()
    {
        // Arrange
        ProgramDTO sampleProgram = new ProgramDTO
        {
            Id = 1,
            Name = "Frontend Development",
            Description = "Program to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
        };

        _programService.DeleteProgramAsync(1).Returns(Task.FromResult<ProgramDTO?>(sampleProgram));
        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Act
        var result = await controller.DeleteProgram(1);

        // Assert
        result.Should().BeOfType<ActionResult<ProgramDTO>>();

        result.Result.Should().BeOfType<OkObjectResult>();

        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        var okResult = result?.Result as OkObjectResult;
        var programData = okResult?.Value as ProgramDTO;
        programData.Should().BeEquivalentTo(sampleProgram);
    }


    [Fact]
    public async Task EditProgram_ShouldReturnOk()
    {
        // Arrange
        ProgramDTO sampleProgram = new ProgramDTO
        {
            Id = 1,
            Name = "Edited Frontend Development",
            Description = "Program to develop Web Applications focusing on HTML, CSS.",
        };

        _programService.EditProgramAsync(sampleProgram).Returns(Task.FromResult<ProgramDTO?>(sampleProgram));
        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Act
        var result = await controller.EditProgram(sampleProgram);

        // Assert
        result.Should().BeOfType<ActionResult<ProgramDTO>>();

        result.Result.Should().BeOfType<OkObjectResult>();

        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        var okResult = result?.Result as OkObjectResult;
        var programData = okResult?.Value as ProgramDTO;
        programData.Should().BeEquivalentTo(sampleProgram);
    }

    [Fact]
    public async Task EditProgram_WhenValidData_ReturnsOk()
    {
        //Arrange
        ProgramDTO sampleProgram = new ProgramDTO
        {
            Id = 1,
            Name = "Edited Frontend Development",
            Description = "Program to develop Web Applications focusing on HTML, CSS.",
        };

        _programService.EditProgramAsync(sampleProgram).Returns(Task.FromResult<ProgramDTO?>(sampleProgram)!);
        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Act
        var result = await controller.EditProgram(sampleProgram);

        // Assert
        result.Should().BeOfType<ActionResult<ProgramDTO>>();
        result.Result.Should().BeOfType<OkObjectResult>();

        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        var okResult = result?.Result as OkObjectResult;
        var programData = okResult?.Value as ProgramDTO;
        programData.Should().BeEquivalentTo(sampleProgram);
    }

    [Fact]
    public async Task EditProgram_WhenInvalidData_ReturnsBadRequest()
    {
        // Arrange
        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
        controller.ModelState.AddModelError("error", "Name is required"); // Add error yo model

        // Act
        var result = await controller.EditProgram(new ProgramDTO());

        // Assert
        result.Should().BeOfType<ActionResult<ProgramDTO>>();
        result.Result.Should().BeOfType<BadRequestObjectResult>();

        (result?.Result as BadRequestObjectResult)?.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task EditProgram_WhenServiceThrowsException_ReturnsInternalServerError()
    {
        // Arrange
        ProgramDTO sampleProgram = new ProgramDTO
        {
            Id = 1,
            Name = "Edited Frontend Development",
            Description = "Program to develop Web Applications focusing on HTML, CSS.",
        };

        _programService
            .When(x => x.EditProgramAsync(Arg.Any<ProgramDTO>()))
            .Throw(new Exception("Something went wrong in the service")); // Forzar que el servicio arroje una excepción
        var controller = GetControllerInstance();

        // Simulate user with admin role
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJHd3NYaVcxYWprMFhhVkV4aVU0eXlkUzNta0YtckNJSEZGTnk2cVlRX2V3In0.eyJleHAiOjE3MDk1NzM0NTEsImlhdCI6MTcwOTU3MzE1MSwianRpIjoiNzQ5OGZmYWItOTE5ZC00NjhkLTg3OGQtOWNmODE5N2U1NTUzIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODQ0My9yZWFsbXMvS2xvY2thbiIsInN1YiI6IjhjZWNjNzAyLWJiNjQtNDg2YS04ODExLTRmODNkNmM1MmIxYiIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFkbWluLWNsaSIsInNlc3Npb25fc3RhdGUiOiI4OGI3ZWMzYy1jN2E3LTQ0M2UtODQ1Zi1mYjYwNWIzZGFlNTYiLCJhY3IiOiIxIiwic2NvcGUiOiJlbWFpbCBwcm9maWxlIiwic2lkIjoiODhiN2VjM2MtYzdhNy00NDNlLTg0NWYtZmI2MDViM2RhZTU2IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJyb2xlcyI6WyJhZG1pbiJdLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiJ9.pVX5V5dFBUnTyJHk_Oar60FOI-toiIChVgsmWVhJcV8cXPPSNb625XOFI72tDLaiuLlQtalXaWbVvZId_n2cx9kSMfTspYCosgtwvDGfqFjAyTcVqKbe3_UWOsPq1wOImI9ExRmzddtVehZ9TBBOfkB6_UmSb2qKCavayLYOHGTGSaV1CvAzlREP1TKSi9r45Ql1MrUhZyKbUD1X4rTZD-uvshLGCY93dvFhgZPnaMW_jsagi97GivqPsb-bWwDLxZd9dv2owXlNyFBun-xSJsGwrK_XeINS79lZIu_rNbonPknkIU4DZr4asPBmBX0WLO1zvy6dah4OzTDDWS2hdQ";
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Act
        var result = await controller.EditProgram(sampleProgram);

        // Assert
        result.Should().BeOfType<ActionResult<ProgramDTO>>();
        result.Result.Should().BeOfType<ObjectResult>();

        (result?.Result as ObjectResult)?.StatusCode.Should().Be(500);
        (result?.Result as ObjectResult)?.Value.Should().Be("Internal server error: Something went wrong in the service");
    }

}
