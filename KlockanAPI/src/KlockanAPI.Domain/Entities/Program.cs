using KlockanAPI.Domain.Entities.Interfaces;

namespace KlockanAPI.Domain.Entities;
public class Program(int id, string name, string description, DateTime createdAt, DateTime updatedAt, DateTime deletedAt) : IEntity {

    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public DateTime DeletedAt { get; set; } = deletedAt;
}

