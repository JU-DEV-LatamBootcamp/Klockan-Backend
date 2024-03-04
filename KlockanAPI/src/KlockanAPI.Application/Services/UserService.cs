using MapsterMapper;

using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;


namespace KlockanAPI.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(int pageSize, int pageNumber)
    {
        var users = await _userRepository.GetAllUsersAsync(pageSize, pageNumber);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDTO createUserDTO)
    {
        var user = _mapper.Map<User>(createUserDTO);
        var createdUser = await _userRepository.CreateUserAsync(user);
        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDTO updateUserDto)
    {
        var user = _mapper.Map<User>(updateUserDto);

        var existingUser = await _userRepository.GetUserByIdAsync(user.Id);


        if (existingUser == null)
        {
            throw new Exception($"User not found");
        }

        existingUser.FirstName = updateUserDto.FirstName;
        existingUser.LastName = updateUserDto.LastName;
        existingUser.Birthdate = updateUserDto.Birthdate;

        var updatedUser = await _userRepository.UpdateUserAsync(existingUser);

        return _mapper.Map<UserDto>(updatedUser);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            throw new Exception($"User not found");
        }

        return _mapper.Map<UserDto>(user);
    }


}
