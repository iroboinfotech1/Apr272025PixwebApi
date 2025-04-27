using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IBuildingRepository
    {
        Task<List<Building>> GetById(int id);
        Task<List<Building>> GetAll();
        Task<Building> Create(Building entity);
        Task<bool> Delete(int id);
        Task<bool> Update(Building entity);
        Task<List<Building>> GetBuildingsByOrg(int id);
    }
}
