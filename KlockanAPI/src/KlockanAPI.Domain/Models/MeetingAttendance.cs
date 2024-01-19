using System.Data.Common;

namespace KlockanAPI.Domain.Models;

public class MeetingAttendace(int id, MeetingAttendanceStatus status, Meeting meeting, ClassroomUser user, int minutesAttended, DateTime createdAt, DateTime updatedAt, DateTime deletedAt)
{
    public int Id { get; set; } = id;
    public Meeting Meeting { get; set; } = meeting;
    public MeetingAttendanceStatus Status { get; set; } = status;
    public ClassroomUser User { get; set; } = user;
    public int MinutesAttended { get; set; } = minutesAttended;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public DateTime DeletedAt { get; set; } = deletedAt;
}