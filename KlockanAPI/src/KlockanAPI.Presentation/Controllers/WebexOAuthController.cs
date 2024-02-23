using KlockanAPI.Application.Services.Webex;
using KlockanAPI.Application.Utils.Webex;
using KlockanAPI.Domain.Models.Webex;
using Microsoft.AspNetCore.Mvc;


namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("webex")]
public class WebexOAuthController : ControllerBase
{
    private readonly WebexService _webexService;

    public WebexOAuthController(WebexService webexService)
    {
        _webexService = webexService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        DateTime date = DateTime.Now;
        string time = "15:00:00";
        int classroomId = 1; 
        int trainerId = 1; 
        List<int> userIds = new List<int> { 2, 3 };

        Meeting meetingDetails = await WebexMeetingUtils.CreateMeetingAsync(date, time, classroomId, trainerId, userIds);

        var result = await _webexService.CreateMeetingAsync(meetingDetails);
        return Ok(result);
    }
}
