using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Nodes;

namespace KlockanAPI.Infrastructure.CrossCutting.Authorization;

public class JwtTokenHelper
{
    public static bool HasRequiredRole(HttpContext httpContext, string requiredRole)
    {
        string jwtToken = GetJwtTokenFromHeaders(httpContext.Request.Headers);
        if (jwtToken == null)
        {
            // No token found in headers
            return false;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var token = tokenHandler.ReadJwtToken(jwtToken);

            // Check if the token contains the required role claim
            var realmAccessClaim = token.Claims.FirstOrDefault(c => c.Type == "realm_access");
            if (realmAccessClaim == null)
                return false;

            var realmAccessNode = JsonNode.Parse(realmAccessClaim.Value);
            if (realmAccessNode is null)
                return false;

            var rolesNode = realmAccessNode["roles"];
            if (rolesNode is null)
                return false;

            JsonArray roles = rolesNode.AsArray();
            return roles.Any(r => r!.GetValue<string>() == requiredRole);
        }
        catch (Exception)
        {
            // Token validation failed
            return false;
        }
    }

    private static string GetJwtTokenFromHeaders(IHeaderDictionary headers)
    {
        string authorizationHeader = headers["Authorization"];
        if (string.IsNullOrWhiteSpace(authorizationHeader))
        {
            // No Authorization header found
            return null;
        }

        // Check if the Authorization header contains a JWT bearer token
        if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            // Extract the JWT token
            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }

        return null;
    }
}
