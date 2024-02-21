using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

using KlockanAPI.Presentation;
using KlockanAPI.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace KlockanAPI.Integration.Tests;

public class KlockanApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer? _postgreSqlContainer;

    public KlockanApiFactory()
    {
        _postgreSqlContainer = new PostgreSqlBuilder()
            .WithDatabase("klockan")
            .WithUsername("test")
            .WithPassword("test")
            .WithCleanUp(true)
            .WithImage("postgres:latest")
            .Build();
    }

    public async Task InitializeAsync()
    {
        if(_postgreSqlContainer is not null)
        {
            await _postgreSqlContainer.StartAsync();
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorType = typeof(DbContextOptions<KlockanContext>);
            var descriptor = services.SingleOrDefault(sd => sd.ServiceType == descriptorType);

            if(descriptor is not null)
            {
                services.Remove(descriptor);
            }

            Console.WriteLine("From testing " + _postgreSqlContainer!.GetConnectionString());

            services.AddDbContext<KlockanContext>(options =>
            {
                options
                    .UseNpgsql(_postgreSqlContainer!.GetConnectionString())
                    .EnableSensitiveDataLogging();
            });


        });
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _postgreSqlContainer!.DisposeAsync();
    }


}
