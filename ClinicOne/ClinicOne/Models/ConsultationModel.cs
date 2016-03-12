using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicOne.Models
{
    public class ConsultationModel
    {
        public Guid Id { get; set; }
        public string AspNetUserId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime TransactonDate { get; set; }
        public string PatientfullName { get; set; }
        
    }

    public class ConsultationDiagnosisModel
    {
        public Guid Id { get; set; }
        public string Diagnosis { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }

    }

    public class PatientRecordModel
    {
        public Guid Id { get; set; }
        public string RecordType { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }
        public string RecordValue { get; set; } 


        public Guid ConsultationId { get; set; }



    }

    public class PatientPrescribeMedicationModel
    {
        public Guid Id { get; set; }
        public string Medication { get; set; }
        public Guid ConsultationId { get; set; }
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }

    }

    public class PatientOtherServiceModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ConsultationId { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }

    }

    public class PatientLabModel
    {
        public Guid Id { get; set; }
        public string RecordType { get; set; }
        public string RecordValue { get; set; }
        public Guid ConsultationId { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }


    }

}