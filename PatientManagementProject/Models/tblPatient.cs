//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PatientManagementProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPatient
    {
        public int PId { get; set; }
        public string PName { get; set; }
        public string Address { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> SecId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public Nullable<System.DateTime> Appointment { get; set; }
    
        public virtual tblDoctor tblDoctor { get; set; }
        public virtual tblSector tblSector { get; set; }
    }
}
