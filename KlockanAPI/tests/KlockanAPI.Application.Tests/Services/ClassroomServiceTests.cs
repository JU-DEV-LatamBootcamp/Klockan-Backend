﻿using NSubstitute;
using FluentAssertions;
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
    private readonly IMapper _mapper;
    private readonly Mock<ICourseRepository> _courseRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public ClassroomServiceTests()
    {
        _classroomRepository = Substitute.For<IClassroomRepository>();
        _mapper = new Mapper();
    }

    public ClassroomService GetServiceInstance() => new(_classroomRepository, _mapper);

    [Fact]
    public async Task GetAllClassroomsAsync_ShouldReturnClassroomDTOs()
    {
        // Arrange
        var classroomservice = GetServiceInstance();

        // Define some sample classrooms from the repository
        List<Classroom> sampleClassrooms = new List<Classroom>{
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
            // Arrange
            var createClassroomDTO = new CreateClassroomDTO
            {
                CourseId = 1,
                ProgramId = 1,
                StartDate = new DateOnly(2024, 2, 23),
            };

            var classroom = new Classroom
            {
                CourseId = 2,
                ProgramId = 1,
                StartDate = new DateOnly(2024, 2, 23),
            };

            var classroomDTO = new ClassroomDTO
            {
                CourseId = 1,
                ProgramId = 2,
                StartDate = new DateOnly(2024, 2, 23),
            };

        _mapperMock.Setup(m => m.Map<Classroom>(It.IsAny<CreateClassroomDTO>())).Returns(classroom);
        _classroomRepositoryMock.Setup(repo => repo.CreateClassroomAsync(It.IsAny<Classroom>())).ReturnsAsync(classroom);
        _mapperMock.Setup(m => m.Map<ClassroomDTO>(It.IsAny<Classroom>())).Returns(classroomDTO);

        var service = new ClassroomService(_classroomRepositoryMock.Object, _classroomRepository, _mapperMock.Object);

        // Act
        var result = await service.CreateClassroomAsync(createClassroomDTO);


        // Assert
        Assert.NotNull(result);
            Assert.Equal(classroomDTO, result);
            _classroomRepositoryMock.Verify(repo => repo.CreateClassroomAsync(It.IsAny<Classroom>()), Times.Once);
            _mapperMock.Verify(m => m.Map<ClassroomDTO>(It.IsAny<Classroom>()), Times.Once);
        }
    }
}

