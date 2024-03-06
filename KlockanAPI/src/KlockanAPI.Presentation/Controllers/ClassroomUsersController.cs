using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/classrooms/{classroomId}/users")]
[ApiVersion("1.0")]
public class ClassroomUsersController : ControllerBase
{
    private readonly IClassroomUserService _classroomUserService;

    public ClassroomUsersController(IClassroomUserService classroomUserService)
    {
        _classroomUserService = classroomUserService;
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ScheduleDTO>>> GetClassroomUsersByClassroomId(int classroomId)
    {
        var users = await _classroomUserService.GetUsersByClassroomIdAsync(classroomId);

        return Ok(users);
    }
}
