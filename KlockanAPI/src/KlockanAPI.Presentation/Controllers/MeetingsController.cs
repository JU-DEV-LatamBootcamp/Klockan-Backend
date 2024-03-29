using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Meeting;
using Asp.Versioning;
using KlockanAPI.Domain.Models.Webex;
using KlockanAPI.Domain.Models;

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
    public async Task<ActionResult<MeetingDto>> CreateMeeting([FromBody] CreateMultipleMeetingsDto createMeetingDto)
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

    [HttpPut("{meetingId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MeetingDto>> UpdateMeeting([FromBody] UpdateMeetingDto meeting, int meetingId)
    {
        var _meeting = await _meetingService.UpdateMeeting(meeting, meetingId);
        return _meeting != null ? Ok(_meeting) : NotFound();
    }

    [HttpPost("schedule")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<MeetingDto>>> CreateMultipleMeetingSchedule([FromBody] CreateMultipleMeetingsScheduleDTO createMultipleMeetingDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdMeetingsDTO = await _meetingService.CreateMultipleMeetingAsync(createMultipleMeetingDto);
            return createdMeetingsDTO;
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("report/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]    
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MeetingReportDTO>> GetMeetingReport(int id)
    {                
        try
        {
            var meetReport = await _meetingService.GetMeetingReportAsync(id);
            return meetReport != null ?

                Ok(meetReport) :
                StatusCode(500);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }        
    }
}
