﻿using MapsterMapper;
using KlockanAPI.Application.DTOs.Program;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;

namespace KlockanAPI.Application.Services;

public class ProgramService : IProgramService
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;

    public ProgramService(IProgramRepository programRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProgramDTO>> GetAllProgramsAsync()
    {
        var programs = await _programRepository.GetAllProgramsAsync();
        return _mapper.Map<IEnumerable<ProgramDTO>>(programs);
    }
}
