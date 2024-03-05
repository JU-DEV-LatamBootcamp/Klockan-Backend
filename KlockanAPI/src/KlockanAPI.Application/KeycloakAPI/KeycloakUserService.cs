using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Application.KeycloakAPI.Interfaces;
using KlockanAPI.Domain.Keycloak;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KlockanAPI.Application.Client;
using KlockanAPI.Domain.Models;


namespace KlockanAPI.Application.KeycloakAPI;

public class KeycloakUserService : IKeycloakUserService
{
    private readonly ICustomHttpClientService _customHttpClient;
    private readonly IConfiguration _configuration;

    public KeycloakUserService(
        ICustomHttpClientService customHttpClient,
        IConfiguration configuration)
    {
        _customHttpClient = customHttpClient;
        _configuration = configuration;
    }


    public async Task<bool> CreateUserAsync(UserDto userDTO, Token adminToken)
    {
        try
        {
            var httpClient = _customHttpClient.GetCustomHttpClient();

            string roleGroup = userDTO.RoleId == Role.ADMIN_ID ? Role.ADMIN_NAME : Role.TRAINER_NAME;


            KeycloakCreateUser keyCloakUser = new KeycloakCreateUser
            {
                username = userDTO.Email,
                email = userDTO.Email,
                firstName = userDTO.FirstName,
                lastName = userDTO.LastName,
                enabled = true,
                groups = new List<string> { roleGroup },
                credentials = new List<Credential> {
                    new() { type = "password", value = "password", temporary = true }
                    }
            };

            var json = JsonConvert.SerializeObject(keyCloakUser);
            var response = new HttpResponseMessage();

            string baseUrl = _configuration["KeyCloakAdmin:BaseUrl"]!;
            string realm = _configuration["KeyCloakAdmin:Realm"]!;
            string requestUri = $"{baseUrl}/admin/realms/{realm}/users";
            httpClient.DefaultRequestHeaders.Add("Authorization", $"{adminToken.token_type} {adminToken.access_token}");

            response = await httpClient.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }
        catch(Exception ex)
        {
            return false;
            throw new Exception($"Error al crear usuario en Keycloak: {ex.Message}");
        }
    }
}
