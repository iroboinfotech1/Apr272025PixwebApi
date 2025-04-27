using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IFloorService
    {
        Task<Floor> Create(Floor request);
        Task<bool> Update(Floor request);
        Task<IReadOnlyList<Floor>> GetAll();
        Task<Floor> GetById(int id);
        Task<bool> Delete(int id);
        Task<IReadOnlyList<Floor>> GetFloorByBuilding(int id);
    }
}
