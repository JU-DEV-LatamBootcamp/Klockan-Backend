namespace KlockanAPI.Application.DTOs.Classroom;

public class UpdateClassroomScheduleDTO
{
  public int Id { get; set; }
  public int WeekdayId { get; set; }
  public TimeOnly StartTime { get; set; }
}
