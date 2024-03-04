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

    public UsersController(IUserService userService, IKeycloakUserService keycloakUserService)
    {
        _userService = userService;
        _keycloakUserService = keycloakUserService;
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
        catch(Exception ex)
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

            int[] roles = [Role.ADMIN_ID, Role.TRAINER_ID];

            if(roles.Contains(createdUserDTO.RoleId))
            {
                bool kycloakUserCreated = await _keycloakUserService.CreateUserAsync(createdUserDTO);
            }

            return CreatedAtAction(null, new { id = createdUserDTO.Id }, createdUserDTO);
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
