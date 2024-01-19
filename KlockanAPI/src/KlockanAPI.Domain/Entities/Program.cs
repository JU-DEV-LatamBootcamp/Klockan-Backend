using KlockanAPI.Domain.Entities.Interfaces;

namespace KlockanAPI.Domain.Entities;
public class Program : IEntity {

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }

    public Program(int id, string name, string description) {
        this.Id = id;
        this.Name = name;
        this.Description = description;
    }
}

