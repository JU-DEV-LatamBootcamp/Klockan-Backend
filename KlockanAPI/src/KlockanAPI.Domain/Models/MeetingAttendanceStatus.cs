namespace KlockanAPI.Domain.Models;

public class MeetingAttendanceStatus(string name) : BaseModel
{
    public string Name { get; set; } = name;
}