using PhoneBook.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Service
{
    public class CountryService
    {
        PersonContext personContext;
        public CountryService()
        {
            personContext = new PersonContext();
        }
        public IEnumerable<Country> Get()
        {
            return personContext.Countries.Where(c=>c.IsActive);
        }
    }
}
