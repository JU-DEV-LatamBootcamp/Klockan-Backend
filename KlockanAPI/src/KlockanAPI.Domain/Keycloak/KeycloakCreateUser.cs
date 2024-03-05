namespace KlockanAPI.Domain.Keycloak;

public class KeycloakCreateUser
{
    public string username { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public bool enabled { get; set; }
    public string emailVerified { get; set; }
    public List<string> groups { get; set; }
    public List<Credential> credentials { get; set; }

}