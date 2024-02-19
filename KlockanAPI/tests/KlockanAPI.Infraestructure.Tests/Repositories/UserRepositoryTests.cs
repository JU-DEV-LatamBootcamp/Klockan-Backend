using Microsoft.EntityFrameworkCore;

using FluentAssertions;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories;

namespace KlockanAPI.Infraestructure.Tests.Repositories;

public class UserRepositoryTests
{
    private readonly KlockanContext _context;

    public UserRepositoryTests()
    {
        DbContextOptionsBuilder<KlockanContext> dbContextOptions = new DbContextOptionsBuilder<KlockanContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new KlockanContext(dbContextOptions.Options);
    }
    private UserRepository GetRepositoryInstance() => new UserRepository(_context);

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnUsers()
    {
        // Arrange
        Random rnd = new Random();
        List<User> sampleUsers = new List<User>
        {
            new User
             {
                 Id = 1,
                 FirstName = "Martín",
                 LastName = "López",
                 Email = "martin.lopez@jala.university",
                 Avatar = $"https://randomuser.me/api/portraits/men/{rnd.Next(1, 99)}.jpg",
                 Birthdate = new DateOnly(1990, 5, 15),
                 CityId = 1,
                 RoleId = 1
             },
             new User
             {
                 Id = 2,
                 FirstName = "Lucía",
                 LastName = "Martínez",
                 Email = "lucia.martinez@jala.university",
                 Avatar = $"https://randomuser.me/api/portraits/women/{rnd.Next(1, 99)}.jpg",
                 Birthdate = new DateOnly(1988, 8, 20),
                 CityId = 3,
                 RoleId = 2
             },
             new User
             {
                 Id = 3,
                 FirstName = "Carlos",
                 LastName = "Gutiérrez",
                 Email = "carlos.gutierrez@jala.university",
                 Avatar = $"https://randomuser.me/api/portraits/men/{rnd.Next(1, 99)}.jpg",
                 Birthdate = new DateOnly(1992, 3, 10),
                 CityId = 2,
                 RoleId = 2
             }
        };
        List<Role> sampleRoles = new List<Role>
        {
            new Role
            {
                Id = 1,
                Name = "Student"
            },
            new Role
            {
                Id = 2,
                Name = "Teacher"
            }
        };
        List<Country> sampleCountries = new List<Country>
        {
            new Country
            {
                Id = 1,
                Name = "Bolivia"
            },
            new Country
            {
                Id = 2,
                Name = "Argentina"
            }
        };
        List<City> sampleCities = new List<City>
        {
            new City
            {
                Id = 1,
                Name = "La Paz",
                CountryId = 1
            },
            new City
            {
                Id = 2,
                Name = "Cochabamba",
                CountryId = 1
            },
            new City
            {
                Id = 3,
                Name = "Buenos Aires",
                CountryId = 2
            }
        };

        _context.Countries.AddRange(sampleCountries);
        _context.Cities.AddRange(sampleCities);
        _context.Roles.AddRange(sampleRoles);
        _context.Users.AddRange(sampleUsers);

        await _context.SaveChangesAsync();

        var userRepository = GetRepositoryInstance();

        // Act
        var result = await userRepository.GetAllUsersAsync();

        // Assert
        result.Should().BeEquivalentTo(sampleUsers);
        result.Should().HaveCount(sampleUsers.Count);
        result.Should().Equal(sampleUsers);
        result.First().FirstName.Should().Be(sampleUsers.First().FirstName);
        result.First().LastName.Should().Be(sampleUsers.First().LastName);
    }
}
