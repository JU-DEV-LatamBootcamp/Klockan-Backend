using MapsterMapper;
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.CrossCutting;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application.Services;

public class ClassroomService : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMeetingRepository _meetingRepository;
    private readonly IMapper _mapper;

    public ClassroomService(IClassroomRepository classroomRepository, IMapper mapper, IMeetingRepository meetingRepository)
    {
        _classroomRepository = classroomRepository;
        _meetingRepository = meetingRepository;
        _mapper = mapper;
    }

    public async Task<ClassroomDTO> CreateClassroomAsync(CreateClassroomDTO createClassroomDTO)
    {
        var classroom = _mapper.Map<Classroom>(createClassroomDTO);
        var createdClassroom = await _classroomRepository.CreateClassroomAsync(classroom);

        return _mapper.Map<ClassroomDTO>(createdClassroom);
    }


    public async Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync()
    {
        var classrooms = await _classroomRepository.GetAllClassroomsAsync();
        return _mapper.Map<IEnumerable<ClassroomDTO>>(classrooms);
    }

    public async Task<IEnumerable<ClassroomUser>> GetClassroomUsersAsync(int classroomId)
    {
        var classroom = (await _classroomRepository.GetClassroomByIdAsync(classroomId))
            ?? throw new NotFoundException($"Classroom with Id {classroomId} not found");
        return _mapper.Map<IEnumerable<ClassroomUser>>(classroom.ClassroomUsers);
    }

    public List<CreateScheduleDTO> MapCreateClassroomSchedulesDTOsToCreateScheduleDTOs(int id, List<CreateClassroomScheduleDTO> classroomSchedules)
    {
        var schedules = classroomSchedules.Aggregate(
            new List<CreateScheduleDTO>(),
            (schedules, createClassroomSchedule) =>
            {
                var newSchedule = new CreateScheduleDTO(
                    createClassroomSchedule.WeekdayId,
                    id,
                    createClassroomSchedule.StartTime
                );
                schedules.Add(newSchedule);

                return schedules;
            }
        );

        return schedules;
    }

    public async Task<ClassroomDTO?> DeleteClassroomAsync(int id)
    {
        var classroom = await _classroomRepository.GetClassroomByIdAsync(id);
        NotFoundException.ThrowIfNull(classroom, $"Classroom with id {id} not found");
        var meetings = await _meetingRepository.GetMeetingsByClassroomIdAsync(id);
        FoundException.ThrowIfNotNull(meetings, $"Classroom {id} has meetings assigned ot it.");
        var deletedClassroom = await _classroomRepository.DeleteClassroomAsync(classroom!);
        return _mapper.Map<ClassroomDTO>(deletedClassroom);
    }
}

