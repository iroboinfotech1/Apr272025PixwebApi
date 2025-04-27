namespace sms.space.management.domain.Entities.Building;
public class Building
{
    public int BuildingId { get; set; }
    public string BuildingName { get; set; }
    public int OrgId { get; set; }
    public string GroupName { get; set; }
    public string Address { get; set; }
    //public string City { get; set; }
    //public string State { get; set; }
    //public string Country { get; set; }
    //public string Zip { get; set; }
    public int[] SupportingFacilities { get; set; }

    //public Organization.Organization Organization { get; set; }

    //public List<SupportGroup> SupportGroups { get; set; }

    public List<Floor> Floors { get; set; }
    public Organization.Organization? Organization { get; set; }
}
