using KlockanAPI.Domain.Models;


namespace KlockanAPI.Infrastructure.Repositories.Interfaces
{
    public interface IScheduleRepository
    {

        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
  
        Task<Schedule> CreateScheduleAsync(Schedule Schedule);
    }
}
