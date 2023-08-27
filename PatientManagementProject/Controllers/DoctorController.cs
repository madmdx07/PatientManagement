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
    public class DoctorController : Controller
    {
        PatientDbEntities db = new PatientDbEntities();
        // GET: Doctor
        public ActionResult Index()
        {
            var doctors = from doc in db.tblDoctors
                          join sec in db.tblSectors on doc.SecId equals sec.SecId
                          join day in db.tblDays on doc.DayId equals day.DayId
                          select new DoctorModel
                          {
                              DocId = doc.DocId,
                              DocName = doc.DocName,
                              SecId = doc.SecId,
                              DayId = doc.DayId,
                              SecName = sec.SecName,
                              DayName = day.DayName
                          };
            return View(doctors.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.DayId = new SelectList(db.tblDays, "DayId", "DayName");
            ViewBag.SecId = new SelectList(db.tblSectors, "SecId", "SecName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocId,DocName,SecId,DayId")] tblDoctor tblDoctor)
        {
            if (ModelState.IsValid)
            {
                db.tblDoctors.Add(tblDoctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayId = new SelectList(db.tblDays, "DayId", "DayName", tblDoctor.DayId);
            ViewBag.SecId = new SelectList(db.tblSectors, "SecId", "SecName", tblDoctor.SecId);
            return View(tblDoctor);
        }

        //GET edit page
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            if (tblDoctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayId = new SelectList(db.tblDays, "DayId", "DayName", tblDoctor.DayId);
            ViewBag.SecId = new SelectList(db.tblSectors, "SecId", "SecName", tblDoctor.SecId);
            return View(tblDoctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocId,DocName,SecId,DayId")] tblDoctor tblDoctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDoctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayId = new SelectList(db.tblDays, "DayId", "DayName", tblDoctor.DayId);
            ViewBag.SecId = new SelectList(db.tblSectors, "SecId", "SecName", tblDoctor.SecId);
            return View(tblDoctor);
        }

        //GET details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            if (tblDoctor == null)
            {
                return HttpNotFound();
            }
            return View(tblDoctor);
        }

        //GET delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            if (tblDoctor == null)
            {
                return HttpNotFound();
            }
            return View(tblDoctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            db.tblDoctors.Remove(tblDoctor);
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