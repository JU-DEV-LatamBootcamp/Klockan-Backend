
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Domain.Models;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IClassroomService
{
    Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync();
    Task<IEnumerable<ClassroomUser>> GetClassroomUsersAsync(int classroomId);
    Task<ClassroomDTO> CreateClassroomAsync(CreateClassroomDTO createClassroomDTO);

    public List<CreateScheduleDTO> MapCreateClassroomSchedulesDTOsToCreateScheduleDTOs(int id, List<CreateClassroomScheduleDTO> classroomSchedules);
    Task<ClassroomDTO?> DeleteClassroomAsync(int id);
}
