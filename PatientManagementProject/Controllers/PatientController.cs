using PatientManagementProject.Models;
using PatientMgmtProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PatientMgmtProject.Controllers
{
    public class PatientController : Controller
    {

        private PatientDbEntities db = new PatientDbEntities();
        // GET: Patient
        public ActionResult Index()
        {
            PatientInfoModel infoModel = new PatientInfoModel();
            var patients = from pat in db.tblPatients
                           join doc in db.tblDoctors on pat.DocId equals doc.DocId
                           join sec in db.tblSectors on pat.SecId equals sec.SecId
                           select new PatientModel
                           {
                               PId = pat.PId,
                               PName = pat.PName,
                               SecId = pat.SecId,
                               Address = pat.Address,
                               Age = pat.Age,
                               DateTime = (DateTime)pat.DateTime,
                               DocId = pat.DocId,
                               SecName = sec.SecName,
                               DocName = doc.DocName
                           };

            return View(patients);
        }


        //GET: Create for Sectors
        public ActionResult Create()
        {

            PatientInfoModel patInfo = new PatientInfoModel();

            patInfo.SecList = db.tblSectors.Select(x => new Dropdown
            {
                Id = x.SecId,
                value = x.SecName,
            }).ToList();
            patInfo.DocList = db.tblDoctors.Select(x => new Dropdown
            {
                Id = x.DocId,
                value = x.DocName,
            }).ToList();
            return View(patInfo);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientInfoModel imodel)
        {
            tblPatient tp = new tblPatient();
            tp.PId = imodel.model.PId;
            tp.DocId = imodel.model.DocId;
            tp.SecId = imodel.model.SecId;
            tp.PName = imodel.model.PName;
            tp.Address = imodel.model.Address;
            tp.Age = imodel.model.Age;
            tp.DateTime = imodel.model.DateTime;


            if (ModelState.IsValid)
            {
                tp.DateTime = DateTime.Now;
                db.tblPatients.Add(tp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imodel);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var patients = from pat in db.tblPatients
                           join doc in db.tblDoctors on pat.DocId equals doc.DocId
                           join sec in db.tblSectors on pat.SecId equals sec.SecId
                           where pat.PId == id
                           select new PatientModel

                           {
                               PId = pat.PId,
                               PName = pat.PName,
                               SecId = pat.SecId,
                               Address = pat.Address,
                               Age = pat.Age,
                               DateTime = (DateTime)pat.DateTime,
                               DocId = pat.DocId,
                               //SecName = sec.SecName,
                               //DocName = doc.DocName
                           };
            var patient = patients.FirstOrDefault();

            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);

        }

        //GET: Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientInfoModel patInfo = new PatientInfoModel();

            var patients = from pat in db.tblPatients
                           join doc in db.tblDoctors on pat.DocId equals doc.DocId
                           join sec in db.tblSectors on pat.SecId equals sec.SecId
                           where pat.PId == id
                           select new PatientModel

                           {
                               PId = pat.PId,
                               PName = pat.PName,
                               SecId = pat.SecId,
                               Address = pat.Address,
                               Age = pat.Age,
                               DateTime = (DateTime)pat.DateTime,
                               DocId = pat.DocId,
                           };
            if (patients == null)
            {
                return HttpNotFound();
            }

            patInfo.model = patients.FirstOrDefault();
            patInfo.SecList = db.tblSectors.Select(x => new Dropdown
            {
                Id = x.SecId,
                value = x.SecName,
            }).ToList();
            patInfo.DocList = db.tblDoctors.Select(x => new Dropdown
            {
                Id = x.DocId,
                value = x.DocName,
            }).ToList();
            return View(patInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientInfoModel imodel)
        {
            tblPatient tp = new tblPatient();
            tp.DocId = imodel.model.DocId;
            tp.PId = imodel.model.PId;
            tp.PName = imodel.model.PName;
            tp.SecId = imodel.model.SecId;
            tp.Age = imodel.model.Age;
            tp.Address = imodel.model.Address;
            tp.DateTime = imodel.model.DateTime;

            if (ModelState.IsValid)
            {
                if (tp.DateTime == null)
                {
                    tp.DateTime = DateTime.Now;
                }
                db.Entry(tp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(imodel);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var patients = from pat in db.tblPatients
                           join doc in db.tblDoctors on pat.DocId equals doc.DocId
                           join sec in db.tblSectors on pat.SecId equals sec.SecId
                           where pat.PId == id
                           select new PatientModel
                           {
                               PId = pat.PId,
                               PName = pat.PName,
                               SecId = pat.SecId,
                               Address = pat.Address,
                               Age = pat.Age,
                               DateTime = (DateTime)pat.DateTime,
                               DocId = pat.DocId,
                               SecName = sec.SecName,
                               DocName = doc.DocName
                           };
            var patientinfo = patients.FirstOrDefault();
            if (patientinfo == null)
            {
                return HttpNotFound();
            }
            return View(patientinfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            //var ifo = db.tblDoctors.Where(x=>x.DayId==true).ToList();
            var patient = db.tblPatients.Where(x => x.PId == id).FirstOrDefault();
            db.tblPatients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}