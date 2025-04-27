using sms.space.management.domain.Entities.PlayerManagement;
using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IPlayerManagementService
    {
        Task<PlayerManagement> Create(PlayerManagement request);
        Task<bool> Update(PlayerManagement request);
        Task<IReadOnlyList<PlayerManagement>> GetAll();

        Task<IReadOnlyList<UtilityPlayer>> RetrieveStaticData();
        Task<PlayerManagement> GetBySerialNumber(string serialNo);
        Task<bool> Delete(string serialNo);

        Task<bool> InsertPlayerSensitiveInformation(PlayerSensitive request);
        Task<PlayerSensitive> RetrievePlayerSensitiveInformation(string serialNo);

        Task<bool> InsertPlayerLogs(PlayerLogs request);
        Task<PlayerLogs> GetPlayerLogsBySerialNumber(string serialNo);
    }
}
