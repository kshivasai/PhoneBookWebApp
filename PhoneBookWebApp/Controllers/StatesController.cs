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
    public class StatesController : Controller
    {
        private PhoneBookContext db = new PhoneBookContext();

        // GET: States
        public ActionResult Index()
        {
            var states = db.States.Include(s => s.Country);
            states = states.Where(s => s.IsActive.Equals(true));
            return View(states.ToList());
        }

        // GET: States/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                ViewBag.Error = "Error processing your request Check Your Url .Please try again!";
                return View("Error");
            }
            State state = db.States.Find(id);
            if (state == null)
            {
                ViewBag.Error = "Your data Not Found";
                return View("Error");
            }
            return View(state);
        }

        // GET: States/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName");
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StateId,StateName,IsActive,CountryId")] State state)
        {
            if (ModelState.IsValid)
            {
                db.States.Add(state);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", state.CountryId);
            return View(state);
        }

        // GET: States/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Error = "Error processing your request Check Your Url .Please try again!";
                return View("Error");
            }
            State state = db.States.Find(id);
            if (state == null)
            {
                ViewBag.Error = "Your data Not Found";
                return View("Error");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", state.CountryId);
            return View(state);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StateId,StateName,IsActive,CountryId")] State state)
        {
            if (ModelState.IsValid)
            {
                db.Entry(state).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", state.CountryId);
            return View(state);
        }

        // GET: States/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                ViewBag.Error = "Error processing your request .Please try again!";
                return View("Error");
            }
            State state = db.States.Find(id);
            if (state == null)
            {
                ViewBag.Error = "Your data Not Found";
                return View("Error");
            }
            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<People> people = db.Peoples.Where(p => p.StateId == id).ToList();
            if (people.Count > 0)
            {
                ViewBag.Error = "Cannot delete this State because this country is used in people";
                return View("Error");
            }
            else
            {
                State state = db.States.Find(id);
                db.States.Remove(state);
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
