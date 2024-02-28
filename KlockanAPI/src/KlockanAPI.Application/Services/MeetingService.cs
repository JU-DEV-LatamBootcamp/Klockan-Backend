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
        var classroomTrainerId = await _meetingRepository.AddUserToClassroomAsync(createMeetingDto.TrainerId, createMeetingDto.ClassroomId);

        foreach (int userId in createMeetingDto.Users)
            await _meetingRepository.AddUserToClassroomAsync(userId, createMeetingDto.ClassroomId);

        var meeting = _mapper.Map<Meeting>(createMeetingDto);
        meeting.TrainerId = classroomTrainerId;
        meeting.SessionNumber = await _meetingRepository.GetSessionNumber(meeting.ClassroomId) + 1;
        meeting.CreatedAt = DateTime.UtcNow;
        var createdMeeting = await _meetingRepository.CreateSingleMeeting(meeting);

        return _mapper.Map<MeetingDto>(createdMeeting);
    }
}
