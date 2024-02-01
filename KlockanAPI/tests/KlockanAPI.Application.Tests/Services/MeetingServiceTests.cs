using NSubstitute;
using FluentAssertions;
using MapsterMapper;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Application.Services;
using KlockanAPI.Application.DTOs.Meeting;

namespace KlockanAPI.Application.Tests.Services;


public class MeetingServiceTest
{
    private readonly IMeetingRepository _meetingRespository;
    private readonly IMapper _mapper;

    public MeetingServiceTest()
    {
        _meetingRespository = Substitute.For<IMeetingRepository>();
        _mapper = new Mapper();
    }

    private MeetingService GetServiceInstance() => new(_meetingRespository, _mapper);


    [Fact]
    public async Task GetAllMeetingsAsync_ShouldReturnMeetingDto()
    {
        // Arrange
        var meetingService = GetServiceInstance();

        List<Meeting> sampleMeetings = new List<Meeting>
        {
            new Meeting{
            Id = 1,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Meeting{
            Id = 2,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Meeting{
            Id = 3,
            SessionNumber = 3,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            Time = new TimeOnly(15, 30, 0),
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Meeting{
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
}