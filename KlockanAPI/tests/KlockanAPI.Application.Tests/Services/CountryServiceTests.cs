using Moq;
using NSubstitute;
using FluentAssertions;
using MapsterMapper;

using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Application.Services;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.DTOs.Country;

namespace KlockanAPI.Application.Tests;

public class CountryServiceTests
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    private readonly Mock<ICountryRepository> _countryRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public CountryServiceTests()
    {
        _countryRepository = Substitute.For<ICountryRepository>();
        _mapper = new Mapper();
    }

    private CountryService GetServiceInstance() => new(_countryRepository, _mapper);

    [Fact]
    public async Task GetCountriesAsync_ShouldReturnCountriesDTO()
    {
        var countryService = GetServiceInstance();

        List<Country> sampleCountries = new List<Country>
        {
            new Country { Id = 1, Name = "Argentina", Code = "AR" },
            new Country { Id = 2, Name = "Bolivia", Code = "BO" },
            new Country { Id = 3, Name = "Brazil", Code = "BR" },
            new Country { Id = 4, Name = "Chile", Code = "CL" }
        };

        _countryRepository.GetAllCountriesAsync().Returns(Task.FromResult<IEnumerable<Country>>(sampleCountries));

        var result = await countryService.GetCountries();

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(sampleCountries.Select(country => _mapper.Map<CountryDto>(country)));
        result.Should().HaveCount(sampleCountries.Count);
        result.Should().ContainItemsAssignableTo<CountryDto>();
    }
}
