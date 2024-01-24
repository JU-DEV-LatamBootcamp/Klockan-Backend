using KlockanAPI.Domain.Models;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IProgramRepository
{
    public Task<List<Program>> GetProgramsAsync();
}