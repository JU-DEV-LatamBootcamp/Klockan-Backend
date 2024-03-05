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

    public async Task<Meeting> CreateSingleMeeting(Meeting meeting)
    {
        await _context.Meetings.AddAsync(meeting);
        await _context.SaveChangesAsync();

        return meeting;
    }

    public async Task<int> GetSessionNumber(int classroomId)
    {
        var meetings = await _context.Meetings
        .AsNoTracking()
        .Where(m => m.ClassroomId == classroomId)
        .ToListAsync();

        var maxSessionNumber = meetings.Any() ? meetings.Max(m => m.SessionNumber) : 0;

        var course = await _context.Classrooms
            .Where(c => c.Id == classroomId)
            .Select(c => c.Course)
            .FirstOrDefaultAsync();

        if (course != null && course.Sessions.HasValue)
            return Math.Max(maxSessionNumber, course.Sessions.Value);

        return maxSessionNumber;
    }

    public async Task<int?> AddUserToClassroomAsync(int userId, int classroomId)
    {
        var existingClassroomUser = await _context.ClassroomUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(cu => cu.UserId == userId && cu.ClassroomId == classroomId);

        if (existingClassroomUser == null)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            var newClassroomUser = new ClassroomUser
            {
                UserId = userId,
                ClassroomId = classroomId,
                RoleId = (int)user.RoleId,
                Classroom = null,
                User = null,
                Role = null
            };

            _context.ClassroomUsers.Add(newClassroomUser);
            await _context.SaveChangesAsync();

            return newClassroomUser.Id;
        }

        return existingClassroomUser.Id;
    }
}
