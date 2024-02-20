namespace KlockanAPI.Domain.Models;

public class User : BaseModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly Birthdate { get; set; }
    public int CityId { get; set; }
    public City? City { get; set; }
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public ICollection<ClassroomUser> ClassroomUsers { get; set; } = new List<ClassroomUser>();
}
