using KlockanAPI.Application.Client;
using KlockanAPI.Application.KeycloakAPI.Interfaces;
using KlockanAPI.Domain.Keycloak;

using Microsoft.Extensions.Configuration;

namespace KlockanAPI.Application.KeycloakAPI;

public class KycloakAuthService : IKeycloakAuthService
{
    private readonly ICustomHttpClientService _customHttpClientService;
    private readonly IConfiguration _configuration;

    public KycloakAuthService(ICustomHttpClientService customHttpClientService, IConfiguration configuration)
    {
        _customHttpClientService = customHttpClientService;
        _configuration = configuration;
    }

    public async Task<Token> GetAdminToken()
    {
        string baseUrl = _configuration["KeyCloakAdmin:BaseUrl"]!;
        string adminTokenPath = _configuration["KeyCloakAdmin:AdminTokenPath"]!;
        string adminClientId = _configuration["KeyCloakAdmin:AdminClientId"]!;
        string adminUsername = _configuration["KeyCloakAdmin:AdminUsername"]!;
        string adminPassword = _configuration["KeyCloakAdmin:AdminPassword"]!;
        string grantType = "password";

        var httpClient = _customHttpClientService.GetCustomHttpClient();
        var bodyContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string?, string?>("client_id", adminClientId),
            new KeyValuePair<string?, string?>("username", adminUsername),
            new KeyValuePair<string?, string?>("password", adminPassword),
            new KeyValuePair<string?, string?>("grant_type", grantType)
        });

        HttpResponseMessage response = await httpClient.PostAsync($"{baseUrl}{adminTokenPath}", bodyContent);

        if(response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            Token token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(content)!;
            return token!;
        }
        else
        {
            throw new Exception($"Failed to get admin token. Status code: {response.StatusCode}");
        }

    }
}
