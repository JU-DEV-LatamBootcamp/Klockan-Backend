using KlockanAPI.Infrastructure.Repositories;
using KlockanAPI.Infrastructure.Repositories.Interfaces;

namespace KlockanAPI.Infrastructure;

public static class InfraestructureServiceRegistration
{
    public static IServiceCollection AddInfraestructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProgramRepository, ProgramRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<IClassroomRepository, ClassroomRepository>();
        return services;
    }
}
