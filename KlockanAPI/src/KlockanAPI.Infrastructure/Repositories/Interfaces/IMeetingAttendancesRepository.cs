using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlockanAPI.Domain.Models;
using KlockanAPI.Domain.Models.Webex;

namespace KlockanAPI.Infrastructure.Repositories.Interfaces;

public interface IMeetingAttendancesRepository
{
    Task<MeetingReport> CreateMeetingAttendance(MeetingReport meetingReport, int meetingId);
}
