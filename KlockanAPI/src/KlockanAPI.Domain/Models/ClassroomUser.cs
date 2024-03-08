namespace KlockanAPI.Domain.Models;

public class ClassroomUser : BaseModel
{
    public int ClassroomId { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public Classroom? Classroom { get; set; }
    public User? User { get; set; }
    public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>(); // refers to the meetings that a trainer only has
    public ICollection<MeetingAttendance> MeetingAttendances { get; set; } = new List<MeetingAttendance>(); // refers to the meetings that a student has attended
}
