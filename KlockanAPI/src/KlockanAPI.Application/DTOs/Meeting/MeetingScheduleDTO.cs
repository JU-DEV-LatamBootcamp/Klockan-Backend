using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockanAPI.Application.DTOs.Meeting
{
    public class MeetingScheduleDTO
    {
        public int WeekdayId { get; set; }
        public TimeOnly StartTime { get; set; }
    }
}
