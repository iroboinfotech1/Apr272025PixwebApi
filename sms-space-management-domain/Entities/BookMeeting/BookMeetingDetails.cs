using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.BookMeeting
{
    public class BookMeetingDetails
    {
        public Meeting Meeting  { get; set; }
        public List<Parkings>? Parkings { get; set; } = null;
        public List<ServiceDetail>? Services { get; set; } = null;
        public string? SourceEventId { get; set; }

        public string Status { get; set; }

    }
}
