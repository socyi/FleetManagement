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
    public class ContainerController : Controller
    {
        private FleetManagementEntities db = new FleetManagementEntities();

        // GET: Container
        public ActionResult Index()
        {
            var containers = db.Containers.Include(c => c.Vessel);
            return View(containers.ToList());
        }

        // GET: Container/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Container container = db.Containers.Find(id);
            if (container == null)
            {
                return HttpNotFound();
            }
            return View(container);
        }

        // GET: Container/Create
        public ActionResult Create()
        {
            ViewBag.VesselID = new SelectList(db.Vessels, "VesselID", "VesselName");
            return View();
        }

        // POST: Container/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContainerID,ContainerName,VesselID")] Container container)
        {
            if (ModelState.IsValid)
            {

                container.Vessel = db.Vessels.Find(container.VesselID);

                if (container.Vessel.checkLoad())
                {
                    db.Containers.Add(container);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }

            ViewBag.VesselID = new SelectList(db.Vessels, "VesselID", "VesselName", container.VesselID);
            return View(container);
        }

        // GET: Container/Transfer/5
        public ActionResult Transfer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Container container = db.Containers.Find(id);
            if (container == null)
            {
                return HttpNotFound();
            }
            ViewBag.VesselID = new SelectList(db.Vessels, "VesselID", "VesselName", container.VesselID);
            return View(container);
        }

        // POST: Container/Transfer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer([Bind(Include = "ContainerID,ContainerName,VesselID")] Container container)
        {
            if (ModelState.IsValid)
            {
                container.Vessel = db.Vessels.Find(container.VesselID);
                Container containerOld = db.Containers.Find(container.ContainerID);

                if (container.VesselID != containerOld.VesselID && container.Vessel.checkLoad())
                {
                    db.Containers.Remove(containerOld);
                    db.Containers.Add(container);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.VesselID = new SelectList(db.Vessels, "VesselID", "VesselName", container.VesselID);
            return View(container);
        }

 
        // GET: Container/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Container container = db.Containers.Find(id);
            if (container == null)
            {
                return HttpNotFound();
            }
            return View(container);
        }

        // POST: Container/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Container container = db.Containers.Find(id);
            db.Containers.Remove(container);
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
