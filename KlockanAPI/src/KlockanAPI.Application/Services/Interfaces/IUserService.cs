using KlockanAPI.Application.DTOs.User;


namespace KlockanAPI.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
}
