namespace KlockanAPI.Application.DTOs.Schedule;

public class ScheduleDTO
{
    public int Id { get; set; }
    public int WeekdayId { get; set; }
    public int ClassroomId { get; set; }
    public TimeOnly StartTime { get; set; }
}
