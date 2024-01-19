namespace KlockanAPI.Domain.Models;

public class Country(int id, string name, string code, City[] cities, DateTime createdAt, DateTime updatedAt, DateTime deletedAt)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Code { get; set; } = code;
    public City[] Cities { get; set; } = cities;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public DateTime DeletedAt { get; set; } = deletedAt;
}