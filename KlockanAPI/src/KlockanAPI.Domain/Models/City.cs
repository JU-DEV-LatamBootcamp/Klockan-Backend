namespace KlockanAPI.Domain.Models;

public class City : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public Country Country { get; set; } = new Country();
    public ICollection<User> Users { get; set; } = new List<User>();
}
