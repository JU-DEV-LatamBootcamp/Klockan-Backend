using KlockanAPI.Application.DTOs.User;


namespace KlockanAPI.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetAllUsersAsync(int pageSize, int pageNumber);
    Task<UserDto> CreateUserAsync(CreateUserDTO createUserDTO);
    Task<UserDto> UpdateUserAsync(UserDto updateUserDTO);
}
