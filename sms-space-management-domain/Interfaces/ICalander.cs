using sms.space.management.domain.Entities.Common;


namespace sms.space.management.domain.Interfaces
{
    public interface ICalendarsController
    {
        bool DeleteCalendar(int id);
        IEnumerable<Calendar> GetCalendar();
        Calendar Get (int id);
        Calendar Add (Calendar calendar);
        Calendar Update (int id, Calendar calendar);
    }
}
