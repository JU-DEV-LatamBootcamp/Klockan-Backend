using Microsoft.AspNetCore.Mvc;

using Moq;
using NSubstitute;
using FluentAssertions;
using MapsterMapper;

using KlockanAPI.Application.Services;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.DTOs.Program;
using KlockanAPI.Application.DTOs.User;

namespace KlockanAPI.Application.Tests.Services;

public class UserServiceTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public UserServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = new Mapper();
    }

    private UserService GetServiceInstance() => new(_userRepository, _mapper);

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnUserDTOs()
    {
        // Arrange
        Random rnd = new Random();
        var userService = GetServiceInstance();

        // Define some sample users from the repository
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
                 CityId = 21, // ID de una ciudad de Bolivia según el seeder de ciudades
                 RoleId = 2
             }
        };

        _userRepository.GetAllUsersAsync().Returns(Task.FromResult<IEnumerable<User>>(sampleUsers));

        // Act
        var result = await userService.GetAllUsersAsync();

        // Assert
        result.Should().NotBeNull();

        // Use BeEquivalentTo to compare elements and properties without requiring exact type matching
        result.Should().BeEquivalentTo(sampleUsers, options => options.ExcludingMissingMembers());

        // Check if the number of items in the result matches the number of items in the samplePrograms
        result.Should().HaveCount(sampleUsers.Count);

        // Ensure that each item in the result is of the expected type ProgramDTO
        result.Should().ContainItemsAssignableTo<UserDto>();
    }
}
