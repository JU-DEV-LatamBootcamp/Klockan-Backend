namespace KlockanAPI.Domain.Models;

public class ClassroomUser(Classroom classroom, User user, Role ClassroomRole) : BaseModel
{
    public Classroom Classroom { get; set; } = classroom;
    public User User { get; set; } = user;
    public Role ClassroomRole { get; set; } = ClassroomRole;
}
