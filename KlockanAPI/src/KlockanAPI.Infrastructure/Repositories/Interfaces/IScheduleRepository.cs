using KlockanAPI.Domain.Models;


namespace KlockanAPI.Infrastructure.Repositories.Interfaces
{
    ´public interface IScheduleRepository
    {

        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<IEnumerable<Schedule>?> GetSchedulesByCourseIdAsync(int courseId);
        Task<IEnumerable<Schedule>?> GetSchedulesByProgramIdAsync(int programId);
        Task<Schedule> CreateScheduleAsync(Schedule Schedule);
    }
}
