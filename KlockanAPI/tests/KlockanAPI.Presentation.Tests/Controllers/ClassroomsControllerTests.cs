using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.Classroom;
using Moq;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class ClassroomsControllerTests
{
    private readonly IClassroomService _classroomService;

    public ClassroomsControllerTests()
    {
        _classroomService = Substitute.For<IClassroomService>();
    }

    private ClassroomsController GetControllerInstance() => new(_classroomService);

    [Fact]
    public async Task GetAllClassrooms_ShouldReturnOk()
    {
        // Arrange
        List<ClassroomDTO> sampleClassrooms = new List<ClassroomDTO>{
            new ClassroomDTO
            {
                Id = 1,
                StartDate = new DateOnly(2024, 2, 23),
                CourseId = 1,
                ProgramId = 1,
                Program = null,
                Course = null,
            },
        };

        _classroomService.GetAllClassroomsAsync().Returns(Task.FromResult<IEnumerable<ClassroomDTO>>(sampleClassrooms));
        var controller = GetControllerInstance();

        // Act
        var result = await controller.GetAllClassrooms();

        // Verify that the result is an ActionResult<IEnumerable<ClassroomDTO>>
        result.Should().BeOfType<ActionResult<IEnumerable<ClassroomDTO>>>();

        // Verify that the Result of the ActionResult is an OkObjectResult
        result.Result.Should().BeOfType<OkObjectResult>();

        // Verify that result is not null
        result.Result.Should().NotBeNull();

        // Verify the status code
        (result.Result as OkObjectResult)!.StatusCode.Should().Be(200);

        // Verify that first item of the value returned is equivalent to the first item of the classrooms sample
        ((result.Result as OkObjectResult)!.Value as IEnumerable<ClassroomDTO>)?.First().Should().BeEquivalentTo(sampleClassrooms.First());
    }

    [Fact]
    public async Task CreateClassroom_Returns201Created_WithValidInput()
    {

    }


    [Fact]
    public async Task DeleteClassroom_ShouldReturnOk()
    {
        ClassroomDTO sampleClassroom = new ClassroomDTO
        {
            Id = 1,
            StartDate = new DateOnly(2024, 1, 23),
            CourseId = 1,
            ProgramId = 1,
        };

        _classroomService.DeleteClassroomAsync(1).Returns(Task.FromResult<ClassroomDTO?>(sampleClassroom));

        var controller = GetControllerInstance();

        var result = await controller.DeleteClassroom(1);

        result.Should().BeOfType<ActionResult<ClassroomDTO>>();

        result.Result.Should().BeOfType<OkObjectResult>();

        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        var okResult = result?.Result as OkObjectResult;
        var classroomData = okResult?.Value as ClassroomDTO;
        classroomData.Should().BeEquivalentTo(classroomData);
    }

    [Fact]
    public async Task UpdateClassroom_ShouldReturnOk()
    {
        // Arrange
        var controller = GetControllerInstance();

        ClassroomDTO classroomDTO = new ClassroomDTO
        {
            Id = 1,
            StartDate = new DateOnly(2024, 2, 23),
            CourseId = 1,
            ProgramId = 1,
        };

        var updateClassroomDTO = new UpdateClassroomDTO
        {
            Id = classroomDTO.Id,
            StartDate = new DateOnly(2024, 7, 7),
            CourseId = 2,
            ProgramId = 2,
        };

        _classroomService.UpdateClassroomAsync(updateClassroomDTO)
            .Returns(Task.FromResult(classroomDTO));

        // Act
        var result = await controller.UpdateClassroom(classroomDTO.Id, updateClassroomDTO);

        // Assert
        result.Should().BeOfType<ActionResult<ClassroomDTO>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);
    }
}
