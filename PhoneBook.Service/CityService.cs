using PhoneBook.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Service
{
    public class CityService
    {
        PersonContext personContext;
        public CityService()
        {
            personContext = new PersonContext();  
        }
        public IEnumerable<City> GetCities()
        {
            return personContext.Cities;
        }
    }
}
