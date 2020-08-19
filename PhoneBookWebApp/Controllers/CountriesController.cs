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
using System.Net.Http;
using System.Threading.Tasks;

namespace PhoneBookWebApp.Controllers
{
    public class CountriesController : Controller
    {
        private PhoneBookContext db = new PhoneBookContext();
        //static readonly HttpClient client = new HttpClient();
        // GET: Countries
        //public async Task<ActionResult> Index()
        public ActionResult Index()
        {
            //try
            //{
            //    HttpResponseMessage response = await client.GetAsync("http://localhost:63322/api/Country");
            //    response.EnsureSuccessStatusCode();
            //    var responseBody = await response.Content.ReadAsAsync<IList<Country>>();
            //    return View(responseBody);
            //}
            //catch (HttpRequestException e)
            //{
            //    ViewBag.Error = e.Message;
            //    return View("Error");
            //}
            //var countries = db.Countries.Where(c => c.IsActive).ToList();
            //return View(countries);
            return View();
            
        }

        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
           
            if (country == null || country.IsActive.Equals(false))
            {
                ViewBag.Error = "Your data Not Found";
                return View("Error");
            }
            return View(country);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryId,CountryName,IsActive")] Country country)
        {
            if (ModelState.IsValid)
            {
               
                db.Countries.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Error = "Error processing your request Check Your Url .Please try again!";
                return View("Error");
            }
            Country country = db.Countries.Find(id);
            if (country == null || country.IsActive.Equals(false))
            {
                ViewBag.Error = "Your data Not Found";
                return View("Error");
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryId,CountryName,IsActive")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                ViewBag.Error = "Error processing your request Check Your Url .Please try again!";
                return View("Error");
            }
            Country country = db.Countries.Find(id);
            if (country == null || country.IsActive.Equals(false))
            {
                ViewBag.Error = "Your data Not Found";
                return View("Error");
            }
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<People> people = db.Peoples.Where(p => p.CountryId == id).ToList();
            if(people.Count>0)
            {
                ViewBag.Error = "Cannot delete this country because this country is used in people";
                return View("Error");
            }
            else
            {
                Country country = db.Countries.Find(id);
                db.Countries.Remove(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
