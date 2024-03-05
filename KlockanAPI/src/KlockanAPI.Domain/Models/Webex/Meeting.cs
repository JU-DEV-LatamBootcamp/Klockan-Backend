namespace KlockanAPI.Domain.Models.Webex;

public class Meeting
{
    public string title;
    public string start;
    public string end;
    public string timezone;
    public string recurrence;
    public List<Guest> invitees;
    public bool sendEmail = true;
    public string hostEmail;
}
