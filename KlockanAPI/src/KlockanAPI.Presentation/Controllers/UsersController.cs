using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

using KlockanAPI.Application.DTOs.User;
using KlockanAPI.Application.Services.Interfaces;

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
        catch(Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
