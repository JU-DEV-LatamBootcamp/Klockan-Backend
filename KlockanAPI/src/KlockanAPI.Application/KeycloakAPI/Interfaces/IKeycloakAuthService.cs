using KlockanAPI.Domain.Keycloak;

namespace KlockanAPI.Application.KeycloakAPI.Interfaces;

public interface IKeycloakAuthService
{
    Task<Token> GetAdminToken();
}
