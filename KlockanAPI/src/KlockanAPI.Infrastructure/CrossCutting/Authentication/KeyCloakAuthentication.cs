using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KlockanAPI.Infrastructure.CrossCutting.Authentication;

public static class KeyCloakAuthentication
{
    public static AuthenticationBuilder AddKeyCloakJWTAuthentication(this IServiceCollection services, IConfigurationSection KeyCloakSecrets, IHostEnvironment env)
    {
        return services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = env.IsProduction();
            o.Authority = KeyCloakSecrets["Authority"];
            o.Audience = KeyCloakSecrets["Audience"];


            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = KeyCloakSecrets["Authority"],
                ValidateIssuerSigningKey = true,
                //IssuerSigningKey = new JsonWebKey(KeyCloakSecrets["CertsUrl"]),
                IssuerSigningKey = new JsonWebKey(KeyCloakSecrets["Certs"]),

            };
            o.TokenValidationParameters = tokenValidationParameters;

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
                    }
                    return c.Response.WriteAsJsonAsync("An error occured processing your authentication.");
                }
                */
            };
        });
    }
}
