using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.DTOs.Schedule;
using Asp.Versioning;
using KlockanAPI.Application.Services.Interfaces;


namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class SchedulesController : ControllerBase
{
    private readonly IScheduleService _ScheduleService;

    public SchedulesController(IScheduleService Scheduleservice)
    {
        _ScheduleService = Scheduleservice;
    }

    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetAllSchedules()
    {
        var Schedules = await _ScheduleService.GetAllSchedulesAsync();
        return Ok(Schedules);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScheduleDTO>> CreateSchedule([FromBody] CreateScheduleDTO createScheduleDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdScheduleDTO = await _ScheduleService.CreateScheduleAsync(createScheduleDTO);
            return CreatedAtAction(null, new { id = createdScheduleDTO.Id }, createdScheduleDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScheduleDTO>> DeleteSchedule(int id)
    {
        throw new NotImplementedException();
    }
}
