using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.BookMeeting
{
    public class Parkings
    {
        public int ParkingId { get; set; }
        public int? MeetingId { get; set; } = null;
        public string ParticipantName { get; set; }
        public string? ParticipantId { get; set; } = null;
        public string VechicleNumber { get; set; }
        public string SlotId { get; set; }
        public string SlotName { get; set; }
        public string? CreatedBy { get; set; }
    }
}
