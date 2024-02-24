
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync();
    Task<List<ScheduleDTO>> CreateScheduleAsync(List<CreateScheduleDTO> createScheduleDTO, int id);
    Task<ScheduleDTO> CreateScheduleAsync(CreateScheduleDTO createScheduleDTO);
    Task<List<ScheduleDTO>> GetSchedulesByClassroomIdAsync(int classroomId);
    Task<bool> UpdateOrCreateManySchedulesAsync(List<UpdateScheduleDTO> createScheduleDTOs);
    Task<bool> RemoveManySchedulesAsync(List<int> idsToDelete);
}
