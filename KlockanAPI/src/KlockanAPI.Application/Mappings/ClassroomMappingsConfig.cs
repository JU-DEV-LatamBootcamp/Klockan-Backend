using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Domain.Models;
using Mapster;

namespace KlockanAPI.Application.Mappings;

public class ClassroomMappingsConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<CreateClassroomDTO, Classroom>.NewConfig()
            .Map(
                dest => dest.Schedule,
                src => src.Schedule
                    .Select(schedule => new Schedule()
                    {
                        Id = schedule.Id,
                        StartTime = schedule.StartTime,
                        WeekdayId = schedule.WeekdayId,
                    })
            );

        TypeAdapterConfig<Classroom, List<Schedule>>
            .NewConfig()
            .MapWith(src => src.Schedule
                .Select(schedule => new Schedule()
                {
                    Id = schedule.Id,
                    ClassroomId = src.Id,
                    StartTime = schedule.StartTime,
                    WeekdayId = schedule.WeekdayId,
                })
                .ToList()
            );
    }
}
