using PatientManagementProject.Models;
using PatientMgmtProject.Models;
using System;
using System.Collections.Generic;
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

            PatientInfoModel patinfo = new PatientInfoModel();

            patinfo.SecList = db.tblSectors.Select(x => new Dropdown
            {
                Id = x.SecId,
                value = x.SecName,
            }).ToList();
            patinfo.DocList = db.tblDoctors.Select(x => new Dropdown
            {
                Id = x.DocId,
                value = x.DocName,
            }).ToList();
            return View(patinfo);

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
                               SecName = sec.SecName,
                               DocName = doc.DocName
                           };
            var patient = patients.FirstOrDefault();
            if (patients == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }
    }
}