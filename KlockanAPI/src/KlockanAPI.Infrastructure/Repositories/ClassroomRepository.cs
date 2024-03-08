using Microsoft.EntityFrameworkCore;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories.Interfaces;

namespace KlockanAPI.Infrastructure.Repositories;

public class ClassroomRepository : IClassroomRepository
{
    private readonly KlockanContext _context;

    public ClassroomRepository(KlockanContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Classroom>> GetAllClassroomsAsync()
    {
        var classrooms = await Task.FromResult(_context.Classrooms
            .Include(c => c.Program)
            .Include(c => c.Course)
            .ToList());

        return classrooms;
    }

    public async Task<Classroom> CreateClassroomAsync(Classroom classroom)
    {
        await _context.Classrooms.AddAsync(classroom);
        await _context.SaveChangesAsync();
        return classroom;
    }

    public async Task<IEnumerable<Classroom>?> GetClassroomsByCourseIdAsync(int courseId)
    {
        var classrooms = await _context.Classrooms.Where(c => c.CourseId == courseId).ToListAsync();
        return classrooms.Count > 0 ? classrooms : null;
    }

    public async Task<IEnumerable<Classroom>?> GetClassroomsByProgramIdAsync(int programId)
    {
        var classrooms = await _context.Classrooms.Where(c => c.ProgramId == programId).ToListAsync();
        return classrooms.Count > 0 ? classrooms : null;
    }

    public async Task<Classroom> GetClassroomByIdAsync(int id)
    {
        try
        {
            return await _context.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Program)
                .Include(c => c.Schedule)
                    .ThenInclude(s => s.Weekday)
                .Include(c => c.ClassroomUsers)
                    .ThenInclude(cu => cu.User)
                .FirstAsync(c => c.Id == id);
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
    }

    public async Task<IEnumerable<User>> GetClassroomUsersAsync(int id)
    {
        return await Task.FromResult(_context.ClassroomUsers
            .Where(cu => cu.ClassroomId == id)
            .Select(cu => new User
            {
                Id = cu.UserId,
                Avatar = cu.User.Avatar,
                Email = cu.User.Email,
                FirstName = cu.User.FirstName,
                LastName = cu.User.LastName,
                Role = cu.Role,
                City = cu.User.City,
            })
            .ToList());
    }

    public async Task<Classroom> DeleteClassroomAsync(Classroom classroom)
    {
        _context.Classrooms.Remove(classroom);
        await _context.SaveChangesAsync();
        return classroom;
    }

    public async Task<User> RemoveUserFromClassroomAsync(Classroom classroom, User user)
    {
        var classroomUserToDelete = classroom.ClassroomUsers.Where(cu => cu.UserId == user.Id).First();
        classroom.ClassroomUsers.Remove(classroomUserToDelete);
        await _context.SaveChangesAsync();
        return user;
    }
}
