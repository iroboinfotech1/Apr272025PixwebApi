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
    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _repository;

        public FloorService(IFloorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Floor> Create(Floor request)
        {

            var floor = await _repository.Create(request);
            return floor;
        }

        public async Task<IReadOnlyList<Floor>> GetAll()
        {
            var floor = await _repository.GetAll();
            return floor;
        }

        public async Task<Floor> GetById(int id)
        {
            var floor = await _repository.GetById(id);
            return floor;
        }

        public Task<bool> Update(Floor request)
        {
            return _repository.Update(request);
        }
        public Task<bool> Delete(int id)
        {
            return _repository.Delete(id);
        }

        public async Task<IReadOnlyList<Floor>> GetFloorByBuilding(int id)
        {
            var floor = await _repository.GetFloorByBuilding(id);
            return floor;
        }
    }
}
