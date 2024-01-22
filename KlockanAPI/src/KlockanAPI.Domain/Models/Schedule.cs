namespace KlockanAPI.Domain.Models;

public class Schedule(Weekdays[] weekdays, Classroom classroom) : BaseModel
{
    public Weekdays[] Weekdays { get; set; } = weekdays;
    public Classroom Classroom { get; set; } = classroom;

}
