using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> CreateAsync(Course course);
    Task<Course?> GetCourseByIdAsync(int id);
    Task<Course> DeleteCourseAsync(Course course);
    Task<Course> UpdateCourseAsync(Course course);
}

