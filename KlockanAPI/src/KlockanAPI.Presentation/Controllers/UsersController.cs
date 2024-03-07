using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

using KlockanAPI.Domain.Models;
using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.KeycloakAPI.Interfaces;
using KlockanAPI.Infrastructure.CrossCutting.Authorization;

namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IKeycloakUserService _keycloakUserService;
    private readonly IKeycloakAuthService _keycloakAuthService;

    public UsersController(IUserService userService, IKeycloakUserService keycloakUserService, IKeycloakAuthService keycloakAuthService)
    {
        _userService = userService;
        _keycloakUserService = keycloakUserService;
        _keycloakAuthService = keycloakAuthService;
    }

    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        try
        {
            var users = await _userService.GetAllUsersAsync(pageSize, pageNumber);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{email}")]    
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
    {
        try
        {
            var user = await _userService.GetUserByEmailAsync(email);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDTO createUserDTO)
    {
        if (!JwtTokenHelper.HasRequiredRole(HttpContext, "admin"))
        {
            return Forbid(); // Return 403 Forbidden if the user does not have the required role
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdUserDTO = await _userService.CreateUserAsync(createUserDTO);

            List<int> roles = [Role.ADMIN_ID, Role.TRAINER_ID];

            if (roles.Contains((int)createdUserDTO.RoleId!))
            {
                var token = await _keycloakAuthService.GetAdminToken();
                await _keycloakUserService.CreateUserAsync(createdUserDTO, token);
            }

            return CreatedAtAction(null, new { id = createdUserDTO.Id }, createdUserDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UserDto updateUserDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedUserDTO = await _userService.UpdateUserAsync(updateUserDTO);
            return Ok(updatedUserDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}
