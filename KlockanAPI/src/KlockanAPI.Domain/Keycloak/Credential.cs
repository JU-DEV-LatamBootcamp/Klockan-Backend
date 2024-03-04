namespace KlockanAPI.Domain.Keycloak;

public class Credential
{
    public string type { get; set; }
    public string value { get; set; }
    public bool temporary { get; set; }
}