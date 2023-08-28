using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatientManagementProject.Models
{
    public class DoctorModel
    {
        public int DocId { get; set; }
        public string DocName { get; set; }
        public string SecName { get; set; }
        public string DayName { get; set; }
        public Nullable<int> SecId { get; set; }
        public Nullable <int> DayId { get; set; }

    }

    public class SectorModel
    {
        public int SecId { get; set; }
        public string SecName { get; set; }
        public int DayID { get; set; }
    }

    public class PatientModel
    {
        public int PId { get; set; }
        public string PName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int DocId { get; set; }
        public int SecId { get; set; }
        public string SecName { get; set; }
        public string DocName { get; set; }
    }

    public class Dropdown
    {
        public int Id { get; set;}
        public List<SelectListItem> ItemList { get; set;}
    }
}
