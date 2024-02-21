namespace KlockanAPI.Domain.Models;

public class Schedule : BaseModel
{
    public int WeekdayId { get; set; }
    public Weekday Weekday { get; set; } = new Weekday();
    public int ClassroomId { get; set; }
    public required Classroom Classroom { get; set; } = new Classroom();
    public TimeOnly StartTime { get; set; }
    public TimeOnly FinishTime { get; set; }

}
