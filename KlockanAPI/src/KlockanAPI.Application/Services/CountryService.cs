using KlockanAPI.Application.DTOs.Country;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using MapsterMapper;

namespace KlockanAPI.Application.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRespository;
    private readonly IMapper _mapper;
    public CountryService(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRespository = countryRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<CountryDto>> GetCountries()
    {
        var countries = await _countryRespository.GetAllCountriesAsync();
        return _mapper.Map<IEnumerable<CountryDto>>(countries);
    }
}
