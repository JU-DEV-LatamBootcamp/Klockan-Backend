using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Meeting;
using Asp.Versioning;
using KlockanAPI.Application.Utils.Webex;
using KlockanAPI.Application.Services.Webex;
using KlockanAPI.Domain.Models.Webex;

namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]

public class MeetingsController : ControllerBase
{
    private readonly IMeetingService _meetingService;
    private readonly WebexService _webexService;

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
            DateTime date = DateTime.Now;
            string time = "15:00:00";
            int classroomId = 1;
            int trainerId = 1;
            List<int> userIds = new List<int> { 2, 3 };

            Meeting meetingDetails = await WebexMeetingUtils.CreateMeetingAsync(date, time, classroomId, trainerId, userIds);
            await _webexService.CreateMeetingAsync(meetingDetails);
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
