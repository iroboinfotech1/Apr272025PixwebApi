using System.Text.Json.Serialization;

namespace sms.space.management.application.Models.Dtos.Organization;

public class CreateOrganizationDto : BaseOrganizationDto
{
    [JsonIgnore]
    public int CreatedBy { get; set; }
    [JsonIgnore]
    public int CreatedDate { get; set; }
}

