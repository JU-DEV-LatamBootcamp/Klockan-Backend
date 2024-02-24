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
        try
        {
            var existingTrainer = await _context.ClassroomUsers
             .FirstOrDefaultAsync(u => u.Id == meeting.TrainerId && u.ClassroomId == meeting.ClassroomId);

            if (existingTrainer == null)
            {
                var userTrainer = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(ut => ut.Id == meeting.TrainerId);

                var newTrainer = new ClassroomUser
                {
                    UserId = userTrainer.Id,
                    ClassroomId = meeting.ClassroomId,
                    RoleId = userTrainer.RoleId,
                    Classroom = null,
                    User = null,
                    Role = null
                };
                await _context.ClassroomUsers.AddAsync(newTrainer);
                //await _context.SaveChangesAsync();
            }
            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();

            return meeting;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task AssignStudents(ICollection<MeetingAttendance> meetingAttendance, int classroomId)
    {
        try
        {
            foreach (var attendance in meetingAttendance)
            {
                var existingUser = await _context.ClassroomUsers
                .FirstOrDefaultAsync(cu => cu.Id == attendance.ClassroomUserId && cu.ClassroomId == classroomId);

                if (existingUser == null)
                {
                    var user = await _context.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == attendance.ClassroomUserId);

                    var newClassroomUser = new ClassroomUser
                    {
                        UserId = user.Id,
                        ClassroomId = classroomId,
                        RoleId = user.RoleId,
                        Classroom = null,
                        User = null,
                        Role = null
                    };
                    await _context.ClassroomUsers.AddAsync(newClassroomUser);
                    attendance.User = newClassroomUser;
                    await _context.SaveChangesAsync();
                }
            }

            await _context.MeetingAttendances.AddRangeAsync(meetingAttendance);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
           
        }
    }

    public async Task<int> GetSessionNumber(int classroomId)
    {
        var maxSessionNumber = await _context.Meetings
        .AsNoTracking()
        .Where(m => m.ClassroomId == classroomId)
        .Select(m => m.SessionNumber)
        .DefaultIfEmpty()
        .MaxAsync();

        if (maxSessionNumber == 0)
        {
            var course = await _context.Meetings
           .AsNoTracking()
           .Where(m => m.ClassroomId == classroomId)
           .Select(m => m.Classroom.Course)
           .FirstOrDefaultAsync();

            if (course != null && course.Sessions.HasValue)
            {
                return course.Sessions.Value;
            }
        }

        return maxSessionNumber;
    }
}
