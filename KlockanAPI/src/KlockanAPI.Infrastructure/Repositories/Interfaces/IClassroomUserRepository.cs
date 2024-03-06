using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IClassroomUserRepository
{
    Task<IEnumerable<ClassroomUser>> UpdateClassroomUsersAsync(int classroomId, IEnumerable<ClassroomUser> classroomUsers);
    Task<IEnumerable<ClassroomUser>> GetClassroomUsersAsync(int classroomId);
}
