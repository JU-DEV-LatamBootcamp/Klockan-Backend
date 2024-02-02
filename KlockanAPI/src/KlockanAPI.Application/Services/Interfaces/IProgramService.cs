using KlockanAPI.Application.DTOs.Program;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IProgramService
{
    Task<IEnumerable<ProgramDTO>> GetAllProgramsAsync();
    Task<ProgramDTO> CreateProgramAsync(CreateProgramDTO createProgramDTO);
}
