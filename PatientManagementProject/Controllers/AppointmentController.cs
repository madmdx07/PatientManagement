using PatientManagementProject.Models;
using PatientMgmtProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementProject.Controllers
{
    public class AppointmentController : Controller
    {
        PatientDbEntities db = new PatientDbEntities();
        // GET: Register
        public ActionResult Register()
        {
            var patients = from pat in db.tblPatients
                           join doc in db.tblDoctors on pat.DocId equals doc.DocId
                           join sec in db.tblSectors on pat.SecId equals sec.SecId
                           join day in db.tblDays on doc.DayId equals day.DayId
                           orderby day.DayName
                           select new PatientModel
                           {
                               //PId = pat.PId,
                               //PName = pat.PName,
                               //SecId = pat.SecId,
                               //Address = pat.Address,
                               //Age = pat.Age,
                               //DateTime = (DateTime)pat.DateTime,
                               //DocId = pat.DocId,
                               //SecName = sec.SecName,
                               //DocName = doc.DocName,
                               //DayName = day.DayName,
                               //Appointment = pat.Appointment,
                               Appointment = null,
                           };

            return View(patients.FirstOrDefault());
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(PatientInfoModel imodel)
        {
            if (!ModelState.IsValid) 
            { 
                return View(imodel);
            }

            tblPatient tp = new tblPatient();
            tp.PId = imodel.model.PId;
            tp.DocId = imodel.model.DocId;
            tp.SecId = imodel.model.SecId;
            tp.PName = imodel.model.PName;
            tp.Address = imodel.model.Address;
            tp.Age = imodel.model.Age;
            tp.Appointment = imodel.model.Appointment;
            tp.DateTime = DateTime.Now;
            db.tblPatients.Add(tp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}