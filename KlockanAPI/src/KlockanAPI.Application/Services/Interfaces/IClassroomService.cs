
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.ClassroomUser;
using KlockanAPI.Application.DTOs.User;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IClassroomService
{
    Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync();
    Task<ClassroomDTO> GetClassroomByIdAsync(int id, bool populate);
    Task<ClassroomDTO> CreateClassroomAsync(CreateClassroomDTO createClassroomDTO);
    Task<ClassroomDTO?> DeleteClassroomAsync(int id);
    Task<ClassroomDTO> UpdateClassroomAsync(UpdateClassroomDTO updateClassroomDTO);
    Task<List<ClassroomUserDTO>> UpdateClassroomUsersAsync(UpdateClassroomUsersDTO updateClassroomUsersDTO);
}
