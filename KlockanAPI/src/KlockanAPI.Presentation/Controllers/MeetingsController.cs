using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Meeting;
using Asp.Versioning;

namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]

public class MeetingsController : ControllerBase
{
    private readonly IMeetingService _meetingService;
    public MeetingsController(IMeetingService meetingService)
    {
        _meetingService = meetingService;
    }

    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MeetingDto>>> GetAllMeetings()
    {
        var meetings = await _meetingService.GetAllMeetingsAsync();
        return Ok(meetings);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MeetingDto>> CreateMeeting([FromBody] CreateMeetingDto createMeetingDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdMeeting = await _meetingService.CreateSingleMeeting(createMeetingDto);
            return createdMeeting != null ?

                CreatedAtAction(nameof(CreateMeeting), new { Id = createdMeeting.Id }, createdMeeting) :
                StatusCode(500);
        }
        catch (Exception ex)
        {
            return StatusCode(500,$"Internal server error: {ex.Message}");
        }
    }
}
