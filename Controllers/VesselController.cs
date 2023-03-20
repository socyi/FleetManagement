using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FleetManagement.Models;

namespace FleetManagement.Controllers
{
    public class VesselController : Controller
    {
        private FleetManagementEntities db = new FleetManagementEntities();

        // GET: Vessel
        public ActionResult Index()
        {
            var vessels = db.Vessels.Include(v => v.Fleet);
            return View(vessels.ToList());
        }

        // GET: Vessel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vessel vessel = db.Vessels.Find(id);
            if (vessel == null)
            {
                return HttpNotFound();
            }
            return View(vessel);
        }

        // GET: Vessel/Create
        public ActionResult Create()
        {
            ViewBag.FleetID = new SelectList(db.Fleets, "FleetID", "FleetName");
            return View();
        }

        // POST: Vessel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VesselID,VesselName,MaxLoad,FleetID")] Vessel vessel)
        {
            if (ModelState.IsValid)
            {
                db.Vessels.Add(vessel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FleetID = new SelectList(db.Fleets, "FleetID", "FleetName", vessel.FleetID);
            return View(vessel);
        }

        // GET: Vessel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vessel vessel = db.Vessels.Find(id);
            if (vessel == null)
            {
                return HttpNotFound();
            }
            ViewBag.FleetID = new SelectList(db.Fleets, "FleetID", "FleetName", vessel.FleetID);
            return View(vessel);
        }

        // POST: Vessel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VesselID,VesselName,MaxLoad,FleetID")] Vessel vessel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vessel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FleetID = new SelectList(db.Fleets, "FleetID", "FleetName", vessel.FleetID);
            return View(vessel);
        }

        // GET: Vessel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vessel vessel = db.Vessels.Find(id);
            if (vessel == null)
            {
                return HttpNotFound();
            }
            return View(vessel);
        }

        // POST: Vessel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vessel vessel = db.Vessels.Find(id);
            db.Vessels.Remove(vessel);
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
