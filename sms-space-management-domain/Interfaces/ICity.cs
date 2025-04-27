using sms.space.management.domain.Entities.Organization;


namespace sms.space.management.domain.Interfaces
{
    public interface ICity
    {
        bool Delete(int ID);
        IEnumerable<City> List();
        City Get(int ID);
        City Add(City city);
        City Upate(int ID, City city);
    }
}
