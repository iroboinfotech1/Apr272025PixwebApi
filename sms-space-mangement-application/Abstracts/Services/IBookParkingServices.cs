using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.BookRoom;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IBookParkingServices
    {
        Task<IReadOnlyList<BookParking>> GetBookParking();
        Task<List<Parkings>> GetBookParking(int meetingId);
        Task<Parkings> CreateBookParking(Parkings request);
        Task<bool> UpdateBookParking(BookParking request);
        Task<bool> DeleteBookParking(int parkingId);

    }
}
