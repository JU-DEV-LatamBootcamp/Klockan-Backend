using KlockanAPI.Application.DTOs.Country;

namespace KlockanAPI.Application.Services.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<CountryDto>> GetCountries();
}
