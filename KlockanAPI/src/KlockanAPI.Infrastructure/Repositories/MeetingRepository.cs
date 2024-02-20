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
        //Include trainer relationship when seeders impleemted 
        return await _context.Meetings.Include(m => m.Classroom).ToListAsync();
    }

    public async Task<IEnumerable<Meeting>?> GetMeetingsByClassroomIdAsync(int classroomId)
    {
        var meetings = await _context.Meetings.Where(m => m.ClassroomId == classroomId).ToListAsync();
        return meetings.Count > 0 ? meetings : null;
    }
}