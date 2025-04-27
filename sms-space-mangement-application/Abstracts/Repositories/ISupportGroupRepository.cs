using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface ISupportGroupRepository
    {
        Task<SupportGroup?> GetById(int id);
        Task<List<SupportGroup>> GetAll();
        Task<SupportGroup> Create(SupportGroup entity);
        Task<bool> Update(SupportGroup entity);
        Task<bool> Delete(int id);
    }
}
