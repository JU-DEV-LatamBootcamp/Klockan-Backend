using MapsterMapper;
using KlockanAPI.Application.DTOs.Meeting;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.CrossCutting;

namespace KlockanAPI.Application.Services;
public class MeetingService : IMeetingService
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IMapper _mapper;
    private readonly IThirdPartyMeeting _thirdPartyMeeting;

    public MeetingService(IMeetingRepository meetingRepository, IMapper mapper, IThirdPartyMeeting thirdPartyMeeting)
    {
        _meetingRepository = meetingRepository;
        _mapper = mapper;
        _thirdPartyMeeting = thirdPartyMeeting;
    }

    public async Task<IEnumerable<MeetingDto>> GetAllMeetingsAsync()
    {
        var meetings = await _meetingRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MeetingDto>>(meetings);
    }

    public async Task<MeetingDto> CreateSingleMeeting(CreateMultipleMeetingsDto createMeetingDto)
    {
        var classroomTrainerId = await _meetingRepository.AddUserToClassroomAsync(createMeetingDto.TrainerId, createMeetingDto.ClassroomId);

        foreach (int userId in createMeetingDto.Users)
            await _meetingRepository.AddUserToClassroomAsync(userId, createMeetingDto.ClassroomId);

        var meeting = _mapper.Map<Meeting>(createMeetingDto);
        meeting.TrainerId = classroomTrainerId;
        meeting.SessionNumber = await _meetingRepository.GetSessionNumber(meeting.ClassroomId) + 1;
        meeting.CreatedAt = DateTime.UtcNow;

        string thirdPartyId = await _thirdPartyMeeting.CreateMeetingAsync(createMeetingDto);
        meeting.ThirdPartyId = thirdPartyId;

        var createdMeeting = await _meetingRepository.CreateSingleMeeting(meeting);

        return _mapper.Map<MeetingDto>(createdMeeting);
    }

    public async Task<MeetingDto> UpdateMeeting(UpdateMeetingDto meetingDto, int meetingId)
    {
        var meeting = await _meetingRepository.GetMeetingById(meetingId);
        NotFoundException.ThrowIfNull(meeting, $"Meeting wit id {meetingId} was not found");

        // TODO: Update on thirdParty
        meeting.Date = meetingDto.Date;
        meeting.Time = meetingDto.Time;
        
        var updatedMeeting = await _meetingRepository.UpdateMeeting(meeting, meetingId);

        return _mapper.Map<MeetingDto>(updatedMeeting);
    }

    public async Task<List<MeetingDto>> CreateMultipleMeetingAsync(CreateMultipleMeetingsScheduleDTO createMultipleMeetingDTO)

    {
        var listMeetings = new List<MeetingDto>();
        var startdate = createMultipleMeetingDTO.StartDate;
        var quantity = createMultipleMeetingDTO.Quantity;

        for (int i = 0; i < quantity; i++)
        {
            int weekday = 0;
            var dateofweek = startdate;

            foreach (MeetingScheduleDTO schedule in createMultipleMeetingDTO.Schedules)
            {

                var difbetwetweenDays = schedule.WeekdayId - weekday;
                dateofweek = dateofweek.AddDays(difbetwetweenDays);

                var createMeeting = new CreateMultipleMeetingsDto
                {
                    ClassroomId = createMultipleMeetingDTO.ClassroomId,
                    Date = dateofweek,
                    Time = schedule.StartTime,
                };

                var meeting = _mapper.Map<Meeting>(createMeeting);
                var createdMeeting = await _meetingRepository.CreateSingleMeeting(meeting);
                weekday = schedule.WeekdayId;



            }
            startdate = startdate.AddDays(7);
        }


        return _mapper.Map<List<MeetingDto>>(listMeetings);
    }
}
