using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.Meeting;
using NSubstitute.ExceptionExtensions;
using System.Net;
using KlockanAPI.Application.Services;
using Microsoft.AspNetCore.Http;
using KlockanAPI.Domain.Models.Webex;

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
    public async Task GetMeetingReport_Return200OK_WithValidInput()
    {
        // Arrange
        MeetingDto meetingDto = new MeetingDto
        {
            Id = 1,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
        };
        MeetingReportDTO meetingReport = new MeetingReportDTO
        {
            items = new List<MeetingParticipantReportDTO>
            {
                new MeetingParticipantReportDTO
                {
                    id = "6aaf4dad853543049f9f47e9ba36d4df_I_285871559454799338_a7f3083c-63f2-31e0-8f25-76a634ce1228",
                    host = false,
                    coHost = false,
                    email = "correo@gmail.com",
                    displayName = "correo@gmail.com",
                    invitee = true,
                    muted = false,
                    state = "end",
                    joinedTime = new DateTime(2024, 02, 19, 10,07,25),
                    leftTime = new DateTime(2024, 02, 19, 10, 40, 00),
                    meetingStartTime = new DateTime(2024, 02, 19, 10 ,06, 20),
                    DurationInMinutes = 32
                }
            }
        };

        _meetingService.GetMeetingReportAsync(meetingDto.Id).Returns(Task.FromResult(meetingReport));        
        var controller = GetControllerInstance();

        //Act
        var result = await controller.GetMeetingReport(meetingDto.Id);
        //Assert
        result.Should().BeOfType<ActionResult<MeetingReport>>();                               
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);

        var meetReport = okResult.Value as MeetingReport;
        meetReport.Should().BeEquivalentTo(meetingReport);
    }    
}
