namespace KlockanAPI.Application.DTOs.Schedule;

public class CreateScheduleDTO
{
    public int WeekdayId { get; set; }
    public int ClassroomId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly FinishTime { get; set; }

    public CreateScheduleDTO(int weekdayId, int classroomId, TimeOnly startTime, TimeOnly finishTime)
    {
        WeekdayId = weekdayId;
        ClassroomId = classroomId;
        StartTime = startTime;
        FinishTime = finishTime;
    }

    public override string ToString()
    {
        return WeekdayId + " - " + ClassroomId + " - " + StartTime + " - " + FinishTime + " - ";
    }
}
