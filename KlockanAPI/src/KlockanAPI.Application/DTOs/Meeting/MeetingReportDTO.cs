using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockanAPI.Application.DTOs.Meeting
{
    public class MeetingReportDTO
    {
        public List<MeetingParticipantReportDTO> items { get; set; }
    }
}
