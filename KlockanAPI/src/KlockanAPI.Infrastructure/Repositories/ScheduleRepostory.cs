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

    public async Task<IEnumerable<Schedule>> GetSchedulesByClassroomIdAsync(int id)
    {
        var Schedules = await Task.FromResult(_context.Schedules
            .Where(s => s.ClassroomId == id)
            .ToList());

        return Schedules;
    }
}
