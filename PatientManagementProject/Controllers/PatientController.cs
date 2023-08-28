using PatientManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementProject.Controllers
{
    public class PatientController : Controller
    {
        PatientDbEntities db = new PatientDbEntities();
        // GET: Patient
        public ActionResult Index()
        {
            List<tblPatient> patients = db.tblPatients.ToList();
            return View(patients);
        }

        //GET: Register

        public ActionResult Register()
        {
            var patients = from pat in db.tblPatients
                          join sec in db.tblSectors on pat.SecId equals sec.SecId
                          join doc in db.tblDoctors on pat.DocId equals doc.DocId
                          select new PatientModel
                          {
                              PId  = pat.PId,
                              DocId = doc.DocId,
                              DocName = doc.DocName,
                              SecId = doc.SecId,
                              SecName = sec.SecName
                          };
            var docs = from doc in db.tblDoctors
                       select new Dropdown
                       {
                           Id = doc.DocId,
                           ItemList = doc.DocName.ToList()
                       };
            var secs = from sec in db.tblSectors
                       select new Dropdown
                       {
                           Id = sec.SecId,
                           ItemList = new List<SelectListItem> { sec.SecName },
                           ItemList = sec.SecName.ToList()
                       };

            //ViewBag.DayId = new SelectList(db.tblDays, "DayId", "DayName");
            ViewBag.DocId = new SelectList(db.tblDoctors, "DocId", "DocName");
            ViewBag.SecId = new SelectList(db.tblSectors, "SecId", "SecName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "PId,PName,Address,Age,SecId,DocId")] tblPatient tblPatient)
        {
            if (ModelState.IsValid)
            {
                tblPatient.DateTime = DateTime.Now;
                db.tblPatients.Add(tblPatient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SecId = new SelectList(db.tblSectors, "SecId", "SecName", tblPatient.SecId);
            ViewBag.DocId = new SelectList(db.tblDoctors, "DocId", "DocName", tblPatient.DocId);
            return View(tblPatient);
        }

        //GET: edit page
        public ActionResult Edit()
        {
            return View();
        }
    }
}