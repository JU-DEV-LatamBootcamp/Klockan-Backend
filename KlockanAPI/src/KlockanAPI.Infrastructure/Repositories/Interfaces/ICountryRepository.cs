using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
}
