namespace KlockanAPI.Domain.Models;

public class MeetingAttendanceStatus : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public ICollection<MeetingAttendance> MeetingAttendances { get; set; } = new List<MeetingAttendance>();
}
