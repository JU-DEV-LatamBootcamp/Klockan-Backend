namespace KlockanAPI.Domain.Entities.Interfaces;
public interface IEntity {
    int Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    DateTime DeletedAt { get; set; }
}
