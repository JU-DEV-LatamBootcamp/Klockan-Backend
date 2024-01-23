namespace KlockanAPI.Domain.Models;

public class Holiday : BaseModel
{
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
}
