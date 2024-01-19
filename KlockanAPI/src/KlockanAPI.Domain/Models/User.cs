namespace KlockanAPI.Domain.Models;

public class User(
    string firstName,
    string lastName,
    string email,
    DateOnly? birthdate,
    City? city,
    Country? country,
    Role role,
    Meeting[]? meetings,
    Classroom? classrooms
    ) : BaseModel
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
    public DateOnly? birthdate { get; set; } = birthdate;
    public City? City { get; set; } = city;
    public Country? Country { get; set; } = country;
    public Role Role { get; set; } = role;
    public Meeting[]? Meetings { get; set; } = meetings;
    public Classroom? Classrooms { get; set; } = classrooms;
}
