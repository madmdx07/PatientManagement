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
    public class DoctorController : Controller
    {
        private PatientDbEntities db = new PatientDbEntities();
        // GET: Doctor

        public ActionResult Index()
        {
            //DoctorModel D = new DoctorModel();
            //tblDoctor td = new tblDoctor(); 
            //tblSector st = new tblSector();
            //tblDay dt = new tblDay();
            //D.DocId = td.DocId;
            //D.DayId = td.DayId;
            //D.DocName = td.DocName;
            //D.SecId = td.SecId;
            //D.SecName = st.SecName;
            //D.DayName = dt.DayName;

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
            return View(doctors);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctors = from doc in db.tblDoctors
                          join sec in db.tblSectors on doc.SecId equals sec.SecId
                          join day in db.tblDays on doc.DayId equals day.DayId
                          where doc.DocId == id
                          select new DoctorModel
                          {
                              DocId = doc.DocId,
                              DocName = doc.DocName,
                              SecId = doc.SecId,
                              DayId = doc.DayId,
                              SecName = sec.SecName,
                              DayName = day.DayName
                          };
            var doctorinfo = doctors;
            if (doctors == null)
            {
                return HttpNotFound();
            }
            return View(doctorinfo);
        }

        //GET: edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INOFOModel a = new INOFOModel();


            var Modeldata = from doc in db.tblDoctors
                            join sec in db.tblSectors on doc.SecId equals sec.SecId
                            join day in db.tblDays on doc.DayId equals day.DayId
                            where doc.DocId == id
                            select new DoctorModel
                            {
                                DocId = doc.DocId,
                                DocName = doc.DocName,
                                SecId = doc.SecId,
                                DayId = doc.DayId,
                                //SecName = sec.SecName,
                                //DayName = day.DayName,

                            };
            if (Modeldata == null)
            {
                return HttpNotFound();
            }

            a.model = Modeldata.FirstOrDefault();
            a.DayList = db.tblDays.Select(x => new Dropdown
            {
                Id = x.DayId,
                value = x.DayName,
            }).ToList();

            a.SecList = db.tblSectors.Select(x => new Dropdown
            {
                Id = x.SecId,
                value = x.SecName,
            }).ToList();

            return View(a);
        }

        //GET: Create for doctors
        public ActionResult Create()
        {

            INOFOModel docinfo = new INOFOModel();

            docinfo.DayList = db.tblDays.Select(x => new Dropdown
            {
                Id = x.DayId,
                value = x.DayName,
            }).ToList();

            docinfo.SecList = db.tblSectors.Select(x => new Dropdown
            {
                Id = x.SecId,
                value = x.SecName,
            }).ToList();

            return View(docinfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(INOFOModel imodel)
        {
            tblDoctor td = new tblDoctor();
            td.DocId = imodel.model.DocId;
            td.DayId = imodel.model.DayId;
            td.DocName = imodel.model.DocName;
            td.SecId = imodel.model.SecId;

            if (ModelState.IsValid)
            {
                db.tblDoctors.Add(td);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imodel);
        }
    }
}