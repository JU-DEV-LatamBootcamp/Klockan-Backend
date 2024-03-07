using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlockanAPI.Domain.Models;
using KlockanAPI.Domain.Models.Webex;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KlockanAPI.Infrastructure.Repositories;

public class MeetingAttendancesRepository : IMeetingAttendancesRepository
{
    private readonly KlockanContext _context;
    public MeetingAttendancesRepository(KlockanContext context)
    {
        _context = context;
    }
    public async Task<MeetingReport> CreateMeetingAttendance(MeetingReport meetingReport, int meetingId)
    {
        if(_context.MeetingAttendances.FirstOrDefault(ma => ma.MeetingId == meetingId) == null)
        {
            var meeting = await _context.Meetings.Where(m => m.Id == meetingId)
                .FirstOrDefaultAsync();
            var attendanceStatusList = await _context.MeetingAttendanceStatuses.ToListAsync();
            foreach (ParticipantReport participantReport in meetingReport.items)
            {
                var user = await _context.Users.Where(u => u.Email == participantReport.email)
                    .FirstOrDefaultAsync();
                var classroomUser = await _context.ClassroomUsers
                .Where(cu => cu.UserId == user.Id && cu.ClassroomId == meeting.Id)
                .FirstOrDefaultAsync();

                if (classroomUser != null && classroomUser.RoleId != 2)
                {
                    var attendanceStatus = attendanceStatusList.Where(status => status.Id == (participantReport.DurationInMinutes > 0 ? 1 : 2))
                        .FirstOrDefault();
                    MeetingAttendance meetingAttendance = new MeetingAttendance
                    {
                        MinutesAttended = participantReport.DurationInMinutes,
                        MeetingId = meetingId,
                        Meeting = meeting,
                        ClassroomUserId = classroomUser.Id,
                        User = classroomUser,
                        MeetingAttendanceStatusId = attendanceStatus.Id,
                        Status = attendanceStatus,
                    };
                    await _context.MeetingAttendances.AddAsync(meetingAttendance);
                }
            }
            await _context.SaveChangesAsync();
        }        
        return meetingReport;
    }
}
