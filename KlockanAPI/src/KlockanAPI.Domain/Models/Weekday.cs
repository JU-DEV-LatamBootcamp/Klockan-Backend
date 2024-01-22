namespace KlockanAPI.Domain.Models;

public class Weekday(string name) : BaseModel
{
    public string Name { get; set; } = name;
}
