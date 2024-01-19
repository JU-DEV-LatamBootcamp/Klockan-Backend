namespace KlockanAPI.Domain.Models;

public class Schedule(int id, Weekdays[] weekdays, Classroom classroom, DateTime createdAt, DateTime updatedAt, DateTime deletedAt)
{
    public int Id { get; set; } = id;
    public Weekdays[] Weekdays { get; set; } = weekdays;
    public Classroom Classroom { get; set; } = classroom;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public DateTime DeletedAt { get; set; } = deletedAt;
}