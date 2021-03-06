﻿using System;
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
        public DateTime TransactionDate { get; set; }
        public string PatientfullName { get; set; }
        

        public virtual List<ConsultationDiagnosisModel> DiagnosisList { get; set; }
        public virtual List<PatientRecordModel> RecordList { get; set; }
        public virtual List<PatientPrescribeMedicationModel> PrescribeMedicationList { get; set; }
        public virtual List<PatientOtherServiceModel> OtherServiceList { get; set; }
        public virtual List<PatientSummaryLabModel> LabModelList { get; set; }
    }

    public class ConsultationDiagnosisModel
    {
        public Guid Id { get; set; }
        public string Diagnosis { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }
        public Guid ConsultationId { get; set; }
    }

    public class PatientRecordModel
    {
        public Guid Id { get; set; }
        public string RecordTypeName { get; set; }
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
        public string Strength { get; set; }
        public string Volume { get; set; }
        public string Frequency { get; set; }
        public string Route { get; set; }

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
        public string RecordCategoryName { get; set; }
        public string RecordTypeName { get; set; }
        public string RecordValue { get; set; }
        public Guid ConsultationId { get; set; }
        public string Remarks { get; set; }
       

    }


    public class PatientSummaryLabModel
    {

        public string RecordCategoryName { get; set; }
        public Guid ConsultationId { get; set; }
        public string Remarks { get; set; }


    }
}