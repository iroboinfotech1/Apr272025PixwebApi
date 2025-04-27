using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Spaces;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IDeskService
    {
        Task<List<AvailableSpace>> GetAvailableDeskList(SpaceFilter filter);

    }
}
