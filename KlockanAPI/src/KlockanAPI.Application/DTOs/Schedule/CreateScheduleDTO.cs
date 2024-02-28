namespace KlockanAPI.Application.DTOs.Schedule;

public class CreateScheduleDTO
{
    public int WeekdayId { get; set; }
    public int ClassroomId { get; set; }
    public TimeOnly StartTime { get; set; }

    public CreateScheduleDTO(int weekdayId, int classroomId, TimeOnly startTime)
    {
        WeekdayId = weekdayId;
        ClassroomId = classroomId;
        StartTime = startTime;
    }
}
