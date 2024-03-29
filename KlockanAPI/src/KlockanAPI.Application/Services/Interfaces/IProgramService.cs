﻿using KlockanAPI.Application.DTOs.Program;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IProgramService
{
    Task<IEnumerable<ProgramDTO>> GetAllProgramsAsync();
    Task<ProgramDTO> CreateProgramAsync(CreateProgramDTO createProgramDTO);
    Task<ProgramDTO?> DeleteProgramAsync(int id);
    Task<ProgramDTO> EditProgramAsync(ProgramDTO programDTO);

}
