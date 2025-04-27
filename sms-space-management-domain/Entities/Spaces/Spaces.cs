using System.Text.Json.Nodes;

namespace sms.space.management.domain.Entities.Spaces
{
    public class Spaces
    {
        public int SpaceId { get; set; }
        public int FloorId { get; set; }
        public int BuildingId { get; set; }
        public int OrgId { get; set; }
        public string? SpaceType { get; set; }
        public string? SpaceAliasName { get; set; }
        //public string[]? MappedCalendarIds { get; set; }
        public string? Email { get; set; }
        public string? DirectionNotes { get; set; }
        public int[]? ServicingFacilities { get; set; }
        public string? Coordinates { get; set; }
        public string? Workweekdays { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public bool? AllowWorkHours { get; set; }
        public bool? AllowRepeat { get; set; }
        public string? MaximumDuration { get; set; }
        public bool? AutoAccept { get; set; }
        public bool? AutoDecline { get; set; }
        public JsonObject? Resources { get; set; }
        public JsonObject? Roles{ get; set; }
      
        public Organization.Organization? Organization { get; set; }
        public Building.Building? Building { get; set; }
        public bool IsAvailable { get; set; }

        public int? SpaceTypeId { get; set; }
        public string[]? MappedCalendarIds { get; set; }
        public string[]? MappedConnectorIds { get; set; }
        public string? Groupname { get; set; }
        public string FloorName { get; set; }
        public string FloorPlan { get; set; }
        public string SpaceImage { get; set; }
        public string name { get; set; }

        public string resource_count { get; set; }
    }   
}
