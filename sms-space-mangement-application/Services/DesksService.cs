using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Spaces;

namespace sms.space.management.application.Services
{
    public class DeskService : IDeskService
    {
        private readonly IDeskRepository _deskRepository;
        public DeskService(IDeskRepository deskRepository)
        {
            _deskRepository = deskRepository;
        }
        public async Task<List<AvailableSpace>> GetAvailableDeskList(SpaceFilter filter)
        {
            var desks = await _deskRepository.GetAllAvailableDesk(filter);
            return desks;
        }
    }
}
