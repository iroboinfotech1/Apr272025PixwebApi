using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Spaces;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IDeskRepository
    {
        //Task<List<Desk>> GetAllAvailableDesk(SpaceFilter filter);

        Task<List<AvailableSpace>> GetAllAvailableDesk(SpaceFilter filter);
    }
}
