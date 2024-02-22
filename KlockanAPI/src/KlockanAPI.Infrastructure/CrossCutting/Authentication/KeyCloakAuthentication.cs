﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;

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
            o.Authority = KeyCloakSecrets!["Authority"]!;
            o.Audience = KeyCloakSecrets!["Audience"]!;

            var certs = await GetCerts(KeyCloakSecrets!["CertsUrl"]!);

            // Obtener los certificados
            if(certs != null)
            {
                // Obtener el token actual
                o.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var token = context.SecurityToken as JwtSecurityToken;
                        if(token != null)
                        {
                            var jit = token.Payload["jit"] as string;

                            foreach(var cert in certs)
                            {
                                var jwk = new JsonWebKey(cert);
                                var validationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = jwk,
                                    ValidateIssuer = true,
                                    ValidateAudience = false
                                };

                                var tokenHandler = new JwtSecurityTokenHandler();
                                SecurityToken validatedToken;
                                try
                                {
                                    tokenHandler.ValidateToken(jit, validationParameters, out validatedToken);

                                    context.Options.TokenValidationParameters.IssuerSigningKey = jwk;
                                    break;
                                }
                                catch(SecurityTokenSignatureKeyNotFoundException)
                                {
                                    continue;
                                }
                                catch(SecurityTokenInvalidSignatureException)
                                {
                                    continue;
                                }
                            }
                        }

                        return Task.CompletedTask;
                    }
                };
            }
            else
            {
                throw new Exception("No se pudieron obtener los certificados o no hay certificados disponibles.");
            }



            o.Events = new JwtBearerEvents()
            {
                OnTokenValidated = (TokenValidatedContext context) =>
                {
                    return Task.CompletedTask;
                }

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
            var jsonObj = JsonConvert.DeserializeObject<JObject>(jsonString);
            var keysArray = jsonObj!["keys"]!.ToString();
            return new string[] { keysArray };
        }
        else
        {
            throw new Exception($"Error al obtener los certificados: {response.StatusCode}");
        }

    }
}
