
using KlockanAPI.Application.DTOs.User;

namespace KlockanAPI.Application.KeycloakAPI.Interfaces;

public interface IKeycloakUserService
{
    Task<bool> CreateUserAsync(UserDto createUserDTO);
}
