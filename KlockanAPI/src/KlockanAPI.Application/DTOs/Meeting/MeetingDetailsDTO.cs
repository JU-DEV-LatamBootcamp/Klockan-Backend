using KlockanAPI.Application.DTOs.User;

namespace KlockanAPI.Application.DTOs.Meeting;

public class MeetingDetailsDTO
{
    public string CourseName { get; set; } = string.Empty;
    public string ProgramName { get; set; } = string.Empty;
    public List<UserMeetingDTO> Users { get; set; } = new List<UserMeetingDTO>();
    public int Sessions { get; set; }
    public int Duration { get; set; }
}
