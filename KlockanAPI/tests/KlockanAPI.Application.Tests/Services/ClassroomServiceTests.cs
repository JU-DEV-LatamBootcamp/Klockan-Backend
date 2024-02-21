using NSubstitute;
using FluentAssertions;
using KlockanAPI.Application.CrossCutting;
using MapsterMapper;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.Services;
using Moq;
using KlockanAPI.Application.DTOs.Classroom;

namespace KlockanAPI.Application.Tests.Services;

public class ClassroomServiceTests
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IProgramRepository _programRepository;
    private readonly IMeetingRepository _meetingRepository;
    private readonly IMapper _mapper;
    private readonly Mock<ICourseRepository> _courseRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public ClassroomServiceTests()
    {
        _classroomRepository = Substitute.For<IClassroomRepository>();
        _courseRepository = Substitute.For<ICourseRepository>();
        _programRepository = Substitute.For<IProgramRepository>();
        _meetingRepository = Substitute.For<IMeetingRepository>();
        _mapper = new Mapper();
    }

    private ClassroomService GetServiceInstance() => new(_classroomRepository, _mapper,_meetingRepository);

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
}