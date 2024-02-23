using KlockanAPI.Domain.Models.Webex;

namespace KlockanAPI.Application.Utils.Webex;
public static class WebexMeetingUtils
{
    public static async Task<Meeting> CreateMeetingAsync(
        DateTime date, string time, int classroomId, int trainerId, List<int> userIds)
    {
        string trainerEmail = "adrian.mendoza@jala.university";

        string courseName = "Frontend Development";
        string programName = "Bootcamp Developers 01";

        string title = $"{courseName} - {programName}";

        List<Invitees> invitees = new List<Invitees>()
        {
            new Invitees
            {
                email = "adriancracker@gmail.com",
                displayName = "Adrian Mendoza",
                coHost = false,
                panelist = false
            },
            new Invitees
            {
                email = "adrian.mendoza.dev@gmail.com",
                displayName = "Mendoza Adrian",
                coHost = false,
                panelist = false
            }
        };

        Meeting meetingDetails = new Meeting
        {
            title = title,
            start = new DateTime(date.Year, date.Month, date.Day,
                             int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), 0)
                             .ToString("yyyy-MM-ddTHH:mm:ss"),
            end = new DateTime(date.Year, date.Month, date.Day,
                           int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), 0)
                           .AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss"),
            timezone = "America/La_Paz",
            recurrence = "FREQ=DAILY;INTERVAL=1",
            invitees = invitees,
            sendEmail = true,
            hostEmail = trainerEmail
        };

        return meetingDetails;
    }
}