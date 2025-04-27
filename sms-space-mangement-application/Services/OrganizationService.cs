using AutoMapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models.Dtos.Organization;
using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.application.Services
{

    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;
        private readonly IMapper _mapper;

        public CountryService(IMapper mapper, ICountryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IReadOnlyList<GetCountryDto>> GetAll()
        {
            var organizations = await _repository.GetAll();
            return _mapper.Map<List<GetCountryDto>>(organizations);
        }
    }

    public class StateService : IStateService
    {
        private readonly IStateRepository _repository;
        private readonly IMapper _mapper;

        public StateService(IMapper mapper, IStateRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IReadOnlyList<GetStateDto>> GetByCountryId(int id)
        {
            var organizations = await _repository.GetByCountryId(id);
            return _mapper.Map<List<GetStateDto>>(organizations);
        }
    }

    public class CityService : ICityService
    {
        private readonly ICityRepository _repository;
        private readonly IMapper _mapper;

        public CityService(IMapper mapper, ICityRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IReadOnlyList<GetCityDto>> GetByStateId(int id)
        {
            var organizations = await _repository.GetByStateId(id);
            return _mapper.Map<List<GetCityDto>>(organizations);
        }
    }

    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _repository;
        private readonly IMapper _mapper;

        public OrganizationService(IMapper mapper, IOrganizationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetOrganizationDto> Create(CreateOrganizationDto request)
        {
            var organization = _mapper.Map<Organization>(request);

            organization.OrgGuid = Guid.NewGuid().ToString();
            organization = await _repository.Create(organization);

            return _mapper.Map<GetOrganizationDto>(organization);
        }

        public async Task<bool> Delete(int id)
        {
            var isdeleted = await _repository.Delete(id);
            return isdeleted;
        }

        public async Task<IReadOnlyList<GetOrganizationDto>> GetAll()
        {
            var organizations = await _repository.GetAll();
            return _mapper.Map<List<GetOrganizationDto>>(organizations);
        }

        public async Task<GetOrganizationDto> GetById(int id)
        {
            var organization = await _repository.GetById(id);
            return _mapper.Map<GetOrganizationDto>(organization);
        }

        public async Task<bool> Update(UpdateOrganizationDto request)
        {
            var organization = _mapper.Map<Organization>(request);

            bool isupdated = await _repository.Update(organization);

            return isupdated;
        }
    }
}
