using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IInfrastructureRepository
    {
        Task<Infrastructure?> GetById(int id);
        Task<List<Infrastructure>> GetAll();
        Task<Infrastructure> Create(Infrastructure entity);
        Task<bool> Update(Infrastructure entity);
        Task<bool> Delete(int id);
    }
}
