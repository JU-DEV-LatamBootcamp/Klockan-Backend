using KlockanAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories.Interfaces;

namespace KlockanAPI.Infrastructure.Repositories;

public class ProgramRepository : IProgramRepository
{
    private readonly KlockanContext _context;

    public ProgramRepository(KlockanContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Program>> GetAllProgramsAsync()
    {
        return await Task.FromResult(_context.Programs.ToList());
    }
}
