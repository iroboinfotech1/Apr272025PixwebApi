using sms.space.management.domain.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface ITestDevRepository
    {
        Task<TestDevs> Create(TestDevs entity);
    }
}
