using KlockanAPI.Domain.Models;
using KlockanAPI.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using KlockanAPI.Infrastructure.Data;

namespace KlockanAPI.Infrastructure.Repositories;
public class ProgramRepository : IProgramRepository
{
    private readonly KlockanContext _context;

    public ProgramRepository(KlockanContext context)
    {
        _context = context;
    }

    public async Task<List<Program>> GetProgramsAsync()
    {
        return await Task.FromResult(_context.Programs.ToList());

        // eliminar lo de aca abajo
        var programs = new List<Program>()
        {
            new Program
            {
                Id = 1,
                Name = "Bootcamp Developers 01",
                Description = "Program covering concepts in software development.",
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
            new Program
            {
                Id = 2,
                Name = "Advanced Bootcamp Developers 01",
                Description = "Program focused on advanced software design and development techniques.",
                CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
            },
        };

        return await Task.FromResult(programs.ToList());
    }
}
