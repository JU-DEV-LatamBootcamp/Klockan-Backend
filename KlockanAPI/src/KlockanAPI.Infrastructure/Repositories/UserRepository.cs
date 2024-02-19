using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly KlockanContext _context;

    public UserRepository(KlockanContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync(int pageSize, int pageNumber)
    {
        return await _context.Users
                .Include(u => u.City)
                    .ThenInclude(c => c!.Country)
                .Include(u => u.Role)
                        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
                .ToListAsync();
    }
}
