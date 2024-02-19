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
}
