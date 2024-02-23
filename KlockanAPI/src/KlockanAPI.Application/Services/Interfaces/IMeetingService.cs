using KlockanAPI.Application.DTOs.Meeting;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IMeetingService
{
    Task<IEnumerable<MeetingDto>> GetAllMeetingsAsync();
    Task<MeetingDto> CreateSingleMeeting(CreateMeetingDto meeting);
}
