using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models.Dtos.Organization;
using sms.space.management.domain.Entities.PlayerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public class PlayerManagementService : IPlayerManagementService
    {
        private readonly IPlayerManagementRepository _repository;

        public PlayerManagementService(IPlayerManagementRepository repository)
        {
            _repository = repository;
        }
        public async Task<PlayerManagement> Create(PlayerManagement request)
        {
            var playerManagement = await _repository.Create(request);
            return playerManagement;
        }

        public async Task<bool> Delete(string serialNo)
        {
            var isdeleted = await _repository.Delete(serialNo);
            return isdeleted;
        }

        public async Task<IReadOnlyList<PlayerManagement>> GetAll()
        {
            var PlayerManagements = await _repository.GetAll();
            return PlayerManagements;
           
        }

        public async Task<IReadOnlyList<UtilityPlayer>> RetrieveStaticData()
        {
            var PlayerManagements = await _repository.RetrieveStaticData();
            return PlayerManagements;
        }

        public async Task<PlayerManagement> GetBySerialNumber(string serialNo)
        {
            var playerManagement = await _repository.GetBySerialNumber(serialNo);
            return playerManagement;
        }

        public Task<bool> Update(PlayerManagement request)
        {
            var isupdated = _repository.Update(request);
            return isupdated;
        }

        public Task<bool> InsertPlayerSensitiveInformation(PlayerSensitive request)
        {
            var isupdated = _repository.InsertPlayerSensitiveInformation(request);
            return isupdated;
        }

        public Task<PlayerSensitive> RetrievePlayerSensitiveInformation(string serialNo)
        {
            var playerSensitiveitem = _repository.RetrievePlayerSensitiveInformation(serialNo);
            return playerSensitiveitem;
        }
        public Task<bool> InsertPlayerLogs(PlayerLogs request)
        {
            var isupdated = _repository.InsertPlayerLogs(request);
            return isupdated;
        }

        public Task<PlayerLogs> GetPlayerLogsBySerialNumber(string serialNo)
        {
            var playerlogsitem = _repository.GetPlayerLogsBySerialNumber(serialNo);
            return playerlogsitem;
        }

    }
}
