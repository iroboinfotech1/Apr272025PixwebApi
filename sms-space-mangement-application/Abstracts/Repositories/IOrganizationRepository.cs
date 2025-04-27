using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.application.Abstracts.Repositories;

public interface IOrganizationRepository
{
    Task<Organization?> GetById(int id);
    Task<List<Organization>> GetAll();
    Task<Organization> Create(Organization entity);
    Task<bool> Update(Organization entity);
    Task<bool> Delete(int id);
}

public interface ICountryRepository
{
    Task<List<Country>> GetAll();
}

public interface IStateRepository
{
    Task<List<StateEntity>> GetByCountryId(int id);
}

public interface ICityRepository
{
    Task<List<CityEntity>> GetByStateId(int id);
}



