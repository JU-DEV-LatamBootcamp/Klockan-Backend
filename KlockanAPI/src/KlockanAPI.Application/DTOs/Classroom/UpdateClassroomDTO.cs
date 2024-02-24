using System.Data.Common;

namespace KlockanAPI.Application.DTOs.Classroom;

public class UpdateClassroomDTO
{
  public int Id { get; set; }
  public DateOnly StartDate { get; set; }
  public int ProgramId { get; set; }
  public int CourseId { get; set; }
  public List<UpdateClassroomScheduleDTO> Schedule { get; set; } = new List<UpdateClassroomScheduleDTO>();
}
