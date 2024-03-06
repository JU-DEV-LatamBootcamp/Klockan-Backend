using KlockanAPI.Application.DTOs.Meeting;
using KlockanAPI.Domain.Models.Webex;

namespace KlockanAPI.Application.Services.Webex;

public static class WebexMeetingUtils
{
    public static string HostEmail { get; set; }

    public static Meeting ConvertToWebexMeeting(CreateMultipleMeetingsDto createMeetingDto, MeetingDetailsDTO meetingDetails)
    {
        List<Guest> invitees = meetingDetails.Users.Select(user => new Guest
        {
            email = user.Email,
            displayName = user.FullName,
            //coHost = user.Role == "Trainer" To be a co-host, the email must be linked.
        }).ToList();

        DateTime Start = createMeetingDto.Date.ToDateTime(createMeetingDto.Time);
        DateTime End = Start.AddMinutes(meetingDetails.Duration);
        string recurrence = $"FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU,WE,TH,FR;COUNT={meetingDetails.Sessions}";

        var meeting = new Meeting
        {
            title = $"{meetingDetails.CourseName} - {meetingDetails.ProgramName}",
            invitees = invitees,
            start = Start.ToString("yyyy-MM-ddTHH:mm:ss"),
            end = End.ToString("yyyy-MM-ddTHH:mm:ss"),
            timezone = "America/La_Paz",
            recurrence = recurrence,
            sendEmail = true,
            hostEmail = HostEmail
        };

        return meeting;
    }
}
