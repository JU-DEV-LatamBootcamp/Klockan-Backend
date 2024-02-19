using Microsoft.EntityFrameworkCore;

using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Data.Seeders;

public static class CitySeeder
{
    public static ModelBuilder SeedCities(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(
            // Argentina
            new City { Id = 1, Name = "Buenos Aires", Code = "BA", CountryId = 1 },
            new City { Id = 2, Name = "Córdoba", Code = "COR", CountryId = 1 },
            new City { Id = 3, Name = "Rosario", Code = "ROS", CountryId = 1 },
            new City { Id = 4, Name = "Mendoza", Code = "MDZ", CountryId = 1 },
            new City { Id = 5, Name = "San Miguel de Tucumán", Code = "TUC", CountryId = 1 },
            new City { Id = 6, Name = "La Plata", Code = "LP", CountryId = 1 },
            new City { Id = 7, Name = "Mar del Plata", Code = "MDQ", CountryId = 1 },
            new City { Id = 8, Name = "Salta", Code = "SLA", CountryId = 1 },
            new City { Id = 9, Name = "Santa Fe", Code = "SFN", CountryId = 1 },
            new City { Id = 10, Name = "San Juan", Code = "SJU", CountryId = 1 },
            new City { Id = 11, Name = "Tucumán", Code = "TUC", CountryId = 1 },
            new City { Id = 12, Name = "Neuquén", Code = "NEU", CountryId = 1 },
            new City { Id = 13, Name = "Bahía Blanca", Code = "BHI", CountryId = 1 },
            new City { Id = 14, Name = "Resistencia", Code = "RES", CountryId = 1 },
            new City { Id = 15, Name = "Formosa", Code = "FOR", CountryId = 1 },
            new City { Id = 16, Name = "Corrientes", Code = "COR", CountryId = 1 },
            new City { Id = 17, Name = "Posadas", Code = "POS", CountryId = 1 },
            new City { Id = 18, Name = "San Salvador de Jujuy", Code = "JUJ", CountryId = 1 },
            new City { Id = 19, Name = "Paraná", Code = "PAR", CountryId = 1 },
            new City { Id = 20, Name = "Santa Rosa", Code = "SRO", CountryId = 1 },

            // Bolivia
            new City { Id = 21, Name = "La Paz", Code = "LP", CountryId = 2 },
            new City { Id = 22, Name = "Santa Cruz de la Sierra", Code = "SCZ", CountryId = 2 },
            new City { Id = 23, Name = "Cochabamba", Code = "CBBA", CountryId = 2 },
            new City { Id = 24, Name = "Sucre", Code = "SUC", CountryId = 2 },
            new City { Id = 25, Name = "Tarija", Code = "TJA", CountryId = 2 },
            new City { Id = 26, Name = "Potosí", Code = "PTS", CountryId = 2 },
            new City { Id = 27, Name = "Oruro", Code = "ORU", CountryId = 2 },
            new City { Id = 28, Name = "Cobija", Code = "CBI", CountryId = 2 },
            new City { Id = 29, Name = "Trinidad", Code = "TRI", CountryId = 2 },
            new City { Id = 30, Name = "Riberalta", Code = "RIB", CountryId = 2 },

            // Brazil
            new City { Id = 31, Name = "São Paulo", Code = "SP", CountryId = 3 },
            new City { Id = 32, Name = "Rio de Janeiro", Code = "RJ", CountryId = 3 },

            // Chile
            new City { Id = 33, Name = "Santiago", Code = "SCL", CountryId = 4 },
            new City { Id = 34, Name = "Valparaíso", Code = "VAL", CountryId = 4 },

            // Colombia
            new City { Id = 35, Name = "Bogotá", Code = "BOG", CountryId = 5 },
            new City { Id = 36, Name = "Medellín", Code = "MDE", CountryId = 5 },

            // Costa Rica
            new City { Id = 37, Name = "San José", Code = "SJ", CountryId = 6 },
            new City { Id = 38, Name = "Limon", Code = "LM", CountryId = 6 },

            // Cuba
            new City { Id = 39, Name = "La Habana", Code = "LH", CountryId = 7 },
            new City { Id = 40, Name = "Santiago de Cuba", Code = "SC", CountryId = 7 },

            // Dominican Republic
            new City { Id = 41, Name = "Santo Domingo", Code = "SD", CountryId = 8 },
            new City { Id = 42, Name = "Santiago de los Caballeros", Code = "SC", CountryId = 8 },

            // Ecuador
            new City { Id = 43, Name = "Quito", Code = "QT", CountryId = 9 },
            new City { Id = 44, Name = "Guayaquil", Code = "GQ", CountryId = 9 },

            // El Salvador
            new City { Id = 45, Name = "San Salvador", Code = "SS", CountryId = 10 },
            new City { Id = 46, Name = "Santa Ana", Code = "SA", CountryId = 10 },

            // Guatemala
            new City { Id = 47, Name = "Guatemala City", Code = "GC", CountryId = 11 },
            new City { Id = 48, Name = "Quetzaltenango", Code = "QZ", CountryId = 11 },

            // Haiti
            new City { Id = 49, Name = "Port-au-Prince", Code = "PA", CountryId = 12 },
            new City { Id = 50, Name = "Cap-Haïtien", Code = "CH", CountryId = 12 },

            // Honduras
            new City { Id = 51, Name = "Tegucigalpa", Code = "TG", CountryId = 13 },
            new City { Id = 52, Name = "San Pedro Sula", Code = "SPS", CountryId = 13 },

            // Mexico
            new City { Id = 53, Name = "Mexico City", Code = "MXC", CountryId = 14 },
            new City { Id = 54, Name = "Guadalajara", Code = "GDL", CountryId = 14 },

            // Nicaragua
            new City { Id = 55, Name = "Managua", Code = "MN", CountryId = 15 },
            new City { Id = 56, Name = "León", Code = "LN", CountryId = 15 },

            // Panama
            new City { Id = 57, Name = "Panama City", Code = "PC", CountryId = 16 },
            new City { Id = 58, Name = "David", Code = "DV", CountryId = 16 },

            // Paraguay
            new City { Id = 59, Name = "Asunción", Code = "ASU", CountryId = 17 },
            new City { Id = 60, Name = "Ciudad del Este", Code = "CDE", CountryId = 17 },

            // Peru
            new City { Id = 61, Name = "Lima", Code = "LM", CountryId = 18 },
            new City { Id = 62, Name = "Arequipa", Code = "AR", CountryId = 18 },

            // Puerto Rico
            new City { Id = 63, Name = "San Juan", Code = "SJ", CountryId = 19 },
            new City { Id = 64, Name = "Ponce", Code = "PC", CountryId = 19 },

            // Uruguay
            new City { Id = 65, Name = "Montevideo", Code = "MTV", CountryId = 20 },
            new City { Id = 66, Name = "Salto", Code = "ST", CountryId = 20 },

            // Venezuela
            new City { Id = 67, Name = "Caracas", Code = "CCS", CountryId = 21 },
            new City { Id = 68, Name = "Maracaibo", Code = "MAR", CountryId = 21 }
        );

        return modelBuilder;
    }
}
