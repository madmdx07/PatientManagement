using PatientManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementProject.Controllers
{
    public class SectorController : Controller
    {
        // GET: Sector
        PatientDbEntities db = new PatientDbEntities();
        public ActionResult Index()
        {
            List<tblSector> sectors= db.tblSectors.ToList();
            return View(sectors);
        }
        //Get: Create
        public ActionResult Create()
        {
            ViewBag.DayId = new SelectList(db.tblDays, "DayId", "DayName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SecId,SecName,DayId")] tblSector tblSector)
        {
            if (ModelState.IsValid)
            {
                db.tblSectors.Add(tblSector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayId = new SelectList(db.tblDays, "DayId", "DayName", tblSector.DayId);
            return View(tblSector);
        }

        //GET edit page
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSector tblSector = db.tblSectors.Find(id);
            if (tblSector == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayId = new SelectList(db.tblDays, "DayId", "DayName", tblSector.DayId);
            return View(tblSector);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SecID,SecName,DayId")] tblSector tblSector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayId = new SelectList(db.tblDays, "DayId", "DayName", tblSector.DayId);
            return View(tblSector);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSector tblSector = db.tblSectors.Find(id);
            if (tblSector == null)
            {
                return HttpNotFound();
            }
            return View(tblSector);
        }

        //GET delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSector tblSector = db.tblSectors.Find(id);
            if (tblSector == null)
            {
                return HttpNotFound();
            }
            return View(tblSector);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSector tblSector = db.tblSectors.Find(id);
            db.tblSectors.Remove(tblSector);
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