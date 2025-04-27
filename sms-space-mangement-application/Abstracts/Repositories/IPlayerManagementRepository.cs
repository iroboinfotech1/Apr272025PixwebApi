using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.PlayerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IPlayerManagementRepository
    {
        Task<PlayerManagement> Create(PlayerManagement request);
        Task<bool> Update(PlayerManagement request);
        Task<IReadOnlyList<PlayerManagement>> GetAll();
        Task<IReadOnlyList<UtilityPlayer>> RetrieveStaticData();
        Task<PlayerManagement> GetBySerialNumber(string serialNo);
        Task<bool> Delete(string serialNo);
        Task<bool> InsertPlayerSensitiveInformation(PlayerSensitive request);
        Task<bool> InsertPlayerLogs(PlayerLogs request);
        Task<PlayerSensitive> RetrievePlayerSensitiveInformation(string serialNo);

        Task<PlayerLogs> GetPlayerLogsBySerialNumber(string serialNo);

    }
}
