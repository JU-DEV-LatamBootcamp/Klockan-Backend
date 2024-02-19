using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.Meeting;

namespace KlockanAPI.Presentation.Tests.Controllers;


public class MeetingsControllerTest
{
    private readonly IMeetingService _meetingService;

    public MeetingsControllerTest()
    {
        _meetingService = Substitute.For<IMeetingService>();
    }

    private MeetingsController GetControllerInstance() => new(_meetingService);

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        // Arrange
        var sampleMeetings = new List<MeetingDto>{
            new MeetingDto
            {
            Id = 1,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            },
            new MeetingDto
            {
            Id = 2,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            },
            new MeetingDto
            {
            Id = 3,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            },
            new MeetingDto
            {
            Id = 4,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            },
        };

        _meetingService.GetAllMeetingsAsync().Returns(Task.FromResult<IEnumerable<MeetingDto>>(sampleMeetings));
        var controller = GetControllerInstance();

        // Act
        var result = await controller.GetAllMeetings();

        // Assert
        result.Should().BeOfType<ActionResult<IEnumerable<MeetingDto>>>();

        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);

        var meetingsData = okResult.Value as IEnumerable<MeetingDto>;
        meetingsData.Should().BeEquivalentTo(sampleMeetings);
    }
}