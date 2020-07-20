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
    public class AddressesController : Controller
    {
        private PhoneBookContext db = new PhoneBookContext();

        // GET: Addresses
        public ActionResult Index()
        {
            var addresses = db.Addresses.Include(a => a.City).Include(a => a.Country).Include(a => a.People).Include(a => a.State);
            return View(addresses.ToList());
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "ID", "CityName");
            ViewBag.CountryId = new SelectList(db.Countries, "ID", "CountryName");
            ViewBag.PeopleId = new SelectList(db.Peoples, "ID", "FirstName");
            ViewBag.StateId = new SelectList(db.States, "ID", "StateName");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AddressOne,AddressTwo,PinCode,CountryId,StateId,CityId,PeopleId")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "ID", "CityName", address.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "ID", "CountryName", address.CountryId);
            ViewBag.PeopleId = new SelectList(db.Peoples, "ID", "FirstName", address.PeopleId);
            ViewBag.StateId = new SelectList(db.States, "ID", "StateName", address.StateId);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "ID", "CityName", address.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "ID", "CountryName", address.CountryId);
            ViewBag.PeopleId = new SelectList(db.Peoples, "ID", "FirstName", address.PeopleId);
            ViewBag.StateId = new SelectList(db.States, "ID", "StateName", address.StateId);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AddressOne,AddressTwo,PinCode,CountryId,StateId,CityId,PeopleId")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "ID", "CityName", address.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "ID", "CountryName", address.CountryId);
            ViewBag.PeopleId = new SelectList(db.Peoples, "ID", "FirstName", address.PeopleId);
            ViewBag.StateId = new SelectList(db.States, "ID", "StateName", address.StateId);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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
