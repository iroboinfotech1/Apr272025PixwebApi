using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities
{
    public  class BookMeetingInfo
    {

        public string MeetingId { get; set; }
        public int SpaceId { get; set; }
        public bool Alldays { get; set; }
        public int Reminder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public int NoOfAttendees { get; set; }
        public string MeetingName { get; set; }
        public int OrgId { get; set; }
        public int MeetingType { get; set; }
    }
}
