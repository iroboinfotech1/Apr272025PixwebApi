using sms.space.management.domain.Entities;
using sms.space.management.domain.Entities.BookDesk;
using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.Spaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IBookMeetingRepository
    {
        Task<List<BookedMeetingDetail>> GetBookMeeting(int spaceId, DateTime startDate, DateTime endDate);
        Task<BookMeetingDetails> GetBookMeeting(int meetingId);
        Task<BookMeetingDetails> CreateBookMeeting(BookMeetingDetails request);

        Task<List<EventDetails>> GetSourceEventId(BookMeetingDetails request);
        Task<List<spaceresources>> GetSpaceResourcesforBookedMeeting(int spaceid);
        Task<BookDeskDetails> CreateBookDesk(BookDeskDetails request);
        Task<bool> UpdateBookMeeting(BookMeetingDetails request);
        Task<bool> DeleteBookMeeting(int meetingId);
        Task<List<connectordetails>> GetConnectorAndCalendarID(int orgid, int buildingid, int floorid);

    }
}
