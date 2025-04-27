using System.Text.Json.Serialization;

namespace sms.space.management.application.Models.Dtos.Facilities;

public class CreateFacilitiesDto : BaseFacilitiesDto
{

    [JsonIgnore]
    public int CreatedBy { get; set; }
    [JsonIgnore]
    public int CreatedDate { get; set; }
}
public class ResourceRequest
{
    public int[]? resources { get; set; }

}

public class CreateResourcesDto : BaseResourcesDto
{


}
