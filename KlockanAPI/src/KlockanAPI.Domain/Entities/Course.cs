using KlockanAPI.Domain.Entities.Interfaces;

namespace KlockanAPI.Domain.Entities;
public class Course(int id, string name, string code, string description, int sessions, int sessionDuration, DateTime createdAt, DateTime updatedAt, DateTime deletedAt) : IEntity {

    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Code { get; set; } = code;
    public string Description { get; set; } = description;
    public int? Sessions { get; set; } = sessions;
    public int? SessionDuration { get; set; } = sessionDuration;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public DateTime DeletedAt { get; set; } = deletedAt;
}