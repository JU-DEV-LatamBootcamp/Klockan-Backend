using Asp.Versioning;
using KlockanAPI.Application.DTOs.Country;
using KlockanAPI.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KlockanAPI.Presentation;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;
    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetAllCountries()
    {
        var countries = await _countryService.GetCountries();
        return Ok(countries);
    }
}
