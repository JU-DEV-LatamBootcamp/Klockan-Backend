using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockanAPI.Domain.Models.Webex;

public class MeetingReport
{
    public List<ParticipantReport> items { get; set; }
}
