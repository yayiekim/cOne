using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicOne.Models
{
    public class WaitingModels
    {

    }

    public class WaitingPatient
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string PatientFullName { get; set; }
        public DateTime Schedule { get; set; }
        public string Remarks { get; set; }
    }

}