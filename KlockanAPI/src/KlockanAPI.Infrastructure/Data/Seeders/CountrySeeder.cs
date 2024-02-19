using Microsoft.EntityFrameworkCore;

using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Data.Seeders;

public static class CountrySeeder
{
    public static ModelBuilder SeedCountries(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(
              new Country { Id = 1, Name = "Argentina", Code = "AR" },
              new Country { Id = 2, Name = "Bolivia", Code = "BO" },
              new Country { Id = 3, Name = "Brazil", Code = "BR" },
              new Country { Id = 4, Name = "Chile", Code = "CL" },
              new Country { Id = 5, Name = "Colombia", Code = "CO" },
              new Country { Id = 6, Name = "Costa Rica", Code = "CR" },
              new Country { Id = 7, Name = "Cuba", Code = "CU" },
              new Country { Id = 8, Name = "Dominican Republic", Code = "DO" },
              new Country { Id = 9, Name = "Ecuador", Code = "EC" },
              new Country { Id = 10, Name = "El Salvador", Code = "SV" },
              new Country { Id = 11, Name = "Guatemala", Code = "GT" },
              new Country { Id = 12, Name = "Haiti", Code = "HT" },
              new Country { Id = 13, Name = "Honduras", Code = "HN" },
              new Country { Id = 14, Name = "Mexico", Code = "MX" },
              new Country { Id = 15, Name = "Nicaragua", Code = "NI" },
              new Country { Id = 16, Name = "Panama", Code = "PA" },
              new Country { Id = 17, Name = "Paraguay", Code = "PY" },
              new Country { Id = 18, Name = "Peru", Code = "PE" },
              new Country { Id = 19, Name = "Puerto Rico", Code = "PR" },
              new Country { Id = 20, Name = "Uruguay", Code = "UY" },
              new Country { Id = 21, Name = "Venezuela", Code = "VE" }
          );

        return modelBuilder;
    }
}
