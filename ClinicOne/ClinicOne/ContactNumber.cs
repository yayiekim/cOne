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
    
    public partial class ContactNumber
    {
        public System.Guid Id { get; set; }
        public string AspNetUserId { get; set; }
        public int CommunicationTypeId { get; set; }
        public string CommunicationValue { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual CommunicationType CommunicationType { get; set; }
    }
}
