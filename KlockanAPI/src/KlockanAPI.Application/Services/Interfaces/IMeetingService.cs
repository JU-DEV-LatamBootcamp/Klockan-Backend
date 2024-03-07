using KlockanAPI.Application.DTOs.Meeting;
using KlockanAPI.Domain.Models.Webex;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IMeetingService
{
    Task<IEnumerable<MeetingDto>> GetAllMeetingsAsync();
    Task<MeetingDto> CreateSingleMeeting(CreateMeetingDto meeting);

    Task<MeetingReport> GetMeetingReportAsync(string meetingId);
}
