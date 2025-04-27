using sms.space.management.application.Models.Dtos.Organization;
using sms.space.management.domain.Entities.Building;

namespace sms.space.management.application.Models.Dtos.Spaces
{
    public class GetSpacesDto
    {
        public int SpaceId { get; set; }
        public int FloorId { get; set; }
        public int BuildingId { get; set; }
        public int OrgId { get; set; }
        public string? SpaceType { get; set; }
        public int? SpaceTypeId { get; set; }
        public string? SpaceAliasName { get; set; }
        public string? SpaceImage { get; set; }
        public string[]? MappedCalendarIds { get; set; }
        public string? Email { get; set; }
        public string? DirectionNotes { get; set; }
        public int[]? ServicingFacilities { get; set; }
        public string? Coordinates { get; set; }
        public string? Workweekdays { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? Resources { get; set; }
        public string? Role { get; set; }
        public GetOrganizationDto? Organization { get; set; }
        public Building? Building { get; set; }

        public string[]? MappedConnectorIds { get; set; }
        public string? Groupname { get; set; }

        public bool AllowRepeat { get; set; }
        public bool AutoAccept { get; set; }
        public bool AutoDecline { get; set; }
        public bool AllowWorkHours { get; set; }
        public string MaximumDuration { get; set; }

        public string FloorName { get; set; }
        public string FloorPlan { get; set; }

        public int resource_count { get; set; }

    }
}
