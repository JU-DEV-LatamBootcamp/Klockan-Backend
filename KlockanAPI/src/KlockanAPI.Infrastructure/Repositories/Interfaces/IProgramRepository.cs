using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IProgramRepository
{
    public Task<List<Program>> GetProgramsAsync();
}