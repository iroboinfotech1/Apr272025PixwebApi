using sms.space.management.domain.Entities.Facilities;

namespace sms.space.management.domain.Entities.Spaces
{
    public class AvailableSpace
    {
        public string SpaceName { get; set; }

        public string space_image { get; set; }
        public int SpaceId { get; set; }
        public int MeetingId { get; set; }
        public string? Coordinates { get; set; }
        public int BuildingId { get; set; }
        public string? BuildingName { get; set; }
        public string? Address { get; set; }
        public string? OrganisationImage { get; set; }
        public bool? IsAvailable { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public string? FloorImage { get; set; }
        public int OrgId { get; set; }
        public string? SpaceType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ResourceId { get; set; }
        public string? ResourceIcon { get; set; }
        public int FacilityId { get; set; }
        public string? FacilityName { get; set; }
        public int ResourceCount { get; set; }
        public List<SpaceResources> SpaceResources { get; set; }
        public int AvailableIn { get; set; }
    }

 public class SpaceResources
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public int ResourceCount { get; set; }
        public int ResourceId { get; set; }
        public bool? IsAvailable { get; set; }
    }

    public class connectordetails
    {
        public string[]? connectorid { get; set; }

        public string[]? calendarId { get; set; }

        public string? spacealiasname { get; set; }

        public bool isDeleted { get; set; }

        public string Meeting_Id { get; set; }

        public string SourceEventId { get; set; }
    }


    public class EventDetails
    {

        public string sourceeventid { get; set;}

        public bool isdeleted { get; set; }

        public string meetingid { get; set; }


    }
}
