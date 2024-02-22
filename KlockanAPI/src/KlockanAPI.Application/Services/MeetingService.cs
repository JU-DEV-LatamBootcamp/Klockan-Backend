using MapsterMapper;
using KlockanAPI.Application.DTOs.Meeting;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace KlockanAPI.Application.Services;
public class MeetingService : IMeetingService
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IMapper _mapper;

    public MeetingService(IMeetingRepository meetingRepository, IMapper mapper)
    {
        _meetingRepository = meetingRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MeetingDto>> GetAllMeetingsAsync()
    {
        var meetings = await _meetingRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MeetingDto>>(meetings);
    }

    public async Task<MeetingDto> CreateSingleMeeting(CreateMeetingDto createMeetingDto)
    {
        var meeting = _mapper.Map<Meeting>(createMeetingDto);
        meeting.SessionNumber = await _meetingRepository.GetSessionNumber(meeting.ClassroomId) + 1;
        meeting.CreatedAt = DateTime.UtcNow;
        var createdMeeting = await _meetingRepository.CreateSingleMeeting(meeting);

        if (!createMeetingDto.Users.IsNullOrEmpty() && createdMeeting != null)
        {
            var meetingAttendances = createMeetingDto.Users.Select(s => new MeetingAttendance
            {
                MeetingId = createdMeeting.Id,
                ClassroomUserId = s,
                CreatedAt = DateTime.UtcNow,
                MeetingAttendanceStatusId = 2,
                User = null,
                Meeting = null,
                Status = null
            }).
            ToList();

            createdMeeting.MeetingAttendances = meetingAttendances;

            await _meetingRepository.AssignStudents(createdMeeting.MeetingAttendances, createdMeeting.ClassroomId);
        }
        return _mapper.Map<MeetingDto>(createdMeeting);
    }

}
