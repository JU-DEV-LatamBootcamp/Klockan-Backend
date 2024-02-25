using MapsterMapper;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.CrossCutting;

namespace KlockanAPI.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;

    public ScheduleService(IScheduleRepository scheduleRepository, IClassroomRepository classroomRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _classroomRepository = classroomRepository;
        _mapper = mapper;
    }

    public async Task<List<ScheduleDTO>> GetSchedulesByClassroomIdAsync(int classroomId)
    {
        var classroom = await _classroomRepository.GetClassroomByIdAsync(classroomId);
        NotFoundException.ThrowIfNull(classroom, $"Classroom with id {classroomId} not found");

        var schedules = await _scheduleRepository.GetAllSchedulesByClassroomIdAsync(classroomId);
        var schedulesDTOs = _mapper.Map<List<ScheduleDTO>>(schedules);

        return schedulesDTOs;
    }

    public async Task<List<ScheduleDTO>> CreateScheduleAsync(List<CreateScheduleDTO> createScheduleDTO, int id)
    {
        foreach (CreateScheduleDTO cShedule in createScheduleDTO)
        {
            cShedule.ClassroomId = id;
            await CreateScheduleAsync(cShedule);
        }

        return _mapper.Map<List<ScheduleDTO>>(createScheduleDTO);
    }

    public async Task<ScheduleDTO> CreateScheduleAsync(CreateScheduleDTO createScheduleDTO)
    {
        var schedule = _mapper.Map<Schedule>(createScheduleDTO);
        var createdSchedule = await _scheduleRepository.CreateScheduleAsync(schedule);
        return _mapper.Map<ScheduleDTO>(createdSchedule);
    }


    public async Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync()
    {
        var Schedules = await _scheduleRepository.GetAllSchedulesAsync();
        return _mapper.Map<IEnumerable<ScheduleDTO>>(Schedules);
    }

    public async Task<bool> CreateManySchedulesAsync(List<CreateScheduleDTO> createScheduleDTOs)
    {
        var schedules = _mapper.Map<List<Schedule>>(createScheduleDTOs);
        var result = await _scheduleRepository.CreateManySchedulesAsync(schedules);

        return result;
    }
}