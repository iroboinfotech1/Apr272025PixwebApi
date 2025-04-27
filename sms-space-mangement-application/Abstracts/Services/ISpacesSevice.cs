using sms.space.management.application.Models.Dtos.Spaces;
using sms.space.management.domain.Entities.Spaces;

namespace sms.space.management.application.Abstracts.Services
{
    public interface ISpacesSevice
    {
        Task<IReadOnlyList<GetSpacesDto>> GetAll();
        Task<GetSpacesDto> GetById(int id, string basedurl, string endpoint, string queryParamKey);
        Task<GetSpacesDto> Create(Spaces request);
        Task<bool> Update(int id, Spaces request);
        Task<bool> Delete(int id);
        Task<bool> SaveSettings(Settings request);
        Task<IReadOnlyList<GetSpacesDto>> GetAllSpacebyFloorId(int floorid);
        Task<IReadOnlyList<AvailableSpaceDto>> GetAvailableSpace(SpaceFilter filter);

        Task<List<KeyValuePair<string, FloorSpaces>>> FindRooms(SpaceFilter filter);
    }
}
