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
    
    public partial class PatientInsurance
    {
        public System.Guid Id { get; set; }
        public string CardNumber { get; set; }
        public string AccountNumber { get; set; }
        public string InsuranceCompany { get; set; }
        public System.Guid PatientId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}