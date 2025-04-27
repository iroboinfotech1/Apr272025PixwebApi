using sms.space.management.domain.Entities.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IPlayListRepository
    {
        Task<PlayList> Create(PlayList request);

        Task<bool> CreateMultiple(List<PlayList> request);
        
        Task<bool> Update(List<PlayList> request);
        Task<IReadOnlyList<PlayList>> GetAll();
        Task<PlayList> GetById(int id);
        Task<IReadOnlyList<PlayList>> GetByPlayListName(string playListName);
        Task<bool> Delete(string playListName);
        Task<bool> DeletePlayListItem(int id);
    }
}
