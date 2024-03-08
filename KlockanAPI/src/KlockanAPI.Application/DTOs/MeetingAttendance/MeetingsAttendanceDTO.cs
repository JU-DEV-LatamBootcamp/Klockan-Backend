using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockanAPI.Application.DTOs.MeetingAttendance;

public class MeetingsAttendanceDTO
{
    public string Email {  get; set; }
    public int MinutesAttended {  get; set; }
    public int MeetingId { get; set; }        
}
