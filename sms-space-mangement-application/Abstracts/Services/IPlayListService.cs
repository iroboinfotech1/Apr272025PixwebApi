using sms.space.management.domain.Entities.ContentManagement;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IPlayListService
    {

        Task<PlayList> Create(PlayList request);
        Task<bool> Update(List<PlayList> request);
        Task<IReadOnlyList<PlayList>> GetAll();
        Task<PlayList> GetById(int id);
        Task<IReadOnlyList<PlayList>> GetByPlayListName(string playListName);
        Task<bool> Delete(string playListName);
        Task<bool> DeletePlayListItem(int id);
    }
}
