
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IScheduleService
{
    Task<List<ScheduleDTO>> GetSchedulesByClassroomIdAsync(int classroomId);
}