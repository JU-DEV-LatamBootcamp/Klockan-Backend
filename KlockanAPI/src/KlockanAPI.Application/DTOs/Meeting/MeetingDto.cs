namespace KlockanAPI.Application.DTOs.Meeting;

public class MeetingDto
{
    public int Id { get; set; }
    public int SessionNumber { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public int ClassroomId { get; set; }

}