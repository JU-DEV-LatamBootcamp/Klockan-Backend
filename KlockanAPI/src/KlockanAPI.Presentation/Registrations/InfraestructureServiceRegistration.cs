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
                services.AddScoped<IScheduleRepository, ScheduleRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<ICountryRepository, CountryRepository>();
                services.AddScoped<IClassroomUserRepository, ClassroomUserRepository>();
                services.AddScoped<IMeetingAttendancesRepository, MeetingAttendancesRepository>();

                return services;
        }
}
