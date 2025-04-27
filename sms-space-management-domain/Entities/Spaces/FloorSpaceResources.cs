namespace sms.space.management.domain.Entities.Spaces
{
    public class FloorSpaces
    {
        public int SpaceId { get; set; }
        public string SpaceName { get; set; } = string.Empty;

        public int OrgId { get; set; }

        public int BuildingId { get; set; }

        public int FloorId { get; set; }

        public string FloorName { get; set; }

        public List<SpaceResources> SpaceResources { get; set; } = new List<SpaceResources>();
        public bool? IsAvailable { get; set; }
        public int AvailableIn { get; set; }
    }
}
