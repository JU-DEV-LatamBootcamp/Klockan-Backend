using Microsoft.EntityFrameworkCore;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Infrastructure.Extensions;

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

    public async Task<Classroom> UpdateClassroomAsync(Classroom classroom)
    {
        var classroomToUpdate = _context.Classrooms.Find(classroom.Id);
        var schedules = _context.Schedules.AsNoTracking().Where(s => s.ClassroomId == classroom.Id).ToList();
        var classroomSchedules = classroom.Schedule.ToList();

        var schedulesToDelete = classroomSchedules.FilterTarget(schedules, (master, target) => target.Id == master.Id, false);
        var schedulesToUpdate =
            schedules.Count == 0
                ? classroomSchedules.Where(schedule => schedule.Id == 0).ToList()
                : classroomSchedules.FilterByTarget(schedules, (master, target) => target.Id == master.Id || master.Id == 0);

        _context.Classrooms.Entry(classroomToUpdate!).CurrentValues.SetValues(classroom);
        _context.Schedules.UpdateRange(schedulesToUpdate);
        _context.Schedules.RemoveRange(schedulesToDelete);

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

    public async Task<Classroom?> GetClassroomByIdAsync(int id, bool populate = false)
    {
        IQueryable<Classroom> result = _context.Classrooms;
        if (populate)
            result = result.Include(c => c.Course).Include(p => p.Program);
        result = result.AsNoTracking().Where((classroom) => classroom.Id == id);

        var classroom = result.Count() > 0 ? result.First() : null;

        return await Task.FromResult(classroom);
    }

    public async Task<Classroom> DeleteClassroomAsync(Classroom classroom)
    {
        _context.Classrooms.Remove(classroom);
        await _context.SaveChangesAsync();
        return classroom;
    }

    public async Task<Classroom?> GetClassroomDetailsAsync(int classroomId)
    {
        return await _context.Classrooms
            .Include(c => c.Course)
            .Include(c => c.Program)
            .Include(c => c.ClassroomUsers)
                .ThenInclude(cu => cu.User)
                    .ThenInclude(u => u.Role)
            .FirstOrDefaultAsync(c => c.Id == classroomId);
    }
}
