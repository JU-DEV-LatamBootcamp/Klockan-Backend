using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using KlockanAPI.Domain.Keycloak;
using KlockanAPI.Domain.Models;
using KlockanAPI.Integration.Tests.Helpers.Keycloak;


namespace KlockanAPI.Integration.Tests;

public class ProgramsControllerTest : IClassFixture<KlockanApiFactory>
{
    public readonly HttpClient _client;
    private readonly AuthServiceHelper _authServiceHelper;

    public ProgramsControllerTest(KlockanApiFactory factory)
    {
        factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/api/v1/programs");
        _client = factory.CreateClient();
        _authServiceHelper = new AuthServiceHelper();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetPrograms_ReturnsOk()
    {
        Token token = await _authServiceHelper.GetAdminToken();
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.access_token}");

        var response = await _client.GetStringAsync("");
        var programs = JsonSerializer.Deserialize<IEnumerable<Program>>(response);
        Console.WriteLine(response);

        foreach(var program in programs)
        {
            Console.WriteLine(program.Name);
        }

        Assert.NotNull(programs);
        Assert.NotEmpty(programs);
    }
}
