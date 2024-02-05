
using KlockanAPI.Application.DTOs.Classroom;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IClassroomService
{
    Task<IEnumerable<ClassroomDTO>> GetAllClassroomsAsync();
}
