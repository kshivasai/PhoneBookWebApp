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
                var people = db.Peoples.Where(p => p.FirstName.Contains(searchString) ||
                                                   p.LastName.Contains(searchString) ||
                                                   p.Country.CountryName.Contains(searchString) ||
                                                   p.State.StateName.Contains(searchString) ||
                                                   p.City.CityName.Contains(searchString) ||
                                                   p.Email.Contains(searchString) ||
                                                   p.PhoneNumber.Contains(searchString));
                people = people.Where(p => p.IsActive.Equals(true));
                return View(people.ToList());
            }
            var peoples = db.Peoples.Include(p => p.City).Include(p => p.Country).Include(p => p.State);
            peoples = peoples.Where(p => p.IsActive.Equals(true));
            return View(peoples.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.Peoples.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            return View(people);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName");
            ViewBag.CountryId = new SelectList(db.Countries, "CuntryId", "CountryName");
            ViewBag.StateId = new SelectList(db.States, "SateId", "StateName");
            return View();
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

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", people.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "CuntryId", "CountryName", people.CountryId);
            ViewBag.StateId = new SelectList(db.States, "SateId", "StateName", people.StateId);
            return View(people);
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
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", people.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "CuntryId", "CountryName", people.CountryId);
            ViewBag.StateId = new SelectList(db.States, "SateId", "StateName", people.StateId);
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
            ViewBag.CountryId = new SelectList(db.Countries, "CuntryId", "CountryName", people.CountryId);
            ViewBag.StateId = new SelectList(db.States, "SateId", "StateName", people.StateId);
            return View(people);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.Peoples.Find(id);
            if (people == null)
            {
                return HttpNotFound();
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
    }
}
