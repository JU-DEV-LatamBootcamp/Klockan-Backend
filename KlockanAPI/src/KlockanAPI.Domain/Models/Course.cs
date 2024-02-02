namespace KlockanAPI.Domain.Models;

public class Course : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? Sessions { get; set; }
    public int? SessionDuration { get; set; }
    public ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();
}
