﻿using System.Reflection;
using Mapster;
using MapsterMapper;
using FluentValidation;

using KlockanAPI.Application.Services;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.Validators;


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
            .AddScoped<ICourseService, CourseService>()
            .AddScoped<IClassroomService, ClassroomService>()
            .AddScoped<IMeetingService, MeetingService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICountryService, CountryService>()
            .AddSingleton(config)
            .AddScoped<IMapper, ServiceMapper>();

        services
            .AddValidatorsFromAssemblyContaining<CreateCourseDTOValidator>()
            .AddValidatorsFromAssemblyContaining<CreateProgramDTOValidator>()
            .AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

        return services;
    }
}
