using sms.space.management.domain.Interfaces;
using Entities = sms.space.management.domain.Entities.Organization;
//using DataAccess = sms.space.management.data.access.Implementations;

namespace sms.space.management.application.Implementations
{
    public class Country : ICountry
    {
        private readonly ICountry _country;
        public Country(ICountry country)
        {

            _country = country;

        }

        public Entities.Country Add(Entities.Country country)
        {
            return _country.Add(country);
        }

        public bool Delete(int ID)
        {
            return _country != null && _country.Delete(ID);
        }

        public Entities.Country Get(int ID, bool includeState = false)
        {
            return _country.Get(ID);
        }

        public IEnumerable<Entities.Country> List(bool includeState = false)
        {
            return _country.List();
        }

        public Entities.Country Update(int ID, Entities.Country country)
        {
            return _country.Update(ID, country);
        }
    }
}
