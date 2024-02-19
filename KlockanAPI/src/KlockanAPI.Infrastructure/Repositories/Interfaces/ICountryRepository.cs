using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
}
