namespace sms.space.management.application.Models.Dtos.Spaces
{
    public class AvailableSpaceDto
    {
        public string? SpaceName { get; set; }

        public string space_image { get; set; }
        public int SpaceId { get; set; }
        public string? Coordinates { get; set; }
        public int BuildingId { get; set; }
        public string? BuildingName { get; set; }
        public string? Address { get; set; }
        public string? OrganisationImage { get; set; }
        public bool? IsAvailable { get; set; }
        public int FloorId { get; set; }
        public string? FloorName { get; set; }
        public int OrgId { get; set; }
        public string? SpaceType { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int ResourceId { get; set; }
        public string? ResourceIcon { get; set; }
        public int FacilityId { get; set; }
        public string? FacilityName { get; set; }
        public string? FloorImage { get; set; }
        public int ResourceCount { get; set; }
    }
}
