using sms.space.management.domain.Entities.Building;


namespace sms.space.management.domain.Interfaces
{
    public interface IDesksController
    {
        bool Delete(int id);
        IEnumerable<Desk> List();
        Desk Get(int id);
        Desk Add(Desk desk);
        Desk Update(int id, Desk desk);
    }
}
