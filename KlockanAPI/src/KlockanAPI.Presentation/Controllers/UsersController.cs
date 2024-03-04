using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.CrossCutting.Authorization;

namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
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
            return CreatedAtAction(null, new { id = createdUserDTO.Id }, createdUserDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDTO updateUserDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingUser = await _userService.GetUserByIdAsync(id);

            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found");
            }

            var updatedUserDTO = await _userService.UpdateUserAsync(id, updateUserDTO);
            return Ok(updatedUserDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}
