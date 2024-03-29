using KlockanAPI.Application.DTOs.Meeting;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IMeetingService
{
    Task<IEnumerable<MeetingDto>> GetAllMeetingsAsync();
    Task<MeetingDto> CreateSingleMeeting(CreateMultipleMeetingsDto meeting);
    Task<MeetingDto> UpdateMeeting(UpdateMeetingDto meetingDto, int meetingId);
    Task<List<MeetingDto>> CreateMultipleMeetingAsync(CreateMultipleMeetingsScheduleDTO meetings);
    Task<MeetingReportDTO> GetMeetingReportAsync(int meetingId);    
}
