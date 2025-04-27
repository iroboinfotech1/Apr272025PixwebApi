namespace sms.space.management.application.Models.Dtos.Organization;

public abstract class BaseOrganizationDto
{
    //
    public string OrgName { get; set; } = null!;

public int Industry { get; set; }
public string? BuildingName { get; set; } = null!;
    public string? MailingAddress { get; set; }
    public int Country { get; set; }
    public int State { get; set; }
    public int City { get; set; }
    public string? Zipcode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Image { get; set; }
    public string? Logo { get; set; }
    public object? Roles { get; set; }
}

