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
    
    public partial class tblDoctor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblDoctor()
        {
            this.tblPatients = new HashSet<tblPatient>();
        }
    
        public int DocId { get; set; }
        public string DocName { get; set; }
        public Nullable<int> SecId { get; set; }
        public Nullable<int> DayId { get; set; }
    
        public virtual tblDay tblDay { get; set; }
        public virtual tblSector tblSector { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPatient> tblPatients { get; set; }
    }
}
