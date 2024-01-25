using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
}