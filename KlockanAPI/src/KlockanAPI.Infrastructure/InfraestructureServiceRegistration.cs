using KlockanAPI.Infrastructure.Repositories;
using KlockanAPI.Application.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace KlockanAPI.Infrastructure;

public static class InfraestructureServiceRegistration
{
    public static IServiceCollection AddInfraestructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProgramRepository, ProgramRepository>();

        return services;
    }
}


