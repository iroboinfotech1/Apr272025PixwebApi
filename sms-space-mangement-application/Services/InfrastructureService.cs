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
    public class InfrastructureService : IInfrastructureService
    {
        private readonly IInfrastructureRepository _repository;

        public InfrastructureService(IInfrastructureRepository repository)
        {
            _repository = repository;
        }

        public async Task<Infrastructure> Create(Infrastructure request)
        {

            var infrastructure = await _repository.Create(request);
            return infrastructure;
        }

        public async Task<IReadOnlyList<Infrastructure>> GetAll()
        {
            var infrastructure = await _repository.GetAll();
            return infrastructure;
        }

        public async Task<Infrastructure> GetById(int id)
        {
            var infrastructure = await _repository.GetById(id);
            return infrastructure;
        }

        public Task<bool> Update(Infrastructure request)
        {
            return _repository.Update(request);
        }
        public Task<bool> Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
