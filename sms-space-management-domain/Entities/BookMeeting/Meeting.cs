using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.BookMeeting
{
    public class Meeting
    {
        public int? MeetingId { get; set; } = null;
        public int SpaceId { get; set; }
        public int NoOfAttendees { get; set; }
        public int BuildingId { get; set; }
        public int OrgId { get; set; }
        public int FloorId { get; set; }
       
        [JsonPropertyName("allDays")]
        public bool Alldays { get; set; }
        public int Reminder { get; set; }
        public string TimeZone { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string? MeetingName { get; set; }
        public string[]? Participants { get; set; }
        public string Notes { get; set; }
        public int MeetingType { get; set; }
        public string? ReferenceNumber { get; set; } = null;

        public string? ConnectorId { get; set; }

        public string? CalendarId { get; set; }
        public string? CreatedBy { get; set; }
        public string Action { get; set; }
        public string SourceEventId { get; set; }

    }
}
