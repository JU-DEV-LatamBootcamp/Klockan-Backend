using Moq;
using NSubstitute;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.DTOs.Program;
using KlockanAPI.Application.Services;

namespace KlockanAPI.Application.Tests.Services;

public class ProgramServiceTests
{

    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;

    private readonly Mock<IProgramRepository> _programRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public ProgramServiceTests()
    {
        _programRepository = Substitute.For<IProgramRepository>();
        _mapper = new Mapper();
    }

    private ProgramService GetServiceInstance() => new(_programRepository, _mapper);

    [Fact]
    public async Task GetAllProgramsAsync_ShouldReturnProgramDTOs()
    {
        // Arrange
        var programService = GetServiceInstance();

        // Define some sample programs from the repository
        List<Program> samplePrograms = new List<Program>{
            new Program
            {
                Id = 1,
                Name = "Bootcamp Developers 01",
                Description = "Program covering concepts in software development.",
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Program
            {
                Id = 2,
                Name = "Advanced Bootcamp Developers 01",
                Description = "Program focused on advanced software design and development techniques.",
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Program
            {
                Id = 3,
                Name = "Bootcamp Data Science and Analytics 01",
                Description = "Program designed to teach the fundamentals of data analysis, machine learning, and statistical modeling.",
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Program
            {
                Id = 4,
                Name = "Bootcamp Developers 02",
                Description = "Program covering concepts in software development.",
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            }
        };


        _programRepository.GetAllProgramsAsync().Returns(Task.FromResult<IEnumerable<Program>>(samplePrograms));

        // Act
        var result = await programService.GetAllProgramsAsync();

        // Assert
        result.Should().NotBeNull(); // Ensure the result is not null

        // Use BeEquivalentTo to compare elements and properties without requiring exact type matching
        result.Should().BeEquivalentTo(samplePrograms.Select(program => _mapper.Map<ProgramDTO>(program)));

        // Check if the number of items in the result matches the number of items in the samplePrograms
        result.Should().HaveCount(samplePrograms.Count);

        // Ensure that each item in the result is of the expected type ProgramDTO
        result.Should().ContainItemsAssignableTo<ProgramDTO>();
    }

    [Fact]
    public async Task CreateProgramAsync_ShouldReturnProgramDTO_WhenCreateIsSuccessful()
    {
        // Arrange
        var createProgramDto = new CreateProgramDTO 
        {
            Name = "Create Program DTO Test",
            Description = "Create Program DTO Test Description.",
        };

        var program = new Program
        {
            Id = 1,
            Name = "Program Test",
            Description = "Program Test Description.",
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        };

        var programDTO = new ProgramDTO
        {
            Id = 2,
            Name = "Program DTO Test",
            Description = "Program DTO Test Description.",
        };

        _mapperMock.Setup(m => m.Map<Program>(It.IsAny<CreateProgramDTO>())).Returns(program);
        _programRepositoryMock.Setup(repo => repo.CreateProgramAsync(It.IsAny<Program>())).ReturnsAsync(program);
        _mapperMock.Setup(m => m.Map<ProgramDTO>(It.IsAny<Program>())).Returns(programDTO);

        var service = new ProgramService(_programRepositoryMock.Object, _mapperMock.Object);

        // Act
        var result = await service.CreateProgramAsync(createProgramDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(programDTO, result);
        Assert.Equal(programDTO.Name, result.Name);
        Assert.Equal(programDTO.Description, result.Description);
        _programRepositoryMock.Verify(repo => repo.CreateProgramAsync(It.IsAny<Program>()), Times.Once);
        _mapperMock.Verify(m => m.Map<ProgramDTO>(It.IsAny<Program>()), Times.Once);
    }
}
