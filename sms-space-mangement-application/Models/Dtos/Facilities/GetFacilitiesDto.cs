namespace sms.space.management.application.Models.Dtos.Facilities;

public class GetFacilitiesDto : BaseFacilitiesDto
{
    public int FacilityId { get; set; }
    public List<GetResourcesDto>? Resources { get; set; }

}


public class GetResourcesDto : BaseResourcesDto
{
    public int ResourceId { get; set; }
}