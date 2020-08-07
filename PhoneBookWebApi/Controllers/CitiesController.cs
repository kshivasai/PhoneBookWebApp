using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using PhoneBook.DAL;
using PhoneBook.Service;

namespace PhoneBookWebApi.Controllers
{
    public class CitiesController : ApiController
    {
        CityService cityService;

        public CitiesController()
        {

        }
        public IEnumerable<City> Get()
        {
            return cityService.GetCities();
        }

        //public IEnumerable<City> GetCities()
        //{
        //    db.Configuration.LazyLoadingEnabled = false;
        //    var cities = db.Cities.Include(c => c.State).Where(c => c.IsActive.Equals(true)).AsEnumerable();

        //    return cities;
        //}
       
       
    }
}
