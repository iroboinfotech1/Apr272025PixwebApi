namespace sms.space.management.domain.Entities.Organization;

public class FacilityTypes
{
    public int FacilityTypeId { get; set; }

    public string FacilityTypeName { get; set; } = null!;
}

public class Facilities
{
    public int FacilityId { get; set; }

    public string FacilityName { get; set; } = null!;

    public string? Email { get; set; }

    public int? EscalationPeriod { get; set; }

    public string? EscalationEmail { get; set; }

    public bool NotifyFacilities { get; set; }

    public bool NotifyOrganizer { get; set; }
    public long OrgId { get; set; }
    public List<Resource>? Resources { get; set; }
    public int FacilityTypeId { get; set; }

    //public Entities.Organization.Organization Organization { get; set; }

}

public class Resource
{
    public int SpaceId { get; set; }
    public int ResourceId { get; set; }
    public string? Type { get; set; }
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public string? Icon { get; set; }
    public int FacilityId { get; set; }
    public int Count { get; set; }
}