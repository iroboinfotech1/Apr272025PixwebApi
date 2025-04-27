using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.BookRoom;
using sms.space.management.domain.Entities.PlayerManagement;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IBookServicesRepository
    {
        Task<IReadOnlyList<ServiceDetail>> GetBookServices();
        Task<List<ServiceDetail>> GetBookServices(int meetingId);
        Task<ServiceDetail> CreateBookServices(ServiceDetail request);
        Task<bool> UpdateBookServices(ServiceDetail request);
        Task<bool> DeleteBookServices(int serviceId);
    }
}

