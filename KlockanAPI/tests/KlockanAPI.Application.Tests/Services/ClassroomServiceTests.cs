using NSubstitute;
using FluentAssertions;
using KlockanAPI.Application.CrossCutting;
using MapsterMapper;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.Services;
using Moq;
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Application.DTOs.Program;
using FluentAssertions.Common;
using KlockanAPI.Application.DTOs.ClassroomUser;

namespace KlockanAPI.Application.Tests.Services;

public class ClassroomServiceTests
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMeetingRepository _meetingRepository;
    private readonly IClassroomUserRepository _classroomUserRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IClassroomRepository> _classroomRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public ClassroomServiceTests()
    {
        _classroomRepository = Substitute.For<IClassroomRepository>();
        _meetingRepository = Substitute.For<IMeetingRepository>();
        _classroomUserRepository = Substitute.For<IClassroomUserRepository>();

        _mapper = new Mapper();
    }

    public ClassroomService GetServiceInstance() => new(_classroomRepository, _meetingRepository, _classroomUserRepository, _mapper);

    [Fact]
    public async Task GetAllClassroomsAsync_ShouldReturnClassroomDTOs()
    {
        // Arrange
        var classroomservice = GetServiceInstance();

        // Define some sample classrooms from the repository
        List<Classroom> sampleClassrooms = new List<Classroom>
        {
            new Classroom
            {
                Id = 1,
                CourseId = 1,
                ProgramId = 1,
                StartDate = new DateOnly(2024, 2, 23),
                Meetings = [],
                Schedule = [],
                ClassroomUsers = [],
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Classroom
            {
                Id = 2,
                CourseId = 2,
                ProgramId = 1,
                StartDate = new DateOnly(2024, 2, 23),
                Meetings = [],
                Schedule = [],
                ClassroomUsers = [],
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Classroom
            {
                Id = 3,
                CourseId = 1,
                ProgramId = 2,
                StartDate = new DateOnly(2024, 2, 23),
                Meetings = [],
                Schedule = [],
                ClassroomUsers = [],
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        _classroomRepository.GetAllClassroomsAsync().Returns(Task.FromResult<IEnumerable<Classroom>>(sampleClassrooms));

        // Act
        var result = await classroomservice.GetAllClassroomsAsync();

        // Assert
        result.Should().NotBeNull();

        result.Should().BeEquivalentTo(sampleClassrooms.Select(classroom => _mapper.Map<ClassroomDTO>(classroom)));

        result.Should().HaveCount(sampleClassrooms.Count);

        result.Should().ContainItemsAssignableTo<ClassroomDTO>();
    }

    [Fact]
    public async Task CreateClassroomAsync_ShouldReturnClassroomDTO_WhenCreateIsSuccessful()
    {

        //DTO CREATE/CLASSROOM/SCHEDULE
        var shedule1 = new UpdateScheduleDTO
        {
            WeekdayId = 1,
            StartTime = new TimeOnly(18, 00, 00)

        };

        var shedule2 = new UpdateScheduleDTO
        {
            WeekdayId = 1,
            StartTime = new TimeOnly(19, 00, 00)

        };
        List<UpdateScheduleDTO> createClassroomScheduleDTO = [shedule1, shedule2];

        //DTO CREATE/CLASSROOM/

        var createClassroomDTO = new CreateClassroomDTO
        {
            StartDate = new DateOnly(2024, 2, 2),
            ProgramId = 2,
            CourseId = 1,
            Schedule = createClassroomScheduleDTO
        };


        var Classroom = new Classroom
        {
            Id = 1,
            StartDate = new DateOnly(2024, 2, 2),
            ProgramId = 1,
            CourseId = 1,
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)

        };

        var ClassroomDTO = new ClassroomDTO
        {
            Id = 1,
            StartDate = new DateOnly(2024, 2, 2),
            ProgramId = 1,
            CourseId = 1,
        };

        _mapperMock.Setup(m => m.Map<Classroom>(It.IsAny<CreateClassroomDTO>())).Returns(Classroom);
        _classroomRepositoryMock.Setup(repo => repo.CreateClassroomAsync(It.IsAny<Classroom>())).ReturnsAsync(Classroom);
        _mapperMock.Setup(m => m.Map<ClassroomDTO>(It.IsAny<Classroom>())).Returns(ClassroomDTO);

        var service = new ClassroomService(_classroomRepositoryMock.Object, _meetingRepository, _classroomUserRepository, _mapperMock.Object);

        // Act
        var result = await service.CreateClassroomAsync(createClassroomDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ClassroomDTO, result);
        Assert.Equal(ClassroomDTO.StartDate, result.StartDate);
        Assert.Equal(ClassroomDTO.ProgramId, result.ProgramId);
        _classroomRepositoryMock.Verify(repo => repo.CreateClassroomAsync(It.IsAny<Classroom>()), Times.Once);
        _mapperMock.Verify(m => m.Map<ClassroomDTO>(It.IsAny<Classroom>()), Times.Once);
    }

    [Fact]
    public async Task DeleteClassroomAsync_ShouldReturnDeleteClassrooomDto()
    {
        ClassroomService classroomService = GetServiceInstance();
        Classroom sampleClassroom = new Classroom
        {
            Id = 1,
            StartDate = new DateOnly(2024, 1, 23),
            CourseId = 1,
            ProgramId = 1,
        };

        _classroomRepository.GetClassroomByIdAsync(1).Returns(Task.FromResult<Classroom?>(sampleClassroom));
        _meetingRepository.GetMeetingsByClassroomIdAsync(1).Returns(Task.FromResult<IEnumerable<Meeting>?>(null));

        _classroomRepository.DeleteClassroomAsync(sampleClassroom).Returns(Task.FromResult(sampleClassroom));
        var result = await classroomService.DeleteClassroomAsync(1);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.Map<ClassroomDTO>(sampleClassroom));
    }

    [Fact]
    public async Task DeleteClassroomAsync_ShouldThrowNotFoundException_WhenCourseNotFound()
    {
        //Arrange
        ClassroomService classroomService = GetServiceInstance();

        _classroomRepository.GetClassroomByIdAsync(10).Returns(Task.FromResult<Classroom?>(null));

        //Act
        Func<Task> act = async () => await classroomService.DeleteClassroomAsync(10);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Classroom with id 10 not found");
    }

    [Fact]
    public async Task DeleteClassroomAsync_ShouldThrowFoundException_WhenClassroomHasMeetings()
    {
        ClassroomService classroomService = GetServiceInstance();

        Meeting sampleMeeting = new Meeting
        {
            Id = 1,
            ClassroomId = 1,
            Date = new DateOnly(2024, 1, 23),
            SessionNumber = 3
        };

        Classroom sampleClassroom = new Classroom
        {
            Id = 1,
            StartDate = new DateOnly(2024, 1, 23),
            CourseId = 1,
            ProgramId = 1,
        };

        _classroomRepository
            .GetClassroomByIdAsync(1)
            .Returns(Task.FromResult<Classroom?>(sampleClassroom));

        //Act
        Func<Task> act = async () => await classroomService.DeleteClassroomAsync(1);

        //Assert
        await act.Should().ThrowAsync<FoundException>("Classroom 1 has meetings assigned ot it.");
    }

    [Fact]
    public async Task UpdateClassroomAsync_ShouldThrowNotFoundException_WhenClassroomIdIsInvalid()
    {
        // Arrange
        var classroomService = GetServiceInstance();
        var updateClassroomDTO = new UpdateClassroomDTO()
        {
            Id = 1,
            CourseId = 1,
            ProgramId = 1,
            StartDate = new DateOnly(2024, 2, 23),
        };

        // Act
        var act = async () => await classroomService.UpdateClassroomAsync(updateClassroomDTO);

        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Classroom with id {updateClassroomDTO.Id} not found");
    }

    [Fact]
    public async Task UpdateClassroomAsync_ShouldUpdateClassroom_WhenClassroomIdIsValid()
    {
        // Arrange
        var classroomService = GetServiceInstance();

        var classroom = new Classroom
        {
            Id = 1,
            CourseId = 1,
            ProgramId = 1,
            StartDate = new DateOnly(2024, 1, 1)
        };

        var updateClassroomDTO = new UpdateClassroomDTO()
        {
            Id = 1,
            CourseId = 1,
            ProgramId = 1,
            StartDate = new DateOnly(2024, 7, 7)
        };

        _classroomRepository.GetClassroomByIdAsync(classroom.Id).Returns(
            Task.FromResult<Classroom?>(classroom)
        );
        _classroomRepository.UpdateClassroomAsync(Arg.Any<Classroom>()).Returns(
            Task.FromResult(_mapper.Map<Classroom>(updateClassroomDTO))
        );

        // Act
        var result = await classroomService.UpdateClassroomAsync(updateClassroomDTO);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(
            _mapper.Map<ClassroomDTO>(_mapper.Map<ClassroomDTO>(updateClassroomDTO))
        );
    }

    [Fact]
    public async Task UpdateClassroomUsersAsync_ShouldThrowNotFoundException_WhenClassroomIdIsInvalid()
    {
        // Arrange
        var classroomService = GetServiceInstance();
        var updateClassroomUsersDTO = new UpdateClassroomUsersDTO();

        // Act
        var act = async () => await classroomService.UpdateClassroomUsersAsync(updateClassroomUsersDTO);

        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Classroom with id {updateClassroomUsersDTO.Id} not found");
    }

    [Fact]
    public async Task UpdateClassroomUsersAsync_ShouldUpdateUserClassroom_WhenClassroomIdIsValid()
    {
        // Arrange
        var classroomService = GetServiceInstance();

        var classroom = new Classroom() { Id = 1 };

        var updateClassroomUsersDTO = new UpdateClassroomUsersDTO()
        {
            Id = classroom.Id,
            Users = new List<UpdateClassroomUserDTO>() {
                new UpdateClassroomUserDTO()
            }
        };

        var classroomUsers = _mapper.Map<List<ClassroomUser>>(updateClassroomUsersDTO);

        _classroomRepository.GetClassroomByIdAsync(classroom.Id).Returns(
            Task.FromResult<Classroom?>(classroom)
        );
        _classroomUserRepository.UpdateClassroomUsersAsync(classroom.Id, classroomUsers).Returns(classroomUsers);

        // Act
        var result = await classroomService.UpdateClassroomUsersAsync(updateClassroomUsersDTO);

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(classroomUsers.Count);
        result.Should().BeEquivalentTo(
            _mapper.Map<List<ClassroomUserDTO>>(classroomUsers)
        );
    }
}

