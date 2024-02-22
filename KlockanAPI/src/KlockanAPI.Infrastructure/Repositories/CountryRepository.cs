using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infrastructure;

public class CountryRepository : ICountryRepository
{
    private readonly KlockanContext _context;
    public CountryRepository(KlockanContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        var countries = await Task.FromResult(_context.Countries.AsNoTracking().Include(c => c.Cities).ToList());
        return countries;
    }
}

