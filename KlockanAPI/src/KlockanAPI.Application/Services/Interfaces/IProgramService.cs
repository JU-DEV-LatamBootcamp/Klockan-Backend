﻿using KlockanAPI.Application.DTOs.Program;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IProgramService
{
    public Task<IEnumerable<ProgramDTO>> GetAllProgramsAsync();
}
