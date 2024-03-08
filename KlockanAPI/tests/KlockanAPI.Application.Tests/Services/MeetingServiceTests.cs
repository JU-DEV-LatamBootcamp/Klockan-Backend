using NSubstitute;
using FluentAssertions;
using MapsterMapper;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Application.Services;
using KlockanAPI.Application.DTOs.Meeting;
using KlockanAPI.Infrastructure.Repositories;
using KlockanAPI.Application.Services.Interfaces;

namespace KlockanAPI.Application.Tests.Services;


public class MeetingServiceTest
{
    private readonly IMeetingRepository _meetingRespository;
    private readonly IThirdPartyMeeting _thirdPartyMeeting;
    private readonly IMapper _mapper;
    private readonly IMeetingAttendancesRepository _meetingAttendancesRepository;

    public MeetingServiceTest()
    {
        _meetingRespository = Substitute.For<IMeetingRepository>();
        _thirdPartyMeeting = Substitute.For<IThirdPartyMeeting>();
        _mapper = new Mapper();
        _meetingAttendancesRepository = Substitute.For<IMeetingAttendancesRepository>();
    }

    private MeetingService GetServiceInstance() => new(_meetingRespository, _mapper, _thirdPartyMeeting, _meetingAttendancesRepository);


    [Fact]
    public async Task GetAllMeetingsAsync_ShouldReturnMeetingDto()
    {
        // Arrange
        var meetingService = GetServiceInstance();

        List<Meeting> sampleMeetings = new List<Meeting>
        {
            new Meeting
            {
            Id = 1,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Meeting
            {
            Id = 2,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Meeting
            {
            Id = 3,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Meeting
            {
            Id = 4,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
        };

        _meetingRespository.GetAllAsync().Returns(Task.FromResult<IEnumerable<Meeting>>(sampleMeetings));

        var result = await meetingService.GetAllMeetingsAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(sampleMeetings.Select(meeting => _mapper.Map<MeetingDto>(meeting)));
        result.Should().HaveCount(sampleMeetings.Count);
        result.Should().ContainItemsAssignableTo<MeetingDto>();
    }

    [Fact]
    public async Task CreateSingleMeeting_ReturnsMeetingDto_WithValidInput()
    {
        // Arrange
        var meetingService = GetServiceInstance();

        var createMeetingDto = new CreateMultipleMeetingsDto
        {
            Date = new DateOnly(2024, 2, 22),
            Time = new TimeOnly(14, 0, 0),
            ClassroomId = 1,
            TrainerId = 1,
            Users = new List<int> { 1, 2, 3 }
        };

        var expectedMeeting = new MeetingDto
        {
            Id = 1,
            SessionNumber = 1,
            Date = createMeetingDto.Date,
            Time = createMeetingDto.Time,
            ClassroomId = createMeetingDto.ClassroomId
        };

        var createdMeeting = new Meeting
        {
            Id = expectedMeeting.Id,
            SessionNumber = expectedMeeting.SessionNumber,
            Date = new DateOnly(createMeetingDto.Date.Year, createMeetingDto.Date.Month, createMeetingDto.Date.Day),
            Time = new TimeOnly(createMeetingDto.Time.Hour, createMeetingDto.Time.Minute, createMeetingDto.Time.Second),
            ClassroomId = expectedMeeting.ClassroomId
        };

        _meetingRespository.CreateSingleMeeting(Arg.Any<Meeting>()).Returns(createdMeeting);

        // Act
        var result = await meetingService.CreateSingleMeeting(createMeetingDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedMeeting);
    }

    [Fact]
    public async Task UpdateMeeting_ShouldReturnUpdatedMeetingDto()
    {
        // Arrange
        var meetingService = GetServiceInstance();

        var initialMeeting = new Meeting
        {
            Id = 1,
            Date = new DateOnly(2024, 2, 22),
            Time = new TimeOnly(14, 0, 0)
        };

        _meetingRespository.GetMeetingById(1).Returns(Task.FromResult(initialMeeting));

        var updatedMeetingDto = new UpdateMeetingDto
        {
            Date = new DateOnly(2024, 2, 23),
            Time = new TimeOnly(15, 0, 0)
        };

        var updatedMeeting = new Meeting
        {
            Id = 1,
            Date = updatedMeetingDto.Date,
            Time = updatedMeetingDto.Time
        };

        _meetingRespository.UpdateMeeting(Arg.Any<Meeting>(), 1).Returns(Task.FromResult(updatedMeeting));

        // Act
        var result = await meetingService.UpdateMeeting(updatedMeetingDto, 1);

        // Assert
        result.Should().NotBeNull();
        result.Date.Should().Be(updatedMeetingDto.Date);
        result.Time.Should().Be(updatedMeetingDto.Time);
    }

    [Fact]
    public async Task GetMeetingReportAsync_ShouldReturnMeetingReport()
    {
        // Arrange
        var meetingService = GetServiceInstance();        
        int meetingId = 1;
        Meeting meeting = new Meeting
        {
            Id = 1,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30),
            ThirdPartyId = "123nonnullvalue"
        };
        Domain.Models.Webex.MeetingReport meetingReport = new Domain.Models.Webex.MeetingReport
        {
            items = new List<Domain.Models.Webex.ParticipantReport>
            {
                new Domain.Models.Webex.ParticipantReport
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

        _meetingRespository.GetMeetingByIdAsync(meetingId).Returns(meeting);
        _thirdPartyMeeting.GetMeetingReportAsync(meeting.ThirdPartyId).Returns(meetingReport);
        //Act
        var meetReportResult = await meetingService.GetMeetingReportAsync(meeting.Id);
        //Assert
        meetReportResult.Should().NotBeNull();
        meetReportResult.Should().BeAssignableTo<MeetingReportDTO>();        
    }
}
