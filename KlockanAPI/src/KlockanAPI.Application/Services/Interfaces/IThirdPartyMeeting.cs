using KlockanAPI.Application.DTOs.Meeting;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IThirdPartyMeeting
{
    Task<string> CreateMeetingAsync(CreateMultipleMeetingsDto meeting);
}
