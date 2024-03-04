using System.Reflection;
using Mapster;
using MapsterMapper;
using FluentValidation;

using KlockanAPI.Application.Services;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.Validators;
using KlockanAPI.Application.KeycloakAPI.Interfaces;
using KlockanAPI.Application.KeycloakAPI;
using KlockanAPI.Application.Client;


namespace KlockanAPI.Application;
public static class ApplicactionServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Mapster configuration, this scans all custom configs
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services
            .AddHttpClient()
            .AddSingleton<ICustomHttpClientService, CustomHttpClient>()
            .AddScoped<IProgramService, ProgramService>()
            .AddScoped<ICourseService, CourseService>()
            .AddScoped<IClassroomService, ClassroomService>()
            .AddScoped<IMeetingService, MeetingService>()
            .AddScoped<IScheduleService, ScheduleService>()

            .AddScoped<IUserService, UserService>()
            .AddScoped<ICountryService, CountryService>()
            .AddSingleton(config)
            .AddScoped<IMapper, ServiceMapper>()
            .AddScoped<IKeycloakUserService, KeycloakUserService>()
            .AddScoped<IKeycloakAuthService, KycloakAuthService>();

        services
            .AddValidatorsFromAssemblyContaining<CreateCourseDTOValidator>()
            .AddValidatorsFromAssemblyContaining<CreateProgramDTOValidator>()
            .AddValidatorsFromAssemblyContaining<CreateUserDTOValidator>()
            .AddValidatorsFromAssemblyContaining<CreateClassroomDTOValidator>()
            .AddValidatorsFromAssemblyContaining<UpdateClassroomDTOValidator>()
            .AddValidatorsFromAssemblyContaining<UpdateScheduleDTOValidator>();

        return services;
    }
}
