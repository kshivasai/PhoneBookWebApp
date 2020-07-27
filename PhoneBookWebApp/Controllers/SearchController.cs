using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneBookWebApp.DAL;
using PhoneBookWebApp.Models;

namespace PhoneBookWebApp.Controllers
{
    public class SearchController : Controller
    {
        private PhoneBookContext db =new PhoneBookContext();
        // GET: Search
        
        public ActionResult Index()
        {
            List<SelectListItem> search = new List<SelectListItem>();
            search.Add(new SelectListItem { Text = "Select", Value = "0" });
            search.Add(new SelectListItem { Text = "City", Value = "1" });
            search.Add(new SelectListItem { Text = "State", Value = "2" });
            search.Add(new SelectListItem { Text = "Country", Value = "3" });
            search.Add(new SelectListItem { Text = "Pincode", Value = "4" });
            ViewBag.Search = search;
            return View();
        }
        
        public JsonResult GetResult(String Id)
        {
            var results = from p in db.Peoples
                          group p.FirstName by p.CityId into g
                          select new
                          {
                              CityId = g.Key,
                              PersonName = g.ToList()
                          };


            //var results = from p in db.Peoples
            //              join c in db.Cities on
            //               p.CityId equals c.CityId

            //              group p.PhoneNumber by p.CityId into g
            //              select new
            //              {
            //                  g.Key,
            //                  p.LastName,
            //                  p.PhoneNumber,
            //                  c.CityName
            //              };

            //var results = (from p in db.Peoples
            //              join c in db.Cities on p.CityId equals c.CityId



            //              group new { p.FirstName, p.PhoneNumber }
            //                 by new { p.CityId } into g
            //              orderby g.Key.CityId
            //              select new
            //              {
            //                  Firstname = g.Select(p=>p.FirstName ),
            //                  phonenumber=g.Select(p=>p.PhoneNumber)

            //              }).ToList();

            //var results = (from p in db.Peoples
            //                    group p by p.CityId).ToList();
            ////iterate each group        
            //foreach (var ageGroup in results)
            //{
            //    Console.WriteLine("Age Group: {0}", ageGroup.Key); //Each group has a key 

            //    foreach (People s in ageGroup) // Each group has inner collection
            //        Console.WriteLine("Student Name: {0}", s.FirstName);
            //}
           



            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}