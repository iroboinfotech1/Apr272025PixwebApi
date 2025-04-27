using sms.space.management.application.Models.Dtos.Organization;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IOrganizationService
    {
        Task<GetOrganizationDto> Create(CreateOrganizationDto request);
        Task<bool> Update(UpdateOrganizationDto request);
        Task<IReadOnlyList<GetOrganizationDto>> GetAll();
        Task<GetOrganizationDto> GetById(int id);
        Task<bool> Delete(int id);
    }

    public interface ICountryService
    {
        Task<IReadOnlyList<GetCountryDto>> GetAll();
    }

    public interface IStateService
    {
        Task<IReadOnlyList<GetStateDto>> GetByCountryId(int id);
    }

    public interface ICityService
    {
        Task<IReadOnlyList<GetCityDto>> GetByStateId(int id);
    }

}
