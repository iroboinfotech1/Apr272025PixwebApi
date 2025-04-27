using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IIndustryRepository
    {
        Task<Industry?> GetById(int id);
        Task<List<Industry>> GetAll();
        Task<Industry> Create(Industry entity);
        Task<bool> Update(Industry entity);
        Task<bool> Delete(int id);
    }
}
