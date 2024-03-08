
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.Schedule;
using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Domain.Models;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IClassroomService
{
    Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync();
    Task<ClassroomDTO> GetClassroomByIdAsync(int id);
    Task<IEnumerable<User>> GetClassroomUsersAsync(int classroomId);
    Task<ClassroomDTO> CreateClassroomAsync(CreateClassroomDTO createClassroomDTO);

    public List<CreateScheduleDTO> MapCreateClassroomSchedulesDTOsToCreateScheduleDTOs(int id, List<CreateClassroomScheduleDTO> classroomSchedules);
    Task<ClassroomDTO?> DeleteClassroomAsync(int id);
    public Task<UserDto?> RemoveUserFromClassroom(int classroomId, int userId);
}
