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
    
    public partial class Waiting
    {
        public System.Guid Id { get; set; }
        public System.Guid PatientId { get; set; }
        public System.DateTime Schedule { get; set; }
        public string Remarks { get; set; }
        public string AspNetUserId { get; set; }
    
        public virtual Patient Patient { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
