using System.Text.Json.Serialization;

namespace sms.space.management.application.Models.Dtos.Organization;

public class UpdateOrganizationDto : BaseOrganizationDto
{
    public int OrgId { get; set; }

    [JsonIgnore]
    public int UpdatedBy { get; set; }
    [JsonIgnore]
    public int UpdatedDate { get; set; }
}

