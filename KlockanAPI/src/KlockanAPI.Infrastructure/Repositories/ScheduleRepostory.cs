using Microsoft.EntityFrameworkCore;

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
        var Schedules = await Task.FromResult(_context.Schedules
            .Include(c => c.Classroom)
            .ToList());

        return Schedules;
    }

    public async Task<Schedule> CreateScheduleAsync(Schedule Schedule)
    {
        await _context.Schedules.AddAsync(Schedule);
        await _context.SaveChangesAsync();
        return Schedule;
    }


}
