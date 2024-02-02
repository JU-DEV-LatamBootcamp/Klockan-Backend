using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IProgramRepository
{
    Task<IEnumerable<Program>> GetAllProgramsAsync();
    Task<Program> CreateProgramAsync(Program program);
    Task<Program?> GetProgramByIdAsync(int id);
    Task<Program> DeleteProgramAsync(Program program);
}
