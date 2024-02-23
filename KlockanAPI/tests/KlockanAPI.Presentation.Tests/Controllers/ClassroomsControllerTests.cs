using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Application.DTOs.Weekday;

using Moq;
using KlockanAPI.Application.DTOs.Program;
using KlockanAPI.Domain.Models;
using FluentAssertions.Common;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class ClassroomsControllerTests
{
    private readonly IClassroomService _classroomService;
    private readonly IScheduleService _scheduleService;

    private readonly Mock<IClassroomService> _mockClassroomService;
    private readonly Mock<IScheduleService> _mockScheduleService;

    private readonly ClassroomsController _controller;

    public ClassroomsControllerTests()
    {
        _classroomService = Substitute.For<IClassroomService>();
        _scheduleService = Substitute.For<IScheduleService>();
    }

    private ClassroomsController GetControllerInstance() => new(_classroomService, _scheduleService);

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

}
