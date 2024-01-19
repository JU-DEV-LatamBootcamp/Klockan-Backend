using System.Data.Common;

namespace KlockanAPI.Domain.Models;

public class MeetingAttendace(int id, MeetingAttendanceStatus status, Meeting meeting, ClassroomUser user, DateTime createdAt, DateTime updatedAt, DateTime deletedAt)
{
    public int Id { get; set; } = id;
    public MeetingAttendanceStatus Status { get; set; } = status;
    public Meeting Meeting { get; set; } = meeting;
    public ClassroomUser User { get; set; } = user;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public DateTime DeletedAt { get; set; } = deletedAt;
}