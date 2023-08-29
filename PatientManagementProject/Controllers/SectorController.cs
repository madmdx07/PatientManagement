using PatientManagementProject.Models;
using PatientMgmtProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                          select new SectorModel
                          {

                              SecId = sec.SecId,
                              DayId = sec.DayId,
                              SecName = sec.SecName,
                              DayName = day.DayName
                          };
            return View(sectors);
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


    }
}