using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.BookMeeting
{
    public class BookedMeetingDetail
    {
        public int SpaceId { get; set; }
        public int MeetingId { get; set; }
        public string MeetingName { get; set; }
        public string SpaceName { get; set; }
        public string FloorName { get; set; }
        public string BuildingName { get; set; }
        public string address { get; set; }
        public string OrgName { get; set; }
        public string Image { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NoOfAttendees { get; set; }
        public string SpaceImage { get; set; }
        public string Notes { get; set; }
        public string[]? Participants { get; set; }
        public List<spaceresources> SpaceResources { get; set; }
    }


    public class spaceresources
    {
        public int SpaceId { get; set; }
        public int ResourceId { get; set; }
        public int ResourceCount { get; set; }
        public string ResourceIcon { get; set; }
        public string ResourceName { get; set; }
    }
}
