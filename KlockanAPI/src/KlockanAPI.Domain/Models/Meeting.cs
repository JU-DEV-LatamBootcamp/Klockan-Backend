namespace KlockanAPI.Domain.Models;

public class Meeting: BaseModel
{
    public int SessionNumber { get; set; } 
    public DateOnly Date { get; set; } 
    public TimeOnly Time { get; set; } 
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; } = new Classroom();
    public int ClassroomUserId { get; set; }
    public ClassroomUser Trainer { get; set; } = new ClassroomUser(); 
}
