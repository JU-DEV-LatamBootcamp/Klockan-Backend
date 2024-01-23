namespace KlockanAPI.Domain.Models;

public class Weekday : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
