namespace sms.space.management.domain.Entities.BookDesk
{
    public class MeetingDesk
    {
        public bool Alldays { get; set; }
        public int Reminder { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int OrgId { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public int DeskId { get; set; }
        public string DeskName { get; set; }
        public string? ReferenceNumber { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int? MeetingId { get; set; }
        public string MeetingName { get; set; } = string.Empty;
    }
}
