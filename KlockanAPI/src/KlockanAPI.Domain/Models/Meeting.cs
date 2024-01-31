#nullable disable

namespace KlockanAPI.Domain.Models;

public class Meeting : BaseModel
{
    public int SessionNumber { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; }
    public int? TrainerId { get; set; }
    public ClassroomUser? Trainer { get; set; }
    public ICollection<MeetingAttendance> MeetingAttendances { get; set; } = new List<MeetingAttendance>();
}
