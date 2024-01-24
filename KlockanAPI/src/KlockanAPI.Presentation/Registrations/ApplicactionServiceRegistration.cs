﻿using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

using KlockanAPI.Application.Services;
using KlockanAPI.Application.Services.Interfaces;

namespace KlockanAPI.Application;
public static class ApplicactionServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Mapster configuration, this scans all custom configs
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services
            .AddScoped<IProgramService, ProgramService>()
            .AddSingleton(config)
            .AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}