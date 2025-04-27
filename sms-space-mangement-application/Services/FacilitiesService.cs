using AutoMapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models.Dtos.Facilities;
using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.application.Services;

public class FacilitiesService : IFacilitiesService
{
    private readonly IFacilitiesRepository _repository;
    private readonly IMapper _mapper;

    public FacilitiesService(IMapper mapper, IFacilitiesRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<GetFacilitiesDto> Create(CreateFacilitiesDto request)
    {
        var facilities = _mapper.Map<Facilities>(request);

        facilities = await _repository.Create(facilities);

        return _mapper.Map<GetFacilitiesDto>(facilities);
    }

    public async Task<bool> Delete(int id)
    {
        var isdeleted = await _repository.Delete(id);
        return isdeleted;

    }

    public async Task<IReadOnlyList<FacilityTypes>> GetAllFacilityTypes()
    {
        var facilityTypes = await _repository.GetAllFacilityTypes();
        return _mapper.Map<List<FacilityTypes>>(facilityTypes);
    }

    public async Task<IReadOnlyList<GetFacilitiesDto>> GetByOrganization(int orgId)
    {
        var facilities = await _repository.GetByOrganization(orgId);
        return _mapper.Map<List<GetFacilitiesDto>>(facilities);
    }

    public async Task<IReadOnlyList<GetFacilitiesDto>> GetByOrganizationAndFacilityType(int orgId, int facilityTypeId)
    {
        var facilities = await _repository.GetByOrganizationAndFacilityType(orgId, facilityTypeId);
        return _mapper.Map<List<GetFacilitiesDto>>(facilities);
    }

    public async Task<GetFacilitiesDto> GetById(int id)
    {
        var facilities = await _repository.GetById(id);
        return _mapper.Map<GetFacilitiesDto>(facilities);
    }

    public async Task<bool> Update(int id, UpdateFacilitiesDto request)
    {
        var facilities = _mapper.Map<Facilities>(request);

        bool isupdated = await _repository.Update(facilities);

        return isupdated;
    }

    public async Task<IReadOnlyList<GetFacilitiesDto>> GetAll()
    {
        var facilities = await _repository.GetAll();
        return _mapper.Map<List<GetFacilitiesDto>>(facilities);
    }

    public async Task<GetFacilitiesDto> GetFacilitiesById(int facilityId)
    {
        var facilities = await _repository.GetFacilitiesById(facilityId);
        return _mapper.Map<GetFacilitiesDto>(facilities);
    }
}

public class ResourcesService : IResourcesService
{
    private readonly IResourcesRepository _repository;
    private readonly IMapper _mapper;

    public ResourcesService(IMapper mapper, IResourcesRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<GetResourcesDto> Create(CreateResourcesDto request)
    {
        var facilities = _mapper.Map<Resource>(request);

        facilities = await _repository.Create(facilities);

        return _mapper.Map<GetResourcesDto>(facilities);
    }

    public async Task<bool> Delete(int id)
    {
        var isdeleted = await _repository.Delete(id);
        return isdeleted;

    }

    public async Task<IReadOnlyList<GetResourcesDto>> GetByFacility(int facilityId)
    {
        var facilities = await _repository.GetByFacility(facilityId);
        return _mapper.Map<List<GetResourcesDto>>(facilities);
    }

    public async Task<bool> Update(UpdateResourcesDto request)
    {
        var facilities = _mapper.Map<Resource>(request);

        bool isupdated = await _repository.Update(facilities);

        return isupdated;
    }

    public async Task<bool> UpdateResourceStatus(UpdateResourcesDto request)
    {
        var facilities = _mapper.Map<Resource>(request);

        bool isupdated = await _repository.UpdateResourceStatus(facilities);

        return isupdated;
    }

    public async Task<IEnumerable<Resource>> GetResourceList(int floorId)
    {
        var resourceList = await _repository.GetResources(floorId);
        return resourceList;
    }
}