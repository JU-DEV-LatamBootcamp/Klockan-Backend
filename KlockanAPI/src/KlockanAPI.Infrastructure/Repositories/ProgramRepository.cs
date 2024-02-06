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

    public async Task<Program> CreateProgramAsync(Program program)
    {
        await _context.Programs.AddAsync(program);
        await _context.SaveChangesAsync();
        return program;
    }

    public async Task<Program> DeleteProgramAsync(Program program)
    {
        _context.Programs.Remove(program);
        await _context.SaveChangesAsync();

        return program;
    }

    public async Task<Program?> GetProgramByIdAsync(int id)
    {
        return await _context.Programs.FindAsync(id);
    }

    public async Task<Program> EditProgramAsync(Program program)
    {
        var editedProgram = await _context.Programs.FindAsync(program.Id);         
        _context.Programs.Entry(editedProgram!).CurrentValues.SetValues(program);
        await _context.SaveChangesAsync();
        return program;
    }

}
