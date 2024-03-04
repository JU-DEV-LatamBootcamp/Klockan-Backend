using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Schedule;

namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/classrooms/{classroomId}/[controller]")]
[ApiVersion("1.0")]
public class SchedulesController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public SchedulesController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ScheduleDTO>>> GetClassroomSchedules(int classroomId)
    {
        var schedules = await _scheduleService.GetSchedulesByClassroomIdAsync(classroomId);

        return Ok(schedules);
    }
}
