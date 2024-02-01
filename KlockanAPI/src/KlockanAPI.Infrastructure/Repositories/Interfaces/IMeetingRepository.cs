using KlockanAPI.Domain.Models;
namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IMeetingRepository
{
    Task<IEnumerable<Meeting>> GetAllAsync();
}