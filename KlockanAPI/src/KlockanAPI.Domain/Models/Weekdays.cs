namespace KlockanAPI.Domain.Models;

public class Weekdays(string name) : BaseModel
{
    public string Name { get; set; } = name;
}