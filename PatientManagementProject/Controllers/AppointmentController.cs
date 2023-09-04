using PatientManagementProject.Models;
using PatientMgmtProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementProject.Controllers
{
    public class AppointmentController : Controller
    {
        PatientDbEntities db = new PatientDbEntities();
        // GET: Register
        public ActionResult Register()
        {   PatientModel model = new PatientModel();
            //IQueryable <PatientModel> patients = from pat in db.tblPatients
            //               join doc in db.tblDoctors on pat.DocId equals doc.DocId
            //               join sec in db.tblSectors on pat.SecId equals sec.SecId
            //               join day in db.tblDays on doc.DayId equals day.DayId
            //               orderby day.DayName
            //               select new PatientModel
            //               {
            //                   PId = pat.PId,
            //                   PName = pat.PName,
            //                   SecId = pat.SecId,
            //                   Address = pat.Address,
            //                   Age = pat.Age,
            //                   DateTime = (DateTime)pat.DateTime,
            //                   DocId = pat.DocId,
            //                   SecName = sec.SecName,
            //                   DocName = doc.DocName,
            //                   DayName = day.DayName,
            //                   Appointment = (DateTime)pat.Appointment,
            //               };
            //model = patients.
            model.Appointment = null;
            model.DocList = db.tblDoctors.Select(x => new Dropdown
            {
                Id = x.DocId,
                value = x.DocName,
            }).ToList();
            model.SecList = db.tblSectors.Select(x => new Dropdown
            {
                Id = x.SecId,
                value = x.SecName,
            }).ToList();
            return View(model);
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(PatientModel imodel)
        {
            if (!ModelState.IsValid) 
            { 
                return View(imodel);
            }

            tblPatient tp = new tblPatient();
            tp.PId = imodel.PId;
            tp.DocId = imodel.DocId;
            tp.SecId = imodel.SecId;
            tp.PName = imodel.PName;
            tp.Address = imodel.Address;
            tp.Age = imodel.Age;
            tp.Appointment = imodel.Appointment;
            tp.DateTime = DateTime.Now;
            db.tblPatients.Add(tp);
            db.SaveChanges();
            return RedirectToAction("Index","Patient");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}