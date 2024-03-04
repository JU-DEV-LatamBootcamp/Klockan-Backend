using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application.DTOs.Classroom;

public class UpdateClassroomDTO
{
  public int Id { get; set; }
  public DateOnly StartDate { get; set; }
  public int ProgramId { get; set; }
  public int CourseId { get; set; }
  public List<UpdateScheduleDTO> Schedule { get; set; } = new List<UpdateScheduleDTO>();
}
