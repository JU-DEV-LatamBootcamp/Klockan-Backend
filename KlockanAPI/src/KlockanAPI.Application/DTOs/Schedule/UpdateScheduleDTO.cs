namespace KlockanAPI.Application.DTOs.Schedule;

public class UpdateScheduleDTO
{
    public int Id;
    public int WeekdayId { get; set; }
    public int ClassroomId { get; set; }
    public TimeOnly StartTime { get; set; }

    public UpdateScheduleDTO(int id, int weekdayId, int classroomId, TimeOnly startTime)
    {
        Id = id;
        WeekdayId = weekdayId;
        ClassroomId = classroomId;
        StartTime = startTime;
    }
}
