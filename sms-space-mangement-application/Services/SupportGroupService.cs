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
    public class SupportGroupService : ISupportGroupService
    {
        private readonly ISupportGroupRepository _repository;

        public SupportGroupService(ISupportGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<SupportGroup> Create(SupportGroup request)
        {

            var supportGroup = await _repository.Create(request);
            return supportGroup;
        }

        public async Task<IReadOnlyList<SupportGroup>> GetAll()
        {
            var supportGroup = await _repository.GetAll();
            return supportGroup;
        }

        public async Task<SupportGroup> GetById(int id)
        {
            var supportGroup = await _repository.GetById(id);
            return supportGroup;
        }

        public Task<bool> Update(SupportGroup request)
        {
            return _repository.Update(request);
        }
        public Task<bool> Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
