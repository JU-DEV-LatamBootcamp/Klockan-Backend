using KlockanAPI.Domain.Models;
namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IMeetingRepository
{
    Task<IEnumerable<Meeting>> GetAllAsync();
    
    Task<IEnumerable<Meeting>?> GetMeetingsByClassroomIdAsync(int classroomId);

    Task<Meeting> CreateSingleMeeting(Meeting meeting);

    Task<int> GetSessionNumber(int classroomId);

    Task<int?> AddUserToClassroomAsync(int userId, int classroomId);
}
