using NSubstitute;
using FluentAssertions;
using KlockanAPI.Application.CrossCutting;
using MapsterMapper;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.Services;
using Moq;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Application.DTOs.Program;
using FluentAssertions.Common;
using KlockanAPI.Infrastructure.Repositories;

namespace KlockanAPI.Application.Tests.Services;

public class ScheduleServiceTests
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IScheduleRepository> _scheduleRepositoryMock = new();
    private readonly Mock<IClassroomRepository> _classroomRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public ScheduleServiceTests()
    {
        _scheduleRepository = Substitute.For<IScheduleRepository>();

        _mapper = new Mapper();
    }

    public ScheduleService GetServiceInstance() => new(_scheduleRepository, _classroomRepository, _mapper);

    [Fact]
    public async Task GetAllSchedulesAsync_ShouldReturnScheduleDTOs()
    {
        // Arrange
        var scheduleservice = GetServiceInstance();

        // Define some sample schedules from the repository
        List<Schedule> sampleSchedules = new List<Schedule>
        {
            new Schedule
            {
                Id = 1,
                WeekdayId = 1,
                ClassroomId = 1,
                StartTime = new TimeOnly(18,00)
            },
            new Schedule
            {
                Id = 2,
                WeekdayId = 2,
                ClassroomId = 2,
                StartTime = new TimeOnly(19,00)
            },
            new Schedule
            {
                Id = 3,
                WeekdayId = 3,
                ClassroomId = 3,
                StartTime = new TimeOnly(20,00)
            }
        };

        _scheduleRepository.GetAllSchedulesAsync().Returns(Task.FromResult<IEnumerable<Schedule>>(sampleSchedules));

        // Act
        var result = await scheduleservice.GetAllSchedulesAsync();

        // Assert
        result.Should().NotBeNull();

        result.Should().BeEquivalentTo(sampleSchedules.Select(schedule => _mapper.Map<ScheduleDTO>(schedule)));

        result.Should().HaveCount(sampleSchedules.Count);

        result.Should().ContainItemsAssignableTo<ScheduleDTO>();
    }

    [Fact]
    public async Task CreateScheduleAsync_ShouldReturnScheduleDTO_WhenCreateIsSuccessful()
    {



      

        var createScheduleDTO = new CreateScheduleDTO(3, 3, new TimeOnly(20, 00)){};


        var Schedule = new Schedule
        {
            Id = 3,
            WeekdayId = 3,
            ClassroomId = 3,
            StartTime = new TimeOnly(20, 00),
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)


        };

        var ScheduleDTO = new ScheduleDTO
        {
            Id = 3,
            WeekdayId = 3,
            ClassroomId = 3,
            StartTime = new TimeOnly(20, 00)
        };

        _mapperMock.Setup(m => m.Map<Schedule>(It.IsAny<CreateScheduleDTO>())).Returns(Schedule);
        _scheduleRepositoryMock.Setup(repo => repo.CreateScheduleAsync(It.IsAny<Schedule>())).ReturnsAsync(Schedule);
        _mapperMock.Setup(m => m.Map<ScheduleDTO>(It.IsAny<Schedule>())).Returns(ScheduleDTO);

        var service = new ScheduleService(_scheduleRepositoryMock.Object, _classroomRepository, _mapperMock.Object );

        // Act
        var result = await service.CreateScheduleAsync(createScheduleDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ScheduleDTO, result);
        Assert.Equal(ScheduleDTO.WeekdayId, result.WeekdayId);
        Assert.Equal(ScheduleDTO.ClassroomId, result.ClassroomId);
        Assert.Equal(ScheduleDTO.StartTime, result.StartTime);
        _scheduleRepositoryMock.Verify(repo => repo.CreateScheduleAsync(It.IsAny<Schedule>()), Times.Once);
        _mapperMock.Verify(m => m.Map<ScheduleDTO>(It.IsAny<Schedule>()), Times.Once);
    }
    /*   Template for delete schedule test
    [Fact]
    public async Task DeleteScheduleAsync_ShouldReturnDeleteClassrooomDto()
    {
        ScheduleService scheduleService = GetServiceInstance();
        Schedule sampleSchedule = new Schedule
        {
            Id = 3,
            WeekdayId = 3,
            ClassroomId = 3,
            StartTime = new TimeOnly(20, 00);
        };

        _scheduleRepository.GetScheduleByIdAsync(1).Returns(Task.FromResult<Schedule?>(sampleSchedule));

        _scheduleRepository.DeleteScheduleAsync(sampleSchedule).Returns(Task.FromResult(sampleSchedule));
        var result = await scheduleService.DeleteScheduleAsync(1);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.Map<ScheduleDTO>(sampleSchedule));
    }

    [Fact]
    public async Task DeleteScheduleAsync_ShouldThrowNotFoundException_WhenCourseNotFound()
    {
        //Arrange
        ScheduleService scheduleService = GetServiceInstance();

        _scheduleRepository.GetScheduleByIdAsync(10).Returns(Task.FromResult<Schedule?>(null));

        //Act
        Func<Task> act = async () => await scheduleService.DeleteScheduleAsync(10);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Schedule with id 10 not found");
    }

    [Fact]
    public async Task DeleteScheduleAsync_ShouldThrowFoundException_WhenScheduleHasMeetings()
    {
        ScheduleService scheduleService = GetServiceInstance();

        Meeting sampleMeeting = new Meeting
        {
            Id = 1,
            ScheduleId = 1,
            Date = new DateOnly(2024, 1, 23),
            SessionNumber = 3
        };

        Schedule sampleSchedule = new Schedule
        {
            Id = 1,
            StartDate = new DateOnly(2024, 1, 23),
            CourseId = 1,
            ProgramId = 1,
        };

        _scheduleRepository
            .GetScheduleByIdAsync(1)
            .Returns(Task.FromResult<Schedule?>(sampleSchedule));

        //Act
        Func<Task> act = async () => await scheduleService.DeleteScheduleAsync(1);

        //Assert
        await act.Should().ThrowAsync<FoundException>("Schedule 1 has meetings assigned ot it.");
    }
    */
}

