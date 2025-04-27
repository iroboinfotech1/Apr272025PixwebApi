using sms.space.management.domain.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IIndustryService
    {
        Task<Industry> Create(Industry request);
        Task<bool> Update(Industry request);
        Task<IReadOnlyList<Industry>> GetAll();
        Task<Industry> GetById(int id);
        Task<bool> Delete(int id);
    }
}
