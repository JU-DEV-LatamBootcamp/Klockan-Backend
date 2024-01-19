namespace KlockanAPI.Domain.Models;

public class User(
    int id,
    string firstName,
    string lastName,
    string email,
    DateOnly? birthdate,
    City? city,
    Country? country,
    Role role,
    Meeting[]? meetings,
    Classroom? classrooms,
    DateTime createdAt,
    DateTime updatedAt,
    DateTime deletedAt)
{
    public int Id { get; set; } = id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
    public DateOnly? birthdate { get; set; } = birthdate;
    public City? City { get; set; } = city;
    public Country? Country { get; set; } = country;
    public Role Role { get; set; } = role;
    public Meeting[]? Meetings { get; set; } = meetings;
    public Classroom? Classrooms { get; set; } = classrooms;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public DateTime DeletedAt { get; set; } = deletedAt;
}