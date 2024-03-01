
using KlockanAPI.Application.DTOs.Classroom;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IClassroomService
{
    Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync();
    Task<ClassroomDTO> CreateClassroomAsync(CreateClassroomDTO createClassroomDTO);
    Task<ClassroomDTO?> DeleteClassroomAsync(int id);
    Task<ClassroomDTO> UpdateClassroomAsync(UpdateClassroomDTO updateClassroomDTO);
}
