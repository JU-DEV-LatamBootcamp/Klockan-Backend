namespace KlockanAPI.Application.DTOs.Classroom;

public class CreateClassroomScheduleDTO
{
  public int WeekdayId { get; set; }
  public TimeOnly StartTime { get; set; }
}
