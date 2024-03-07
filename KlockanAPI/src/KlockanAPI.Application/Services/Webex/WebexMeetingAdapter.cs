using KlockanAPI.Application.DTOs.Meeting;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Domain.Models.Webex;

namespace KlockanAPI.Application.Services.Webex;
public class WebexMeetingAdapter : IThirdPartyMeeting
{
    private readonly WebexService _webexService;
    private readonly MeetingDetailsService _meetingDetailsService;

    public WebexMeetingAdapter(WebexService webexService, MeetingDetailsService meetingDetailsService)
    {
        _webexService = webexService;
        _meetingDetailsService = meetingDetailsService;
    }

    public async Task<string> CreateMeetingAsync(CreateMultipleMeetingsDto createMeetingDto)
    {
        var meetingDetails = await _meetingDetailsService.GetMeetingDetailsAsync(createMeetingDto.ClassroomId);

        var webexMeeting = WebexMeetingUtils.ConvertToWebexMeeting(createMeetingDto, meetingDetails);

        var meetingId = await _webexService.CreateMeetingAsync(webexMeeting);

        return meetingId;
    }

    public async Task<MeetingReport> GetMeetingReportAsync(string meetingId)
    {
        return await _webexService.GetMeetingReportAsync(meetingId);
    }
}
