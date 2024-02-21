using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.Weekday;

namespace KlockanAPI.Application.DTOs.Schedule;

public class ScheduleDTO
{
    public int Id { get; set; }

    public int WeekdayId { get; set; }

    public int ClassroomId { get; set; }
    public ClassroomDTO Classroom { get; set; }

    public WeekdayDTO Weekday { get; set; }
    public TimeOnly StartTime { get; set; }

    public TimeOnly FinisihTime { get; set; }

}
