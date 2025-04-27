using sms.space.management.domain.Entities.Building;

namespace sms.space.management.domain.Interfaces
{
    public interface IBuilding
    {
        bool Delete (int id);
        IEnumerable<Building> List();
        Building Get (int id);
        Building Add (Building building);
        Building Update (int id, Building building);
    }
}