using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;
using KlockanAPI.Infrastructure.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infrastructure.Repositories;

public class ClassroomRepository : IClassroomRepository
{
    private readonly KlockanContext _context;

    public ClassroomRepository(KlockanContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Classroom>?> GetClassroomsByCourseIdAsync(int courseId)
    {
        var classrooms = await _context.Classrooms.Where(c => c.CourseId == courseId).ToListAsync();
        return classrooms.Count > 0 ? classrooms : null;
    }
}
