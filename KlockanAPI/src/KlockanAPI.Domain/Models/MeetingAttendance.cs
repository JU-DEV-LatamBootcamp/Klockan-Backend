namespace KlockanAPI.Domain.Models;

public class MeetingAttendance : BaseModel
{
    public int MinutesAttended { get; set; } 
    public int MeetingId { get; set; }
    public Meeting Meeting { get; set; } = new Meeting();
    public int ClassroomUserId { get; set; }
    public ClassroomUser User { get; set; } = new ClassroomUser();
    public int MeetingAttendanceStatusId { get; set; }
    public MeetingAttendanceStatus Status { get; set; } = new MeetingAttendanceStatus();
}
