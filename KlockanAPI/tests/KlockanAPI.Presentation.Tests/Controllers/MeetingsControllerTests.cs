using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.Meeting;
using NSubstitute.ExceptionExtensions;
using System.Net;
using Microsoft.AspNetCore.Http;

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

    [Fact]
    public async Task CreateMeeting_Return201Created_WithValidInput()
    {
        //Arrange
        var createMeetingDto = new CreateMultipleMeetingsDto
        {
            Date = new DateOnly(2024, 2, 22),
            Time = new TimeOnly(14, 0, 0),
            ClassroomId = 1,
            TrainerId = 1,
            Users = new List<int> { 1, 2, 3 }
        };
        var createdMeetingDto = new MeetingDto
        {
            Id = 1,
            SessionNumber = 1,
            Date = createMeetingDto.Date,
            Time = createMeetingDto.Time,
            ClassroomId = createMeetingDto.ClassroomId
        };

        _meetingService.CreateSingleMeeting(createMeetingDto).Returns(Task.FromResult(createdMeetingDto));
        var controller = GetControllerInstance();

        //Act
        var result = await controller.CreateMeeting(createMeetingDto);
        //Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var model = Assert.IsType<MeetingDto>(createdAtActionResult.Value);
        Assert.Equal(createdMeetingDto, model);
    }

    [Fact]
    public async Task CreateMeeting_Returns400BadRequest_WithInvalidInput()
    {
        // Arrange
        var controller = GetControllerInstance();
        var createMeetingDto = new CreateMultipleMeetingsDto
        {
            Date = new DateOnly(2024, 2, 22),
            Time = new TimeOnly(14, 0, 0),
        };

        controller.ModelState.AddModelError("PropertyName", "Error message");

        // Act
        var result = await controller.CreateMeeting(createMeetingDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateMeeting_ShouldReturnOk_WhenMeetingIsUpdated()
    {
        // Arrange
        var meetingService = Substitute.For<IMeetingService>();

        var meetingId = 1;
        var updatedMeetingDto = new UpdateMeetingDto
        {
            Date = new DateOnly(2024, 2, 22),
            Time = new TimeOnly(14, 0, 0)
        };

        var updatedMeeting = new MeetingDto
        {
            Id = meetingId,
            SessionNumber = 2,
            ClassroomId = 3,
            Date = updatedMeetingDto.Date,
            Time = updatedMeetingDto.Time
        };

        meetingService.UpdateMeeting(Arg.Any<UpdateMeetingDto>(), meetingId).Returns(Task.FromResult(updatedMeeting));

        var controller = new MeetingsController(meetingService);

        // Act
        var result = await controller.UpdateMeeting(updatedMeetingDto, meetingId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ActionResult<MeetingDto>>();

        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var meetingDto = okResult.Value as MeetingDto;
        meetingDto.Should().NotBeNull();
        meetingDto.Id.Should().Be(updatedMeeting.Id);
        meetingDto.SessionNumber.Should().Be(updatedMeeting.SessionNumber);
        meetingDto.Date.Should().Be(updatedMeetingDto.Date);
        meetingDto.Time.Should().Be(updatedMeetingDto.Time);
        meetingDto.ClassroomId.Should().Be(updatedMeeting.ClassroomId);
    }
}
