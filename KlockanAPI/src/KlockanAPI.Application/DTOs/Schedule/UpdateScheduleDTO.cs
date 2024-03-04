namespace KlockanAPI.Application.DTOs.Schedule;

public class UpdateScheduleDTO
{
  public int Id { get; set; }
  public int WeekdayId { get; set; }
  public TimeOnly StartTime { get; set; }
}
