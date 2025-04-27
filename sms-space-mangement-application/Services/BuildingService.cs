using AutoMapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _repository;
        public BuildingService( IBuildingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Building> Create(Building request)
        {        
            var buildingRequest = await _repository.Create(request);
            return buildingRequest;
        }

        public Task<bool> Delete(int id)
        {
            return _repository.Delete(id);
        }

        public async Task<IReadOnlyList<Building>> GetAll()
        {
            var buildings = await _repository.GetAll();
            return buildings;
        }

        public async Task<IReadOnlyList<Building>> GetBuildingsByOrg(int id)
        {
            var buildings = await _repository.GetBuildingsByOrg(id);
            return buildings;

        }

        public async Task<IReadOnlyList<Building>>GetById(int id)
        {
            var building = await _repository.GetById(id);
            return building;
        }

        public Task<bool> Update(Building request)
        {
            return _repository.Update(request);
        }
    }
}
