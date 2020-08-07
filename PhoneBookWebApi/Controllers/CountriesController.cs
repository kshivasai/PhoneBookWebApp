using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using PhoneBook.Service;
using PhoneBook.DAL;


namespace PhoneBookWebApi.Controllers
{
    public class CountriesController : ApiController
    {
        CountryService countryService;
        public CountriesController()
        {
            countryService = new CountryService();
        }


        // GET: Country
        public IEnumerable<Country> Get()
        {
            return countryService.Get();
        }
    }
}
