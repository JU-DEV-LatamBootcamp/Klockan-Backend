namespace KlockanAPI.Domain.Models;

public class Classroom(
    Course course,
    DateOnly startDate,
    User creator,
    Program program,
    Meeting[] meetings,
    ClassroomUser[] classroomUsers,
    Schedule schedule
) : BaseModel
{

    public Course Course { get; set; } = course;
    public DateOnly StartDate { get; set; } = startDate;
    public User Creator { get; set; } = creator;
    public Program Program { get; set; } = program;
    public Meeting[]? Meetings { get; set; } = meetings;
    public ClassroomUser[]? ClassroomUsers { get; set; } = classroomUsers;
    public Schedule? Schedule { get; set; } = schedule;

}
