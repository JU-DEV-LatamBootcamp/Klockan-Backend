using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync(int pageSize, int pageNumber);
    Task <User> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(int userId);
    Task<User> UpdateUserAsync(User user);

}
