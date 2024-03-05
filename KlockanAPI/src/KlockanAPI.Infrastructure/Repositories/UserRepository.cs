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

    public async Task<User> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var userToUpdate = await _context.Users.FindAsync(user.Id);
        userToUpdate!.FirstName = user.FirstName != "" ? user.FirstName : userToUpdate.FirstName;
        userToUpdate!.LastName = user.LastName != "" ? user.LastName : userToUpdate.LastName;
        userToUpdate!.Avatar = user.Avatar != "" ? user.Avatar : userToUpdate.Avatar;
        userToUpdate!.Birthdate = user.Birthdate != default ? user.Birthdate : userToUpdate.Birthdate;

        if (user.RoleId != 0)
        {
            var role = await _context.Roles.FindAsync(user.RoleId);
            userToUpdate!.RoleId = user.RoleId;
            userToUpdate!.Role = role;
        }

        if (user.CityId != 0)
        {
            var city = await _context.Cities.FindAsync(user.CityId);
            userToUpdate!.CityId = user.CityId;
            userToUpdate!.City = city;
        }

        userToUpdate!.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return userToUpdate;
    }
}
