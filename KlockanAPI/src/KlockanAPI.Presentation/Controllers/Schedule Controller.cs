using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.DTOs.Schedule;
using Asp.Versioning;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.DTOs.Program;


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

    [HttpPost("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ScheduleDTO>>> CreateSchedule([FromBody] List<CreateScheduleDTO> createScheduleDTO, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {

            var createdScheduleDTO = await _ScheduleService.CreateScheduleAsync(createScheduleDTO, id);
            return CreatedAtAction(null, new { createdScheduleDTO });



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
        await Task.FromResult(-1);
        throw new NotImplementedException();
    }
}
