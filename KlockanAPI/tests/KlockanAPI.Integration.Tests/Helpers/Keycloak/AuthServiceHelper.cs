using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KlockanAPI.Domain.Keycloak;
using KlockanAPI.Integration.Tests.Helpers.CustomClient;
namespace KlockanAPI.Integration.Tests.Helpers.Keycloak;

public class AuthServiceHelper
{
    public readonly ICustomHttpClientService _customHttpClientService;
    public readonly HttpClient _customHttpClient;

    public AuthServiceHelper()
    {
        _customHttpClientService = new CustomHttpClient();
        _customHttpClient = _customHttpClientService.GetCustomHttpClient();
    }

    public async Task<Token> GetAdminToken()
    {
        string uri = "https://localhost:8443/realms/Klockan/protocol/openid-connect/token";
        string adminClientId = "admin-cli";
        string adminUsername = "admin";
        string adminPassword = "admin";
        string grantType = "password";

        var bodyContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string?, string?>("client_id", adminClientId),
            new KeyValuePair<string?, string?>("username", adminUsername),
            new KeyValuePair<string?, string?>("password", adminPassword),
            new KeyValuePair<string?, string?>("grant_type", grantType)
        });

        HttpResponseMessage response = await _customHttpClient.PostAsync(uri, bodyContent);

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
