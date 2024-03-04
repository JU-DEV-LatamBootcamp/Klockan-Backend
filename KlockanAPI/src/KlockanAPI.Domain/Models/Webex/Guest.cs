namespace KlockanAPI.Domain.Models.Webex;

public class Guest
{
    public string email;
    public string displayName;
    public bool coHost { get; set; } = false; 
    public bool panelist { get; set; } = false;
}
