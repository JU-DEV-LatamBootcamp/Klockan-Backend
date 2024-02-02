﻿using MapsterMapper;
using KlockanAPI.Application.DTOs.Program;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.CrossCutting;

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

    public async Task<ProgramDTO> CreateProgramAsync(CreateProgramDTO createProgramDTO)
    {
        var program = _mapper.Map<Program>(createProgramDTO);
        var createdProgram = await _programRepository.CreateProgramAsync(program);
        return _mapper.Map<ProgramDTO>(createdProgram);
    }

    async public Task<ProgramDTO?> DeleteProgramAsync(int id)
    {
        var program = await _programRepository.GetProgramByIdAsync(id);
        NotFoundException.ThrowIfNull(program, $"Program with id {id} not found");

        var deletedProgram = await _programRepository.DeleteProgramAsync(program!);

        return _mapper.Map<ProgramDTO>(deletedProgram);
    }

}
