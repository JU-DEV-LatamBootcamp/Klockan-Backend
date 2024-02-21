

namespace KlockanAPI.Application.DTOs.Schedule;

public class CreateScheduleDTO
{
    public int WeekdayId { get; set; }
    public int ClassroomId { get; set; }
    public TimeOnly StartTime { get; set; }
}
