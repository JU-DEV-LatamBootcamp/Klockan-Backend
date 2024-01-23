namespace KlockanAPI.Domain.Models;

public class Program : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } 
    public ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();
}
