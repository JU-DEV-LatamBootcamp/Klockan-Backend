using Mapster;
using MapsterMapper;

using Microsoft.Extensions.DependencyInjection;

namespace KlockanAPI.Application.Mappings;

public static class MappingRegistrations
{
    public static IServiceCollection AddMappingRegistrations(this IServiceCollection services)
    {
        MappingConfigUser.ConfigureMappingUserUserDto();

        return services;
    }
}
