namespace KlockanAPI.Domain.Models;

public class Class {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public int Sessions { get; set; }
    public int SessionDuration { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }

    public Class(int id, string name, string code, string description, int sessions, int sessionDuration) {
        this.Id = id;
        this.Name = name;
        this.Code = code;
        this.Description = description;
        this.Sessions = sessions;
        this.SessionDuration = sessionDuration;
    }

}
