using sms.space.management.domain.Entities.BookMeeting;

namespace sms.space.management.domain.Entities.BookDesk
{
    public class BookDeskDetails
    {
        public MeetingDesk Desk { get; set; }
        public List<Parkings>? Parkings { get; set; } = null;
        public List<ServiceDetail>? Services { get; set; } = null;
    }
}
