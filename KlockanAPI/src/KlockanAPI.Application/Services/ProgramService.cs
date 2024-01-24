using Mapster;
using MapsterMapper;
using KlockanAPI.Application.DTOs.Program;
using KlockanAPI.Application.Services.Interfaces;

namespace KlockanAPI.Application.Services;

public class ProgramService : IProgramService
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;

    public ProgramService(IProgramRepository programRepository, IMapper mapper)
    {
        this._programRepository = programRepository;
        this._mapper = mapper;
    }

    public async Task<List<ProgramDTO>> GetProgramsAsync()
    {
        var result = await _programRepository.GetProgramsAsync();
        var resultDTO = _mapper.Map<List<ProgramDTO>>(result);

        return resultDTO;
    }
}