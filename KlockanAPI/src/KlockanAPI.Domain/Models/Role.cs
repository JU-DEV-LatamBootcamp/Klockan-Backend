namespace KlockanAPI.Domain.Models;

public class Role : BaseModel
{
    public static readonly int ADMIN_ID = 1;
    public static readonly int TRAINER_ID = 2;

    public string Name { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<ClassroomUser> ClassroomUsers { get; set; } = new List<ClassroomUser>();
}
