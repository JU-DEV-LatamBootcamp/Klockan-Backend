namespace KlockanAPI.Domain.Models;

public class Course(string name, string code, string description, int sessions, int sessionDuration) : BaseModel
{
    public string Name { get; set; } = name;
    public string Code { get; set; } = code;
    public string Description { get; set; } = description;
    public int Sessions { get; set; } = sessions;
    public int SessionDuration { get; set; } = sessionDuration;

}
