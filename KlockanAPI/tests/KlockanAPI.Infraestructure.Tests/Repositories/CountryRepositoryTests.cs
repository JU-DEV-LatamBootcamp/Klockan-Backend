using FluentAssertions;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure;
using KlockanAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infraestructure.Tests;

public class CountryRepositoryTests
{
    private readonly KlockanContext _context;

    public CountryRepositoryTests()
    {
        DbContextOptionsBuilder<KlockanContext> dbContextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new KlockanContext(dbContextOptions.Options);
    }

    private CountryRepository GetRepositoryInstance() => new(_context);

    [Fact]
    public async Task GetCountriesAsync_ShouldReturnCountriesDto()
    {

        List<Country> sampleCountries = new List<Country>
        {
            new Country { Id = 1, Name = "Argentina", Code = "AR" },
            new Country { Id = 2, Name = "Bolivia", Code = "BO" },
            new Country { Id = 3, Name = "Brazil", Code = "BR" },
            new Country { Id = 4, Name = "Chile", Code = "CL" }
        };
        _context.Countries.AddRange(sampleCountries);
        await _context.SaveChangesAsync();

        var _repository = GetRepositoryInstance();

        var result = await _repository.GetAllCountriesAsync();

        result.Should().BeEquivalentTo(sampleCountries);
        result.Should().HaveCount(sampleCountries.Count);
    }
}
