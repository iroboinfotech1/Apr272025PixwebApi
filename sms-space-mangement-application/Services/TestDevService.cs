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
    public class TestDevService : ITestDevService
    {
        private readonly ITestDevRepository _repository;
        public TestDevService(ITestDevRepository repository)
        {
            _repository = repository;
        }

        public async Task<TestDevs> Create(TestDevs request)
        {
            var testDevRequest = await _repository.Create(request);
            return testDevRequest;
        }
    }
}
