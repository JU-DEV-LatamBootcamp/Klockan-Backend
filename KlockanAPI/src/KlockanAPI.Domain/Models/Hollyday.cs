namespace KlockanAPI.Domain.Models;

public class Hollyday(DateOnly date, string description) : BaseModel
{
    public DateOnly Date { get; set; } = date;
    public string Description { get; set; } = description;

}