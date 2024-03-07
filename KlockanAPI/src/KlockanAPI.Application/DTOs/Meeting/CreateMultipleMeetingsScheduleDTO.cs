namespace KlockanAPI.Application.DTOs.Meeting;

public class CreateMultipleMeetingsScheduleDTO
{
    public DateOnly StartDate { get; set; }
    public int Quantity { get; set; }
    public int ClassroomId { get; set; }
    public List<MeetingScheduleDTO> Schedules { get; set; } = new List<MeetingScheduleDTO>();

}
