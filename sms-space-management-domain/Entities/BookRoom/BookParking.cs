namespace sms.space.management.domain.Entities.BookRoom;
public class BookParking
{
    public int ParkingId { get; set; }
    public int MeetingRoomsId { get; set; }
    public string ParticipantName { get; set; }
    public string VechicleNumber { get; set; }
    public string SlotDetails { get; set; }
    public string? CreatedBy { get; set; }

}
