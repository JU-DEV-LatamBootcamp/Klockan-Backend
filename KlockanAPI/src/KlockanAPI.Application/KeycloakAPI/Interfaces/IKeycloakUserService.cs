
using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Domain.Keycloak;

namespace KlockanAPI.Application.KeycloakAPI.Interfaces;

public interface IKeycloakUserService
{
    Task<bool> CreateUserAsync(UserDto userDTO, Token adminToken);
}
