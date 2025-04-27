using sms.space.management.domain.Entities.Spaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface ISpacesRepository
    {
        Task<List<Spaces>> GetAll();
        Task<Spaces?> GetById(int id);
        Task<Spaces> Create(Spaces entity);
        Task<bool> Update(Spaces entity);
        Task<bool> Delete(int id);
        Task<bool> SaveSettings(Settings request);
        Task<List<AvailableSpace>> GetAllAvailableSpaces(SpaceFilter filter);
        Task<List<KeyValuePair<string, FloorSpaces>>> FindRooms(SpaceFilter filter);

        //Task<List<KeyValuePair<string, FloorSpaces>>> FindRooms(SpaceFilter filter);
    }
}
