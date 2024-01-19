namespace KlockanAPI.Domain.Models;

public class City(int id, string name, string code, Country country, DateTime createdAt, DateTime updatedAt, DateTime deletedAt)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Code { get; set; } = code;
    public Country Country { get; set; } = country;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public DateTime DeletedAt { get; set; } = deletedAt;
}