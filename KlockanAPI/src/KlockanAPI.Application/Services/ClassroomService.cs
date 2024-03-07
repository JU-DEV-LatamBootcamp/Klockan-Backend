using MapsterMapper;
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.CrossCutting;
using KlockanAPI.Application.DTOs.ClassroomUser;

namespace KlockanAPI.Application.Services;

public class ClassroomService : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMeetingRepository _meetingRepository;
    private readonly IClassroomUserRepository _classroomUserRepository;
    private readonly IMapper _mapper;

    public ClassroomService(
        IClassroomRepository classroomRepository,
        IMeetingRepository meetingRepository,
        IClassroomUserRepository classroomUserRepository,
        IMapper mapper)
    {
        _classroomRepository = classroomRepository;
        _meetingRepository = meetingRepository;
        _classroomUserRepository = classroomUserRepository;
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

    public async Task<ClassroomDTO> UpdateClassroomAsync(UpdateClassroomDTO updateClassroomDTO)
    {
        var id = updateClassroomDTO.Id;
        var dbClassroom = await _classroomRepository.GetClassroomByIdAsync(id);
        NotFoundException.ThrowIfNull(dbClassroom, $"Classroom with id {id} not found");

        var classroom = _mapper.Map<Classroom>(updateClassroomDTO);
        var schedules = _mapper.Map<List<Schedule>>(classroom);

        classroom.Schedule = schedules;

        var udpatedClassroom = await _classroomRepository.UpdateClassroomAsync(classroom);

        return _mapper.Map<ClassroomDTO>(udpatedClassroom);
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

    public async Task<List<ClassroomUserDTO>> UpdateClassroomUsersAsync(UpdateClassroomUsersDTO updateClassroomUsersDTO)
    {
        var classroom = await _classroomRepository.GetClassroomByIdAsync(updateClassroomUsersDTO.Id);
        NotFoundException.ThrowIfNull(classroom, $"Classroom with id {updateClassroomUsersDTO.Id} not found");

        var classroomUsers = _mapper.Map<List<ClassroomUser>>(updateClassroomUsersDTO);

        var udpatedClassroomUsers = await _classroomUserRepository.UpdateClassroomUsersAsync(updateClassroomUsersDTO.Id, classroomUsers);

        return _mapper.Map<List<ClassroomUserDTO>>(udpatedClassroomUsers);
    }
}
