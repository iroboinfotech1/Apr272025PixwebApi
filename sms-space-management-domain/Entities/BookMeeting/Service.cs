using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.BookMeeting
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int MeetingId { get; set; }
        public int NoOfSnakes { get; set; }
        public int NoOfCofee { get; set; }
        public int NoOfLunch { get; set; }
    }
}
