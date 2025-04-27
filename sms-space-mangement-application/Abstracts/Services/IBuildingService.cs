using sms.space.management.domain.Entities.Building;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IBuildingService
    {
        Task<Building> Create(Building request);
        Task<IReadOnlyList<Building>> GetAll();

        Task<IReadOnlyList<Building>> GetById(int id);

        Task<bool> Delete(int id);

        Task<bool> Update(Building request);
        Task<IReadOnlyList<Building>> GetBuildingsByOrg(int id);
    }
}
