using sms.space.management.application.Models.Dtos.Facilities;
using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.application.Abstracts.Services;

public interface IFacilitiesService
{
    Task<IReadOnlyList<FacilityTypes>> GetAllFacilityTypes();
    Task<IReadOnlyList<GetFacilitiesDto>> GetByOrganization(int orgId);
    Task<IReadOnlyList<GetFacilitiesDto>> GetByOrganizationAndFacilityType(int orgId, int facilityTypeId);
    Task<IReadOnlyList<GetFacilitiesDto>> GetAll();
    Task<GetFacilitiesDto> GetById(int id);
    Task<GetFacilitiesDto> Create(CreateFacilitiesDto request);
    Task<bool> Update(int id, UpdateFacilitiesDto request);
    Task<bool> Delete(int id);
    Task<GetFacilitiesDto> GetFacilitiesById(int facilityId);

}

public interface IResourcesService
{
    Task<IReadOnlyList<GetResourcesDto>> GetByFacility(int facilityId);
    Task<GetResourcesDto> Create(CreateResourcesDto request);
    Task<bool> Update(UpdateResourcesDto request);
    Task<bool> Delete(int id);
    Task<bool> UpdateResourceStatus(UpdateResourcesDto request);

    Task<IEnumerable<Resource>> GetResourceList(int floorId);

}