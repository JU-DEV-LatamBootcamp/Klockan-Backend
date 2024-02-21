using MapsterMapper;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;




namespace KlockanAPI.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _ScheduleRepository;
    private readonly IMapper _mapper;

    public ScheduleService(IScheduleRepository ScheduleRepository, IMapper mapper)
    {
        _ScheduleRepository = ScheduleRepository;
        _mapper = mapper;
    }

    public async Task<List<ScheduleDTO>> CreateScheduleAsync(List<CreateScheduleDTO> createScheduleDTO, int id)
    {
        foreach (CreateScheduleDTO cShedule in createScheduleDTO)
        {
            cShedule.ClassroomId = id;
            CreateSAsync(cShedule);
        }

        return _mapper.Map<List<ScheduleDTO>>(createScheduleDTO);
    }

    public async Task<ScheduleDTO> CreateSAsync(CreateScheduleDTO createScheduleDTO)
    {
        var schedule = _mapper.Map<Schedule>(createScheduleDTO);
        var createdSchedule = await _ScheduleRepository.CreateScheduleAsync(schedule);
        return _mapper.Map<ScheduleDTO>(createdSchedule);
    }


    public async Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync()
    {
        var Schedules = await _ScheduleRepository.GetAllSchedulesAsync();
        return _mapper.Map<IEnumerable<ScheduleDTO>>(Schedules);
    }

}

