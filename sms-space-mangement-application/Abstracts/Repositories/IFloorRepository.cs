using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IFloorRepository
    {
        Task<Floor?> GetById(int id);
        Task<List<Floor>> GetAll();
        Task<Floor> Create(Floor entity);
        Task<bool> Update(Floor entity);
        Task<bool> Delete(int id);
        Task<List<Floor>> GetFloorByBuilding(int id);
    }
}
