using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using KlockanAPI.Domain.Models;

namespace KlockanAPI.Integration.Tests;

public class ProgramsControllerTest : IClassFixture<KlockanApiFactory>
{
    public readonly HttpClient _client;
    public ProgramsControllerTest(KlockanApiFactory factory)
    {
        factory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/api/v1/programs");
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPrograms_ReturnsOk()
    {
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
