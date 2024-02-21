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

    public Task<Classroom?> GetClassroomByIdAsync(int id)
    {
        var classroom = _context.Classrooms.Find(id);

        return Task.FromResult(classroom);
    }

}
