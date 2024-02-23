namespace KlockanAPI.Domain.Models.Webex;

public class Meeting
{
    public string title;
    public string start;
    public string end;
    public string timezone;
    public string recurrence;
    public List<Invitees> invitees;
    public bool sendEmail;
    public string hostEmail;
}
