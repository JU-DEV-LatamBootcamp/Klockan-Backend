
using KlockanAPI.Application.DTOs.ClassroomUser;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IClassroomUserService
{
    Task<List<ClassroomUserDTO>> GetUsersByClassroomIdAsync(int classroomId);
}