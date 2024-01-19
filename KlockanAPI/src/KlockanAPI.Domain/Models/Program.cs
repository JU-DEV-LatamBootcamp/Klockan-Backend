namespace KlockanAPI.Domain.Models;

public class Program(string name, string description) : BaseModel
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

}
