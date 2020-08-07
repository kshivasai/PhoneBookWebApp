using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phone.DAL;
using Phone.Service;
using System.Threading.Tasks;
using System.Web.Http.Cors;


namespace PhoneWebApi.Controllers
{
    //[EnableCors(origins: "http://localhost:62155/", headers: "*", methods: "*")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CountryController : ApiController
    {
        CountryService countryService;
        public CountryController()
        {
            countryService = new CountryService();
        }

        // GET: Country
        public IEnumerable<Country> Get()
        {
            return countryService.GetCountries();
        }

        public IEnumerable<Country> GetCountriesById(int Id)
        {
            return countryService.GetCountryById(Id);
        }
        public IEnumerable<Country> AddCountry(Country country)
        {
            return countryService.AddCountry(country);
        }
        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]Country country)
        {
            try
            {
                if (id == default(int))
                {
                    return NotFound();
                }
                country.CountryId = id;
                Country result = countryService.UpdateCountries(country);

                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Country result = countryService.DeleteCountries(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
