using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.application.Abstracts.Repositories;

public interface IFacilitiesRepository
{
    Task<List<FacilityTypes>> GetAllFacilityTypes();
    Task<List<Facilities>> GetByOrganization(int orgId);
    Task<List<Facilities>> GetByOrganizationAndFacilityType(int orgId, int facilityTypeId);
    Task<List<Facilities>> GetAll();
    Task<Facilities?> GetById(int id);
    Task<Facilities> Create(Facilities entity);
    Task<bool> Update(Facilities entity);
    Task<bool> Delete(int id);
    Task<Facilities> GetFacilitiesById(int facilityId);
}

public interface IResourcesRepository
{
    Task<List<Resource>> GetByFacility(int facilityId);
    Task<Resource> Create(Resource entity);
    Task<bool> Update(Resource entity);
    Task<bool> Delete(int id);
    Task<bool> UpdateResourceStatus(Resource entity);
    Task<IReadOnlyList<Resource>> GetResources(int floorId);
}
