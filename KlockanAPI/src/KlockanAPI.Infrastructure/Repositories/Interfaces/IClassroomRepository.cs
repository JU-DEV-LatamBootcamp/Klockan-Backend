using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IClassroomRepository
{
    Task<IEnumerable<Classroom>> GetAllClassroomsAsync();
    Task<IEnumerable<Classroom>?> GetClassroomsByCourseIdAsync(int courseId);
    Task<IEnumerable<Classroom>?> GetClassroomsByProgramIdAsync(int programId);
    Task<Classroom> CreateClassroomAsync(Classroom classroom);
    Task<Classroom> UpdateClassroomAsync(Classroom classroom);
    Task<Classroom?> GetClassroomByIdAsync(int id);
    Task<Classroom> DeleteClassroomAsync(Classroom classroom);
    Task<Classroom?> GetClassroomDetailsAsync(int classroomId);
}
