using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IClassroomRepository
{
    Task<IEnumerable<Classroom>?> GetClassroomsByCourseIdAsync(int courseId);
}
