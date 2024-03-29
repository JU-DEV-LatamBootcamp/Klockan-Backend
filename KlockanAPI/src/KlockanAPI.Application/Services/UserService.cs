﻿using MapsterMapper;

using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.CrossCutting;


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

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.UserExistsByEmailAsync(email);
        return _mapper.Map<UserDto>(user!);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDTO createUserDTO)
    {
        var user = _mapper.Map<User>(createUserDTO);
        var userExists = await _userRepository.UserExistsByEmailAsync(user.Email);
        if(userExists != null)
        {
            throw new Exception("User already exists with this email");
        }
        var createdUser = await _userRepository.CreateUserAsync(user);
        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<UserDto> UpdateUserAsync(UserDto updateUserDTO)
    {
        var user = _mapper.Map<User>(updateUserDTO);
        var updatedUser = await _userRepository.UpdateUserAsync(user);
        return _mapper.Map<UserDto>(updatedUser);
    }
}
