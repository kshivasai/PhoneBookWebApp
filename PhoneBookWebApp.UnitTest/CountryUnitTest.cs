using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneBookWebApp.Controllers;

namespace PhoneBookWebApp.UnitTest
{
    [TestClass]
    public class CountryUnitTest
    {
        [TestMethod]
        public void CreateCountry()
        {
            CountriesController countries = new CountriesController();
            ViewResult result = countries.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void CountryIndex()
        {
            CountriesController countries = new CountriesController();
            var result = countries.Index();

            Assert.IsNotNull(result);
        }
    }
}
