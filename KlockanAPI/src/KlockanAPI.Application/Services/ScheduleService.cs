using MapsterMapper;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
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

        var schedules = await _scheduleRepository.GetSchedulesByClassroomIdAsync(classroomId);
        var schedulesDTOs = _mapper.Map<List<ScheduleDTO>>(schedules);

        return schedulesDTOs;
    }
}
