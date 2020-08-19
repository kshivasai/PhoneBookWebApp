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
    public class PeopleController : Controller
    {
        private PhoneBookContext db = new PhoneBookContext();

      
        // GET: People
        public ActionResult Index(String searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var people = db.Peoples.Where(p => (p.FirstName.Contains(searchString) ||
                                                   p.LastName.Contains(searchString) ||
                                                   p.Country.CountryName.Contains(searchString) ||
                                                   p.State.StateName.Contains(searchString) ||
                                                   p.City.CityName.Contains(searchString) ||
                                                   p.Email.Contains(searchString) ||
                                                   p.PhoneNumber.Contains(searchString)) &&
                                                   p.IsActive);
                return View(people.ToList());
            }
            var peoples = db.Peoples.Include(p => p.City).Include(p => p.Country).Include(p => p.State).ToList();
            peoples = peoples.Where(p => p.IsActive).ToList();
            return View(peoples);
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                ViewBag.Error = "Error processing your request Check Your Url .Please try again!";
                return View("Error");
            }
            People people = db.Peoples.Find(id);
            if (people == null)
            {
                ViewBag.Error = "Your data Not Found";
                return View("Error");
            }
            return View(people);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            List<Country> coun = db.Countries.ToList();
            if (coun != null)
            {
                List<Country> country = db.Countries.Where(c => c.IsActive).ToList();
                List<SelectListItem> co = new List<SelectListItem>();
                foreach (var c in country)
                {
                    co.Add(new SelectListItem
                    {
                        Text = c.CountryName,
                        Value = c.CountryId.ToString()
                    });
                    ViewBag.country = co;
                }
                return View();
            }
            else
            {
                ViewBag.Error = "Atleast one country is requried ";
                return View("Error");
            }
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,PhoneNumber,Email,AddressOne,AddressTwo,PinCode,IsActive,CountryId,StateId,CityId")] People people)
        {
            if (ModelState.IsValid)
            {
                db.Peoples.Add(people);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(people);
        }
        public JsonResult GetStates(int id)
        {
            var states = db.States.Where(s => s.CountryId == id && s.IsActive).ToList();
            List<SelectListItem> listates = new List<SelectListItem>();
            listates.Add(new SelectListItem { Text = "--Select State--", Value = "0" });
            if (states != null)
            {
                foreach (var s in states)
                {
                    listates.Add(new SelectListItem { Text = s.StateName, Value = s.StateId.ToString() });
                }
            }
            return Json(new SelectList(listates, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public JsonResult GetCities(int id)
        {
            var cities = db.Cities.Where(c => c.StateId == id && c.IsActive).ToList();
            List<SelectListItem> licity = new List<SelectListItem>();
            licity.Add(new SelectListItem { Text = "--Select City--", Value = "0" });
            if (cities != null)
            {
                foreach (var c in cities)
                {
                    licity.Add(new SelectListItem { Text = c.CityName, Value = c.CityId.ToString() });
                }

            }

            return Json(new SelectList(licity, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.Peoples.Find(id);
            if (people == null)
            {
                ViewBag.Error = "Your data Not Found";
                return View("Error");
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", people.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", people.CountryId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", people.StateId);
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,PhoneNumber,Email,AddressOne,AddressTwo,PinCode,IsActive,CountryId,StateId,CityId")] People people)
        {
            if (ModelState.IsValid)
            {
                db.Entry(people).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", people.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", people.CountryId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", people.StateId);
            return View(people);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                ViewBag.Error = "Error processing your request Check Your Url .Please try again!";
                return View("Error");
            }
            People people = db.Peoples.Find(id);
            if (people == null)
            {
                ViewBag.Error = "Your data Not Found";
                return View("Error");
            }
            return View(people);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            People people = db.Peoples.Find(id);
            db.Peoples.Remove(people);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Api()
        {
            return View();
        }
        public JsonResult GetSearchingData(string searchBy, string searchValue)
        {
            List<People> people = new List<People>();
            if (searchBy == "FirstName")
            {
                try
                {
                   
                    people = db.Peoples.Where(p=>p.FirstName.Contains(searchValue) || searchValue==null).ToList();
                }
                catch(FormatException)
                {
                    Console.WriteLine("Data not found");
                }
                return Json(people, JsonRequestBehavior.AllowGet);
            }
            else if(searchBy == "LastName")
            {
                try
                {
                    int id = Convert.ToInt32(searchValue);
                    people = db.Peoples.Where(p => p.LastName.Contains(searchValue)|| searchValue == null).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Data not found");
                }
                return Json(people, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    people = db.Peoples.Where(p => p.PhoneNumber.Contains(searchValue) || searchValue == null).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Data not found");
                }
                return Json(people, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
