using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using NSubstitute;

using KlockanAPI.Application.Client;
using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Application.KeycloakAPI.Interfaces;
using KlockanAPI.Application.KeycloakAPI;
using KlockanAPI.Domain.Keycloak;
using KlockanAPI.Domain.Models;

namespace KlockanAPI.Application.Tests.KeycloakAPI;

public class KeycloakUserServiceTests
{

    [Fact]
    public async Task CreateUserAsync_WithInvalidUserAndToken_ShouldReturnFalse()
    {
        // Arrange
        var userDTO = new UserDto(); // Invalid user
        var adminToken = new Token(); // Assuming Token class is properly defined
        var mockCustomHttpClientService = Substitute.For<ICustomHttpClientService>();
        var keycloakUserService = new KeycloakUserService(
            mockCustomHttpClientService,
            Substitute.For<IConfiguration>()
        );

        // Act
        var result = await keycloakUserService.CreateUserAsync(userDTO, adminToken);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetAdminToken_WithInvalidCredentials_ShouldThrowException()
    {
        // Arrange
        var mockCustomHttpClientService = Substitute.For<ICustomHttpClientService>();
        mockCustomHttpClientService.GetCustomHttpClient().Returns(new HttpClient(new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.Unauthorized))));
        var configuration = Substitute.For<IConfiguration>();
        configuration["KeyCloakAdmin:BaseUrl"].Returns("http://example.com");
        configuration["KeyCloakAdmin:AdminTokenPath"].Returns("/admin/token");
        configuration["KeyCloakAdmin:AdminClientId"].Returns("adminClientId");
        configuration["KeyCloakAdmin:AdminUsername"].Returns("adminUsername");
        configuration["KeyCloakAdmin:AdminPassword"].Returns("adminPassword");
        var keycloakAuthService = new KycloakAuthService(mockCustomHttpClientService, configuration);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => keycloakAuthService.GetAdminToken());
    }

    // FakeHttpMessageHandler implementation for mocking HttpClient requests
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public FakeHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }
}
