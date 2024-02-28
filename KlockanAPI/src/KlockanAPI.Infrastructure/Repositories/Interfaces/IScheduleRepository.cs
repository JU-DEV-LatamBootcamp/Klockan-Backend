using KlockanAPI.Domain.Models;


namespace KlockanAPI.Infrastructure.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<IEnumerable<Schedule>> GetAllSchedulesByClassroomIdAsync(int id);
        Task<Schedule> CreateScheduleAsync(Schedule Schedule);
        Task<bool> CreateManySchedulesAsync(IEnumerable<Schedule> schedules);
    }
}
