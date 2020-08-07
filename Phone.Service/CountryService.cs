using Phone.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Service
{
    public class CountryService
    {

        PhoneContext phoneContext;
        public CountryService()
        {
            phoneContext = new PhoneContext();
        }
        public IEnumerable<Country> GetCountries()
        {
            return phoneContext.Countries.Where(c => c.IsActive);
        }
        public IEnumerable<Country> GetCountryById(int id)
        {
            return phoneContext.Countries.Where(c => c.CountryId.Equals(id));
        }

        public IEnumerable<Country> AddCountry(Country country)
        {
            phoneContext.Countries.Add(country);
            phoneContext.SaveChanges();
            return phoneContext.Countries;
        }

        public Country UpdateCountries(Country country)
        {
            var update= phoneContext.Countries.Single(x => x.CountryId == country.CountryId);
            if (update==null)
            {
                throw new InvalidOperationException("Country Id NotFound");
            }
            update.CountryName = country.CountryName;
            phoneContext.SaveChanges();
            return country;
        }
        public Country DeleteCountries(int id)
        {

            var val = phoneContext.Countries.Single(x => x.CountryId.Equals(id));
            if(val==null)
            {
                throw new InvalidOperationException("Record Not Found, It may be removed");
            }

            val.IsActive = false;

            phoneContext.SaveChanges();

            return val;

        }
    }
}
