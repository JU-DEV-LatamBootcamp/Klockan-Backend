namespace KlockanAPI.Domain.Models;

public class MeetingAttendace(MeetingAttendanceStatus status, Meeting meeting, ClassroomUser user, int minutesAttended) : BaseModel
{
    public Meeting Meeting { get; set; } = meeting;
    public MeetingAttendanceStatus Status { get; set; } = status;
    public ClassroomUser User { get; set; } = user;
    public int MinutesAttended { get; set; } = minutesAttended;

}
