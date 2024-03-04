using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.Classroom;
using Moq;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class SchedulesControllerTests
{
    private readonly IScheduleService _scheduleService;

    public SchedulesControllerTests()
    {
        _scheduleService = Substitute.For<IScheduleService>();
    }

    private SchedulesController GetControllerInstance() => new(_scheduleService);

    [Fact]
    public async Task GetClassroomScheduless_ShouldReturnOk()
    {
        // Arrange
        var schedulesController = GetControllerInstance();

        List<ScheduleDTO> schedules = new List<ScheduleDTO>
        {
            new ScheduleDTO() { Id = 1, ClassroomId = 1, WeekdayId = 1 },
            new ScheduleDTO() { Id = 2, ClassroomId = 1, WeekdayId = 1 },
            new ScheduleDTO() { Id = 3, ClassroomId = 1, WeekdayId = 1 },
            new ScheduleDTO() { Id = 4, ClassroomId = 1, WeekdayId = 1 },
        };
        _scheduleService.GetSchedulesByClassroomIdAsync(1).Returns(schedules);

        // Act
        var result = await schedulesController.GetClassroomSchedules(1);

        // Assert
        result.Should().BeOfType<ActionResult<List<ScheduleDTO>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);
    }
}
