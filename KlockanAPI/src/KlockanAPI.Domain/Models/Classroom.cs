namespace KlockanAPI.Domain.Models;

public class Classroom(
    int id, string code,
    Course course,
    DateOnly startDate,
    User creator,
    Program program,
    Meeting[] meetings,
    ClassroomUser[] classroomUsers,
    Schedule schedule,
    DateTime createdAt,
    DateTime updatedAt,
    DateTime deletedAt)
{
    public int Id { get; set; } = id;
    public string Code { get; set; } = code;
    public Course Course { get; set; } = course;
    public DateOnly StartDate { get; set; } = startDate;
    public User Creator { get; set; } = creator;
    public Program Program { get; set; } = program;
    public Meeting[]? Meetings { get; set; } = meetings;
    public ClassroomUser[]? ClassroomUsers { get; set; } = classroomUsers;
    public Schedule? Schedule { get; set; } = schedule;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public DateTime DeletedAt { get; set; } = deletedAt;
}