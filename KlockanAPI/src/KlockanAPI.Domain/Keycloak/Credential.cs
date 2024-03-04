namespace KlockanAPI.Domain.Keycloak;

public interface Credential
{
    string type { get; set; }
    string value { get; set; }
    bool temporary { get; set; }
}
