using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientMgmtProject.Models
{
    public class DoctorModel
    {
        public int DocId { get; set; }

        [Display(Name = "Doctor")]
        public string DocName { get; set; }

        [Display(Name = "Sector")]
        public string SecName { get; set; }
        public string DayName { get; set; }
        public Nullable<int> SecId { get; set; }
        public Nullable<int> DayId { get; set; }
        public List<Dropdown> SecList { get; set; }
        public List<Dropdown> DayList { get; set; }
    }

    public class SectorModel
    {
        public int SecId { get; set; }
        public string SecName { get; set; }
        public string DayName { get; set; }
        public Nullable<int> DayId { get; set; }
    }

    public class PatientModel
    {
        public int PId { get; set; }

        [Display(Name = "Name")]
        public string PName { get; set; }
        public string Address { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> SecId { get; set; }

        [Display(Name = "Registered on:")]
        public Nullable<DateTime> DateTime { get; set; }

        [Display(Name = "Sector")]
        public string SecName { get; set; }

        [Display(Name = "Doctor")]
        public string DocName { get; set; }
    }

    public class Dropdown
    {
        public int Id { get; set; }
        public string value { get; set; }
    }

    public class INOFOModel
    {
        public DoctorModel model { get; set; }
        public List<Dropdown> SecList { get; set; }
        public List<Dropdown> DayList { get; set; }
    }

    public class SectorInfoModel
    {
        public SectorModel model { get; set; }
        public List<Dropdown> DayList { get; set; }
    }

    public class PatientInfoModel
    {
        public PatientModel model { get; set; }
        public List<Dropdown> SecList { get; set; }
        public List<Dropdown> DocList { get; set; }
    }
}