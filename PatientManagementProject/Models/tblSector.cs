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
    
    public partial class tblSector
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSector()
        {
            this.tblDoctors = new HashSet<tblDoctor>();
            this.tblPatients = new HashSet<tblPatient>();
        }
    
        public int SecId { get; set; }
        public string SecName { get; set; }
        public Nullable<int> DayId { get; set; }
    
        public virtual tblDay tblDay { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDoctor> tblDoctors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPatient> tblPatients { get; set; }
    }
}
