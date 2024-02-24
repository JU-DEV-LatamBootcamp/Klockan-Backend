using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories.Interfaces;

namespace KlockanAPI.Infrastructure.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly KlockanContext _context;

    public ScheduleRepository(KlockanContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
    {
        var Schedules = await Task.FromResult(_context.Schedules.ToList());

        return Schedules;
    }

    public async Task<Schedule> CreateScheduleAsync(Schedule Schedule)
    {
        await _context.Schedules.AddAsync(Schedule);
        await _context.SaveChangesAsync();
        return Schedule;
    }

    public async Task<IEnumerable<Schedule>> GetAllSchedulesByClassroomIdAsync(int id)
    {
        var Schedules = await Task.FromResult(_context.Schedules
            .Where(s => s.ClassroomId == id)
            .ToList());

        return Schedules;
    }

    public async Task<bool> UpdateOrCreateManySchedulesAsync(IEnumerable<Schedule> schedules)
    {
        await _context.Schedules.AddRangeAsync(schedules);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveManySchedulesAsync(List<int> idsToDelete)
    {
        _context.Schedules.RemoveRange(
            _context.Schedules.Where(e => idsToDelete.Contains(e.Id))
        );
        await _context.SaveChangesAsync();

        return true;
    }
}
