using sms.space.management.domain.Entities.Organization;


namespace sms.space.management.domain.Interfaces
{
    public interface ICountry
    {
        bool Delete(int ID);
        IEnumerable<Country> List(bool includeSates = false);
        Country Get(int ID, bool includeSates = false);
        Country Add(Country country);
        Country Update(int ID, Country country);
    }
}
