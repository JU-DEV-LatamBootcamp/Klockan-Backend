using KlockanAPI.Application.DTOs.Meeting;
using KlockanAPI.Domain.Models.Webex;

namespace KlockanAPI.Application.Services.Webex;

public static class WebexMeetingUtils
{
    public static string HostEmail { get; set; }

    public static Meeting ConvertToWebexMeeting(CreateMeetingDto createMeetingDto, MeetingDetailsDTO meetingDetails)
    {
        List<Guest> invitees = meetingDetails.Users.Select(user => new Guest
        {
            email = user.Email,
            displayName = user.FullName,
        }).ToList();

        DateTime Start = createMeetingDto.Date.ToDateTime(createMeetingDto.Time);
        DateTime End = Start.AddMinutes(meetingDetails.Duration);

        var meeting = new Meeting
        {
            title = $"{meetingDetails.CourseName} - {meetingDetails.ProgramName}",
            invitees = invitees,
            start = Start.ToString("yyyy-MM-ddTHH:mm:ss"),
            end = End.ToString("yyyy-MM-ddTHH:mm:ss"),
            timezone = "America/La_Paz",
            sendEmail = true,
            hostEmail = HostEmail
        };

        return meeting;
    }
}
