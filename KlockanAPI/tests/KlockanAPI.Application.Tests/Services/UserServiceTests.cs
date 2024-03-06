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
        int pageSize = 10;
        int pageNumber = 1;
        UserService userService = GetServiceInstance();

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

        _userRepository.GetAllUsersAsync(pageSize, pageNumber).Returns(Task.FromResult<IEnumerable<User>>(sampleUsers));

        // Act
        var result = await userService.GetAllUsersAsync(pageSize, pageNumber);

        // Assert
        result.Should().NotBeNull();

        // Use BeEquivalentTo to compare elements and properties without requiring exact type matching
        result.Should().BeEquivalentTo(sampleUsers, options => options.ExcludingMissingMembers());

        // Check if the number of items in the result matches the number of items in the samplePrograms
        result.Should().HaveCount(sampleUsers.Count);

        // Ensure that each item in the result is of the expected type ProgramDTO
        result.Should().ContainItemsAssignableTo<UserDto>();
    }
    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnUsersPaginated()
    {
        // Arrange
        Random rnd = new Random();
        var pageSize = 1;
        var pageNumber = 2;
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

        _userRepository.GetAllUsersAsync(pageSize, pageNumber)
            .Returns(Task.FromResult<IEnumerable<User>>(sampleUsers.Skip((pageNumber - 1) * pageSize).Take(pageSize)));

        // Act
        var result = await userService.GetAllUsersAsync(pageSize, pageNumber);

        // Assert
        result.Should().NotBeNull();
        // Response count should be equal to the pageSize
        result.Should().HaveCount(pageSize);
        // Response should be equal to the second user in the sampleUsers list
        result.Should().BeEquivalentTo(sampleUsers.Skip(pageSize).Take(pageSize), options => options.ExcludingMissingMembers());
        // Ensure that each item in the result is of the expected type ProgramDTO
        result.Should().ContainItemsAssignableTo<UserDto>();
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnUserDTO_WhenCreateIsSuccessful()
    {
        var createUserDTO = new CreateUserDTO
        {
            FirstName = "Create",
            LastName = "Dto",
            Email = "test_create@dto.com",
            Birthdate = new DateOnly(1990, 1, 1),
            CityId = 1,
            RoleId = 1,

        };

        var user = new User
        {
            Id = 1,
            FirstName = "Test",
            LastName = "User",
            Email = "test@user.com",
            Birthdate = new DateOnly(1991, 1, 2),
            CityId = 14,
            RoleId = 2
        };

        var userDTO = new UserDto
        {
            Id = 2,
            FirstName = "User",
            LastName = "Dto",
            Email = "test_user@dto.com",
            Birthdate = new DateOnly(1992, 1, 3)
        };

        _mapperMock.Setup(m => m.Map<User>(It.IsAny<CreateUserDTO>())).Returns(user);
        _userRepositoryMock.Setup(repo => repo.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(user);
        _mapperMock.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(userDTO);

        var service = new UserService(_userRepositoryMock.Object, _mapperMock.Object);

        var result = await service.CreateUserAsync(createUserDTO);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(userDTO, result);
        Assert.Equal(userDTO.FirstName, result.FirstName);
        Assert.Equal(userDTO.LastName, result.LastName);
        _userRepositoryMock.Verify(repo => repo.CreateUserAsync(It.IsAny<User>()), Times.Once);
        _mapperMock.Verify(m => m.Map<UserDto>(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldReturnUpdatedUserDTO()
    {
        // Arrange
        var userService = GetServiceInstance();

        var initialUser = new User
        {
            Id = -1,
            FirstName = "Initial",
            LastName = "User",
            Email = "initial@user.com",
            Birthdate = new DateOnly(1990, 5, 15),
            RoleId = 1,
            CityId = 1,
            CreatedAt = DateTime.UtcNow
        };

        _userRepository.GetUserByIdAsync(-1).Returns(Task.FromResult<User?>(initialUser));


        var updatedUser = new User
        {
            Id = -1,
            FirstName = "Updated",
            LastName = "User",
            Email = "initial@user.com",
            Birthdate = new DateOnly(1990, 5, 15),
            RoleId = 1,
            CityId = 1,
            CreatedAt = DateTime.UtcNow
        };

        var updatedUserDto = new UserDto
        {
            Id = -1,
            FirstName = "Updated",
            LastName = "User",
            Email = "initial@user.com",
            Birthdate = new DateOnly(1990, 5, 15),
            RoleId = 1,
            CityId = 1
        };

        _userRepository
            .UpdateUserAsync(Arg.Any<User>())
            .Returns(callInfo =>
            {
                var userToUpdate = callInfo.ArgAt<User>(0);

                initialUser.FirstName = userToUpdate.FirstName;
                initialUser.UpdatedAt = DateTime.UtcNow;

                return Task.FromResult(initialUser);
            });

        // Act
        var result = await userService.UpdateUserAsync(updatedUserDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.Map<UserDto>(updatedUser));
    }
}
