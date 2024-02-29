using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Application.DTOs.Weekday;

using Moq;
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Domain.Models;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using KlockanAPI.Application.DTOs.Program;

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
    public async Task CreateClassroom_Returns201Created_WithValidInput()
    {
        // Arrange

        //DTO CREATE/CLASSROOM/SCHEDULE
        CreateClassroomScheduleDTO schedule1 = new CreateClassroomScheduleDTO
        {
            WeekdayId = 1,
            StartTime = new TimeOnly(18, 00, 00)
        };

        CreateClassroomScheduleDTO schedule2 = new CreateClassroomScheduleDTO
        {
            WeekdayId = 1,
            StartTime = new TimeOnly(19, 00, 00)
        };

        List<CreateClassroomScheduleDTO> createClassroomScheduleDTO = new List<CreateClassroomScheduleDTO> { schedule1, schedule2 };

        var createClassroomDTO = new CreateClassroomDTO
        {
            CourseId = 1,
            ProgramId = 1,
            StartDate = new DateOnly(2024, 2, 23),
            Schedule = createClassroomScheduleDTO
        };

        var createdClassrrom = new ClassroomDTO();

        // Mock the _classroomService and _scheduleService
        var mockClassroomService = new Mock<IClassroomService>();
        var mockScheduleService = new Mock<IScheduleService>();

        // Set up the expected behavior of the mockClassroomService and mockScheduleService.
        mockClassroomService
            .Setup(m => m.CreateClassroomAsync(It.IsAny<CreateClassroomDTO>()))
            .ReturnsAsync(createdClassrrom);



        // Create an instance of the controller with the mocked services.
        var controller = new ClassroomsController(mockClassroomService.Object, mockScheduleService.Object);

        // Act
        var result = await controller.CreateClassroom(createClassroomDTO);

        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, actionResult.StatusCode);

        // Check if the value is of the expected type
        if (actionResult.Value is ClassroomDTO createdClassroomDTO)
        {
            // Additional assertions on createdClassroomDTO
            Assert.NotNull(createdClassroomDTO);
            // ...
        }
        else
        {
            // Fail the test if the value is not of the expected type
            Assert.True(false, "Unexpected type returned from the action.");
        }

    }

}
