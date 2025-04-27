using sms.space.management.domain.Entities;
using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.BookDesk;
using sms.space.management.domain.Entities.BookRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sms.space.management.domain.Entities.Spaces;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IBooKMeetingService
    {
        Task<List<BookedMeetingDetail>> GetBookMeeting(int spaceId, DateTime startDate, DateTime endDate);
        Task<BookMeetingDetails> GetBookMeeting(int meetingId);

        Task<List<spaceresources>> GetSpaceResourcesforBookedMeeting(int spaceid);
        Task<BookMeetingDetails> CreateMeeting(BookMeetingDetails request);
        Task<BookDeskDetails> CreateDeskBooking(BookDeskDetails request);
        Task<bool> UpdateMeeting(BookMeetingDetails request);
        Task<bool> DeleteMeeting(int meetingId);
        Task<List<connectordetails>> GetConnectorAndCalendarID(int orgid, int buildingid, int floorid);
    }
}
