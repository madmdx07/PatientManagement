using PatientManagementProject.Models;
using PatientMgmtProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace PatientMgmtProject.Controllers
{
    public class SectorController : Controller
    {
        private PatientDbEntities db = new PatientDbEntities();

        // GET: Sector
        public ActionResult Index()
        {
            var sectors = from sec in db.tblSectors
                          join day in db.tblDays on sec.DayId equals day.DayId
                          select new SectorInfoModel
                          {
                              SecId = sec.SecId,
                              DayId = sec.DayId,
                              SecName = sec.SecName,
                              DayName = day.DayName
                          };
            return View(sectors.ToList());
        }

        //GET: Create for Sectors
        public ActionResult Create()
        {

            SectorInfoModel secinfo = new SectorInfoModel();

            secinfo.DayList = db.tblDays.Select(x => new Dropdown
            {
                Id = x.DayId,
                value = x.DayName,
            }).ToList();
            return View(secinfo);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SectorInfoModel imodel)
        {
            tblSector ts = new tblSector();
            ts.SecId = imodel.model.SecId;
            ts.DayId = imodel.model.DayId;
            ts.SecName = imodel.model.SecName;

            if (ModelState.IsValid)
            {
                db.tblSectors.Add(ts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imodel);
        }

        //GET: edit for sectors
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            SectorInfoModel sectorInfoModel = new SectorInfoModel();
            sectorInfoModel = db.tblSectors.Where(x => x.SecId == id).Select(x=>
                new SectorInfoModel {
                SecId = x.SecId,
                DayId = x.DayId,
                SecName=x.SecName,
                }).FirstOrDefault();
           
            sectorInfoModel.DayList = db.tblDays.Select(x => new Dropdown
            {
                Id = x.DayId,
                value = x.DayName,
            }).ToList();
            return View(sectorInfoModel);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectorInfoModel sectorInfoModel)
        {
            tblSector ts = new tblSector();
            ts.SecId = sectorInfoModel.SecId;
            ts.SecName = sectorInfoModel.SecName;
            ts.DayId = sectorInfoModel.DayId; 
            if (ModelState.IsValid)
            {
                db.Entry(ts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return  View(sectorInfoModel);
        }
    }
}