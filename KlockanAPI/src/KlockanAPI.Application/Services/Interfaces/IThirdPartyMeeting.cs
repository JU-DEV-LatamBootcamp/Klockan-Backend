﻿using KlockanAPI.Application.DTOs.Meeting;
using KlockanAPI.Domain.Models.Webex;

namespace KlockanAPI.Application.Services.Interfaces;

public interface IThirdPartyMeeting
{
    Task<string> CreateMeetingAsync(CreateMultipleMeetingsDto meeting);
    Task<MeetingReport> GetMeetingReportAsync(string meetingId);
}
