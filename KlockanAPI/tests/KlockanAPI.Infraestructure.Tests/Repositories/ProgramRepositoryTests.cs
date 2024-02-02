using NSubstitute;
using FluentAssertions;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infraestructure.Tests.Repositories;

public class ProgramRepositoryTests
{
    private readonly KlockanContext _context;

    public ProgramRepositoryTests()
    {
        DbContextOptionsBuilder<KlockanContext> dbContextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new KlockanContext(dbContextOptions.Options);
    }
    private ProgramRepository GetRepositoryInstance() => new(_context);

    [Fact]
    public async Task GetAllProgramsAsync_ShouldReturnPrograms()
    {
        // Arrange
        var programs = new List<Program>
        {
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

        _context.Programs.AddRange(programs);
        await _context.SaveChangesAsync();

        var programRepository = GetRepositoryInstance();
        // Act
        var result = await programRepository.GetAllProgramsAsync();

        // Assert
        result.Should().BeEquivalentTo(programs);
        result.Should().HaveCount(programs.Count);
        result.Should().Equal(programs);
        result.First().Name.Should().Be(programs.First().Name);
        result.First().Description.Should().Be(programs.First().Description);
    }

    [Fact]
    public async Task CreateProgramAsync_ShouldAddProgram_WhenCalled()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(databaseName: "ProgramRepositoryDB")
            .Options;

        using (var context = new KlockanContext(options))
        {
            var repository = new ProgramRepository(context);

            var newProgram = new Program
            {
                // Configura las propiedades necesarias de tu modelo
            };

            // Act
            var result = await repository.CreateProgramAsync(newProgram);

            // Assert
            Assert.NotNull(result);
            var programInDb = await context.Programs.FindAsync(result.Id);
            Assert.NotNull(programInDb);
        }
    }

    [Fact]
    public async Task DeleteProgramAsync_ShouldReturnDeletedProgram()
    {
        // Arrange
        var program = new Program
        {
            Id = 1,
            Name = "Frontend Development",
            Description = "Program to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        };

        _context.Programs.Add(program);
        await _context.SaveChangesAsync();

        var repository = new ProgramRepository(_context);

        // Act
        var result = await repository.DeleteProgramAsync(program);

        // Assert
        Assert.Equal(program, result);
    }

}
