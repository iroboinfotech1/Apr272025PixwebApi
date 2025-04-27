namespace sms.space.management.application.Models.Dtos.Facilities;

public abstract class BaseFacilitiesDto
{

    public string FacilityName { get; set; } = null!;

    public string? Email { get; set; }

    public int? EscalationPeriod { get; set; }

    public string? EscalationEmail { get; set; }

    public bool NotifyFacilities { get; set; }

    public bool NotifyOrganizer { get; set; }
    public long OrgId { get; set; }
    public int FacilityTypeId { get; set; }

}

public abstract class BaseResourcesDto
{

    public string? Type { get; set; }
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public string? Icon { get; set; }
    public int FacilityId { get; set; }
    public int Count { get; set; }
    public int SpaceId { get; set; }
}

