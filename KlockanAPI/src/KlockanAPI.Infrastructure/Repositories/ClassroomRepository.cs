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

    public async Task<Classroom> UpdateClassroomAsync(Classroom classroom)
    {
        var classroomToUpdate = _context.Classrooms.Find(classroom.Id);
        var schedules = _context.Schedules.AsNoTracking().Where(s => s.ClassroomId == classroom.Id).ToList();
        var schedulesToDelete = GetMissingElements(schedules, classroom.Schedule.ToList(), (schedule) => schedule.Id);

        _context.Classrooms.Entry(classroomToUpdate!).CurrentValues.SetValues(classroom);
        _context.Schedules.UpdateRange(classroom.Schedule);
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

    public async Task<Classroom?> GetClassroomByIdAsync(int id)
    {
        var result = _context.Classrooms.AsNoTracking()
            .Where((classroom) => classroom.Id == id);

        var classroom = result.Count() > 0 ? result.First() : null;

        return await Task.FromResult(classroom);
    }

    public async Task<Classroom> DeleteClassroomAsync(Classroom classroom)
    {
        _context.Classrooms.Remove(classroom);
        await _context.SaveChangesAsync();
        return classroom;
    }

    public List<T> GetMissingElements<T, K>(List<T> baseList, List<T> updatedList, Func<T, K> getValueFunc)
        where T : class
        where K : IEquatable<K>
    {
        var deletedSchedules = new List<T>();

        foreach (var baseElement in baseList)
        {
            var baseValue = getValueFunc(baseElement);
            if (!updatedList.Any(otherElement => baseValue.Equals(getValueFunc(otherElement))))
            {
                deletedSchedules.Add(baseElement);
            }
        }

        return deletedSchedules;
    }
}
