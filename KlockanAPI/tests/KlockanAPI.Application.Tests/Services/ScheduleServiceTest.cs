using FluentAssertions;
using KlockanAPI.Application.CrossCutting;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Application.Services;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using MapsterMapper;
using NSubstitute;

public class ScheduleServiceTests
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;

    public ScheduleServiceTests()
    {
        _classroomRepository = Substitute.For<IClassroomRepository>();
        _scheduleRepository = Substitute.For<IScheduleRepository>();
        _mapper = new Mapper();
    }


    public ScheduleService GetServiceInstance() => new(_scheduleRepository, _classroomRepository, _mapper);

    [Fact]
    public async Task GetSchedulesByClassroomIdAsync_ShouldThrowNotFoundError_WhenClassroomIdIsInvalid()
    {
        // Arrage
        var classroomId = 5;
        var scheduleService = GetServiceInstance();

        // Act
        var act = async () => await scheduleService.GetSchedulesByClassroomIdAsync(classroomId);

        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Classroom with id {classroomId} not found");
    }

    [Fact]
    public async Task GetSchedulesByClassroomIdAsync_ShouldReturnScheduleDTOs()
    {
        // Arrage
        var scheduleService = GetServiceInstance();

        var classroom = new Classroom() { Id = 5 };
        var schedulesSample = new List<Schedule>() {
            new Schedule()
            {
                Id = 1, ClassroomId = classroom.Id, WeekdayId = 1, StartTime = new TimeOnly(16, 1, 0)
            },
            new Schedule()
            {
                Id = 2, ClassroomId = classroom.Id, WeekdayId = 2, StartTime = new TimeOnly(16, 1, 0)
            },
        };

        _classroomRepository.GetClassroomByIdAsync(classroom.Id).Returns(Task.FromResult<Classroom?>(classroom));
        _scheduleRepository.GetSchedulesByClassroomIdAsync(classroom.Id)
            .Returns(Task.FromResult<IEnumerable<Schedule>>(schedulesSample));

        // Act
        var result = await scheduleService.GetSchedulesByClassroomIdAsync(classroom.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(schedulesSample.Count);
        result.Should().BeEquivalentTo(_mapper.Map<List<ScheduleDTO>>(schedulesSample));
    }
}
