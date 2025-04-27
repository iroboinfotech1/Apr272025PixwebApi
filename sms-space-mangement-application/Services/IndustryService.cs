using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public class IndustryService : IIndustryService
    {
        private readonly IIndustryRepository _repository;

        public IndustryService(IIndustryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Industry> Create(Industry request)
        {

            var Industry = await _repository.Create(request);
            return Industry;
        }

        public async Task<IReadOnlyList<Industry>> GetAll()
        {
            var industry = await _repository.GetAll();
            return industry;
        }

        public async Task<Industry> GetById(int id)
        {
            var industry = await _repository.GetById(id);
            return industry;
        }

        public Task<bool> Update(Industry request)
        {
            return _repository.Update(request);
        }
        public Task<bool> Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
