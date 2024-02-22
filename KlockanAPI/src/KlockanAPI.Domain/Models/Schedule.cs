namespace KlockanAPI.Domain.Models;

public class Schedule : BaseModel
{
    public int WeekdayId { get; set; }
    public int ClassroomId { get; set; }
    public TimeOnly StartTime { get; set; }
    public Weekday? Weekday { get; set; }
    public Classroom? Classroom { get; set; }
}
