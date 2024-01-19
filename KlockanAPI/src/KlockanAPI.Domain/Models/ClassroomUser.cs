namespace KlockanAPI.Domain.Models;

public class ClassroomUser(int id, Classroom classroom, User user, Role ClassroomRole, DateTime createdAt, DateTime updatedAt, DateTime deletedAt)
{
    public int Id { get; set; } = id;
    public Classroom Classroom { get; set; } = classroom;
    public User User { get; set; } = user;
    public Role ClassroomRole { get; set; } = ClassroomRole;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public DateTime DeletedAt { get; set; } = deletedAt;
}