using KlockanAPI.Domain.Models;
namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IMeetingRepository
{
    Task<IEnumerable<Meeting>> GetAllAsync();
    
    Task<IEnumerable<Meeting>?> GetMeetingsByClassroomIdAsync(int classroomId);

    Task<Meeting> CreateSingleMeeting(Meeting meeting);

    Task AssignStudents(ICollection<MeetingAttendance> meetingAttendance, int classroomId);

    Task<int> GetSessionNumber(int classroomId);
}
