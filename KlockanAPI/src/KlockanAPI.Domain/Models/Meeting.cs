namespace KlockanAPI.Domain.Models;

public class Meeting(int classNumber, DateOnly date, TimeOnly time, Classroom classroom, ClassroomUser trainer, ClassroomUser[] participants) : BaseModel
{
    public int ClassNumber { get; set; } = classNumber;
    public DateOnly date { get; set; } = date;
    public TimeOnly Time { get; set; } = time;
    public Classroom Classroom { get; set; } = classroom;
    public ClassroomUser Trainer { get; set; } = trainer;
    public ClassroomUser[]? Participants { get; set; } = participants;
}