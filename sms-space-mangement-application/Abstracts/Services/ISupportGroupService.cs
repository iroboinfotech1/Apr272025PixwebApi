using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Services
{
    public interface ISupportGroupService
    {
        Task<SupportGroup> Create(SupportGroup request);
        Task<bool> Update(SupportGroup request);
        Task<IReadOnlyList<SupportGroup>> GetAll();
        Task<SupportGroup> GetById(int id);
        Task<bool> Delete(int id);
    }
}
