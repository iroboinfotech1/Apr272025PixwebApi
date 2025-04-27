using System.Text.Json.Serialization;

namespace sms.space.management.application.Models.Dtos.Facilities;

public class UpdateFacilitiesDto : BaseFacilitiesDto
{
    public int FacilityId { get; set; }
    [JsonIgnore]
    public int UpdatedBy { get; set; }
    [JsonIgnore]
    public int UpdatedDate { get; set; }
}

public class UpdateResourcesDto : BaseResourcesDto
{
    public int ResourceId { get; set; }
}