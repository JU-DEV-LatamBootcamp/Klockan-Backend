using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace KlockanAPI.Infrastructure.CrossCutting.Authentication;

public static class KeyCloakAuthentication
{
    public static AuthenticationBuilder AddKeyCloakJWTAuthentication(this IServiceCollection services, IConfigurationSection KeyCloakSecrets, IHostEnvironment env)
    {
        return services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(async o =>
        {
            o.RequireHttpsMetadata = env.IsProduction();
            o.Authority = KeyCloakSecrets["Authority"];
            o.Audience = KeyCloakSecrets["Audience"];

            /*
            var jwk = new JsonWebKey(KeyCloakSecrets["Certs"]);
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = KeyCloakSecrets["Authority"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwk
            };
            o.TokenValidationParameters = tokenValidationParameters;
            */


            var certs = await GetCerts(KeyCloakSecrets["CertsUrl"]!);

            if(certs != null && certs.Length > 1)
            {
                var jwk = new JsonWebKey(certs[1]);
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = KeyCloakSecrets["Authority"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwk
                };
                o.TokenValidationParameters = tokenValidationParameters;
            }
            else
            {
                throw new Exception("No se pudieron obtener los certificados o no hay suficientes certificados disponibles.");
            }



            o.Events = new JwtBearerEvents()
            {
                OnTokenValidated = (TokenValidatedContext context) =>
                {
                    return Task.CompletedTask;
                }

                //validar token
                /*
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "text/plain";
                    if(env.IsDevelopment())
                    {
                        return c.Response.WriteAsJsonAsync(c.Exception.ToString());
                    }System.Net.Http.HttpRequestException: 'The SSL connection could not be established, see inner exception.'

                    return c.Response.WriteAsJsonAsync("An error occured processing your authentication.");
                }
                */
            };
        });
    }

    private static async Task<string[]> GetCerts(string certsUrl)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(certsUrl);
        Console.WriteLine(response);
        if(response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonArray = JsonConvert.DeserializeObject<string[]>(jsonString);
            return jsonArray!;
        }
        else
        {
            throw new Exception($"Error al obtener los certificados: {response.StatusCode}");
        }
    }

}
