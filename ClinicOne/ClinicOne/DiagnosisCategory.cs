//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClinicOne
{
    using System;
    using System.Collections.Generic;
    
    public partial class DiagnosisCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiagnosisCategory()
        {
            this.Diagnosis = new HashSet<Diagnosi>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string AspNetUserId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diagnosi> Diagnosis { get; set; }
    }
}
