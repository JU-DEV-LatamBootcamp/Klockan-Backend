using Microsoft.EntityFrameworkCore;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories.Interfaces;


namespace KlockanAPI.Infrastructure.Repositories;

public class MeetingRepository : IMeetingRepository
{
    private readonly KlockanContext _context;

    public MeetingRepository(KlockanContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Meeting>> GetAllAsync()
    {
        return await _context.Meetings.Include(m => m.Classroom).ToListAsync();
    }
}