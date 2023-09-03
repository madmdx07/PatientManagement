using PatientManagementProject.Models;
using PatientMgmtProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            var doctors = from doc in db.tblDoctors
                          join sec in db.tblSectors on doc.SecId equals sec.SecId
                          join day in db.tblDays on doc.DayId equals day.DayId
                          orderby doc.DayId
                          select new DoctorModel
                          {
                              DocId = doc.DocId,
                              DocName = doc.DocName,
                              SecId = doc.SecId,
                              DayId = doc.DayId,
                              SecName = sec.SecName,
                              DayName = day.DayName,
                              Activity = doc.Activity,
                          };
            return View(doctors);
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
            if(!ModelState.IsValid)
            {
                return View(imodel);
            }
            tblDoctor td = new tblDoctor();
            td.DocId = imodel.model.DocId;
            td.DayId = imodel.model.DayId;
            td.DocName = imodel.model.DocName;
            td.SecId = imodel.model.SecId;
            td.Activity = imodel.model.Activity;
                db.tblDoctors.Add(td);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        //GET: Details
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
                              DayName = day.DayName,
                              Activity = doc.Activity,
                          };
            var doctorinfo = doctors.FirstOrDefault();
            if (doctorinfo == null)
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
            INOFOModel docInfo = new INOFOModel();

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
                                Activity = doc.Activity,
                            };
            if (Modeldata == null)
            {
                return HttpNotFound();
            }

            docInfo.model = Modeldata.FirstOrDefault();
            docInfo.DayList = db.tblDays.Select(x => new Dropdown
            {
                Id = x.DayId,
                value = x.DayName,
            }).ToList();

            docInfo.SecList = db.tblSectors.Select(x => new Dropdown
            {
                Id = x.SecId,
                value = x.SecName,
            }).ToList();

            return View(docInfo);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(INOFOModel imodel)
        {
            tblDoctor td = new tblDoctor();
            td.DocId = imodel.model.DocId;
            td.DayId = imodel.model.DayId;
            td.DocName = imodel.model.DocName;
            td.SecId = imodel.model.SecId;
            td.Activity = imodel.model.Activity;
            if (ModelState.IsValid)
            {
                db.Entry(td).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(imodel);
        }
        //GET: Doctor/ActiveDoctors
        public ActionResult ActiveDoctors()
        {
            var doctors = from doc in db.tblDoctors
                          join sec in db.tblSectors on doc.SecId equals sec.SecId
                          join day in db.tblDays on doc.DayId equals day.DayId
                          orderby doc.DayId
                          where doc.Activity == true
                          select new DoctorModel
                          {
                              DocId = doc.DocId,
                              DocName = doc.DocName,
                              SecId = doc.SecId,
                              DayId = doc.DayId,
                              SecName = sec.SecName,
                              DayName = day.DayName,
                              Activity = doc.Activity,
                          };
            return View(doctors);
        }

        //Get: Delete
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var doctors = from doc in db.tblDoctors
        //                  join sec in db.tblSectors on doc.SecId equals sec.SecId
        //                  join day in db.tblDays on doc.DayId equals day.DayId
        //                  where doc.DocId == id
        //                  select new DoctorModel
        //                  {
        //                      DocId = doc.DocId,
        //                      DocName = doc.DocName,
        //                      SecId = doc.SecId,
        //                      DayId = doc.DayId,
        //                      SecName = sec.SecName,
        //                      DayName = day.DayName
        //                  };
        //    var doctorinfo = doctors.FirstOrDefault();
        //    if (doctorinfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(doctorinfo);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    //var ifo = db.tblDoctors.Where(x=>x.DayId==true).ToList();
        //    var doctor  = db.tblDoctors.Where(x=>x.DocId== id).FirstOrDefault();

        //    db.tblDoctors.Remove(doctor);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}