namespace KlockanAPI.Domain.Models;

public class Country : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string? Code { get; set; }
    public ICollection<City> Cities { get; set; } = new List<City>();
}
