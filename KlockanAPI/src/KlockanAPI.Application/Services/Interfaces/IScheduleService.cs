
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync();

    Task<List<ScheduleDTO>> CreateScheduleAsync(List<CreateScheduleDTO> createScheduleDTO, int id);

    Task<ScheduleDTO> CreateSAsync(CreateScheduleDTO createScheduleDTO);

}
