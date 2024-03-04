
namespace KlockanAPI.Domain.Keycloak;

public interface User
{
    string username { get; set; }
    bool enabled { get; set; }
    Credential[] credentials { get; set; }
}
