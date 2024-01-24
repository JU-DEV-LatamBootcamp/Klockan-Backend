namespace KlockanAPI.Domain.Models;

public class ClassroomUser : BaseModel
{
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; } = new Classroom();
    public int UserId { get; set; }
    public User User { get; set; } = new User();
    public int RoleId { get; set; }
    public Role Role { get; set; } = new Role();
    public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>(); // refers to the meetings that a trainer only has
    public ICollection<MeetingAttendance> MeetingAttendances { get; set; } = new List<MeetingAttendance>(); // refers to the meetings that a student has attended

}
