
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.ClassroomUser;
using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Domain.Models;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IClassroomService
{
    Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync();
    Task<IEnumerable<User>> GetClassroomUsersAsync(int classroomId);
    Task<ClassroomDTO> GetClassroomByIdAsync(int id, bool populate);
    Task<ClassroomDTO> CreateClassroomAsync(CreateClassroomDTO createClassroomDTO);
    Task<ClassroomDTO?> DeleteClassroomAsync(int id);
    public Task<UserDto?> RemoveUserFromClassroom(int classroomId, int userId);
    Task<ClassroomDTO> UpdateClassroomAsync(UpdateClassroomDTO updateClassroomDTO);
    Task<List<ClassroomUserDTO>> UpdateClassroomUsersAsync(UpdateClassroomUsersDTO updateClassroomUsersDTO);
}
