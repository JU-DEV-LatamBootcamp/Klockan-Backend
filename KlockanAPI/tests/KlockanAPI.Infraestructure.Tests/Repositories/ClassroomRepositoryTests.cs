using Microsoft.EntityFrameworkCore;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;

namespace KlockanAPI.Infraestructure.Tests.Repositories;

public class ClassroomRepositoryTests : IDisposable
{
    private readonly DbContextOptions<KlockanContext> _options;

    public ClassroomRepositoryTests()
    {
        //Configure the options of the context of the in memory database
        _options = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetClassroomsByCourseIdAsync_ShouldReturnClassroomsByCourseId()
    {
        // Arrange

        using var context = new KlockanContext(_options);

        var classrooms = new List<Classroom>
        {
            new Classroom { Id = 1, StartDate = new DateOnly(2024, 1, 23), CourseId = 1, ProgramId = 1 },
            new Classroom { Id = 2, StartDate = new DateOnly(2024, 1, 30), CourseId = 1, ProgramId = 1 },
            new Classroom { Id = 3, StartDate = new DateOnly(2024, 2, 6), CourseId = 2, ProgramId = 1 }
        };

        context.Classrooms.AddRange(classrooms);
        await context.SaveChangesAsync();

        var repository = new ClassroomRepository(context);

        // Act
        var result = await repository.GetClassroomsByCourseIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result!.Count());
    }

    // Implement IDisposable to destroy the context after each test case
    public void Dispose()
    {
        using var context = new KlockanContext(_options);
        context.Database.EnsureDeleted(); // Asegurarse de que la base de datos en memoria se elimine al finalizar todas las pruebas
    }

}
