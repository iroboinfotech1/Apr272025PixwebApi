using System.Collections.Generic;

namespace sms.space.management.domain.Entities.BookRoom;
public class BookServices
{
    public int ServiceId { get; set; }
    public int MeetingRoomsId { get; set; }
    public int NoOfSnakes { get; set; }
    public int NoOfCofee { get; set; }
    public int NoOfLunch { get; set; }
    public string ServiceName { get; set; }
    public int ServiceValue { get; set; }
}
