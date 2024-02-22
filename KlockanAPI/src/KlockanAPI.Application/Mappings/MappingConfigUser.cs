using Mapster;

using KlockanAPI.Domain.Models;
using KlockanAPI.Application.DTOs.User;

namespace KlockanAPI.Application.Mappings;

public static class MappingConfigUser
{
    public static void ConfigureMappingUserUserDto()
    {
        TypeAdapterConfig<User, UserDto>.NewConfig()
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.Country, src => src.City.Country)
            .Map(dest => dest.Role, src => src.Role);
    }
}
