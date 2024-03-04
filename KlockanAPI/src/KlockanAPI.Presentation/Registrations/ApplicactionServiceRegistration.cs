using System.Reflection;
using Mapster;
using MapsterMapper;
using FluentValidation;
using Microsoft.Extensions.Options;

using KlockanAPI.Application.Services;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.Validators;
using KlockanAPI.Application.Services.Webex;
using KlockanAPI.Domain.Models.Webex;


namespace KlockanAPI.Application;
public static class ApplicactionServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Mapster configuration, this scans all custom configs
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        // Bind Webex options
        var webexOptionsSection = configuration.GetSection("Webex");
        services.Configure<WebexOptions>(webexOptionsSection);

        // Register WebexService with HttpClient configured
        services.AddHttpClient<WebexService>(client =>
        {
            client.BaseAddress = new Uri(configuration["Webex:MeetingsApiUrl"]);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", configuration["Webex:AccessToken"]);
        });

        services
            .AddScoped<IProgramService, ProgramService>()
            .AddScoped<ICourseService, CourseService>()
            .AddScoped<IClassroomService, ClassroomService>()
            .AddScoped<IMeetingService, MeetingService>()
            .AddScoped<IScheduleService, ScheduleService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICountryService, CountryService>()
            .AddSingleton<WebexMeetingAdapter>()
            .AddSingleton<IThirdPartyMeeting>(serviceProvider =>
                MeetingServiceFactory.CreateMeetingService("Webex", serviceProvider))
            .AddSingleton(config)
            .AddScoped<IMapper, ServiceMapper>();

        // Register validators
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
