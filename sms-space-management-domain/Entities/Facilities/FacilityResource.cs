namespace sms.space.management.domain.Entities.Facilities
{
    public class FacilityResource
    {
        public int ResourceId { get; set; }
        public string Name { get; set; }
        public int FacilityId { get; set; }
        public string Type { get; set; }
        public bool IsEnabled { get; set; }
        public string Icon { get; set; }
        public int ResourceCount { get; set; }
    }
}
