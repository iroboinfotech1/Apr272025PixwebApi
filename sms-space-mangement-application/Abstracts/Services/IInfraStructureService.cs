using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IInfrastructureService
    {
        Task<Infrastructure> Create(Infrastructure request);
        Task<bool> Update(Infrastructure request);
        Task<IReadOnlyList<Infrastructure>> GetAll();
        Task<Infrastructure> GetById(int id);
        Task<bool> Delete(int id);
    }
}
