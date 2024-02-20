using KlockanAPI.Application.DTOs.City;
using KlockanAPI.Application.DTOs.Country;
using KlockanAPI.Application.DTOs.Role;

namespace KlockanAPI.Application.DTOs.User;

public class CreateUserDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly Birthdate { get; set; }
    public int? CityId { get; set; }
    public int? RoleId { get; set; }
}