using ClinicOne.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClinicOne.Controllers
{
    public class ReportsController : Controller
    {

        ClinicOneEntities db = new ClinicOneEntities();

        // GET: Reports
        public ActionResult PatientRecords()
        {
            return View();
        }

        //public async Task<JsonResult> getPatientRecord(Guid PatientId)
        //{
        //    List<ConsultationModel> thelist = new List<ConsultationModel>();

        //    var consultations = await db.Consultations.Where(i => i.PatientId == PatientId).ToListAsync();

        //    foreach (var consultation in consultations)
        //    {
        //        var diagnosis = await db.ConsultationsDiagnosis.Where(i => i.ConsultationId == consultation.Id).ToListAsync();
        //        List<ConsultationDiagnosisModel> diagnosisList = new List<ConsultationDiagnosisModel>();
        //        foreach (var diagnost in diagnosis)
        //        {
        //            ConsultationDiagnosisModel diagnostModel = new ConsultationDiagnosisModel()
        //            {
        //                Id = diagnost.Id,
        //                Amount = diagnost.Amount.GetValueOrDefault(0m),
        //                Diagnosis = diagnost.Diagnosis,
        //                Remarks = diagnost.Remarks,
        //                ConsultationId = consultation.Id
        //            };

        //            diagnosisList.Add(diagnostModel);
        //        }


        //        var records = await db.PatientsRecords.Where(i => i.ConsultationId == consultation.Id).ToListAsync();
        //        List<PatientRecordModel> recordsList = new List<PatientRecordModel>();
        //        foreach (var record in records)
        //        {
        //            PatientRecordModel recordModel = new PatientRecordModel()
        //            {
        //                Id = record.Id,
        //                ConsultationId = consultation.Id,
        //                RecordTypeName = record.RecordType,
        //                RecordValue = record.RecordValue


        //            };

        //            recordsList.Add(recordModel);
        //        }



        //        var prescribeMeds = await db.PrescribedMedications.Where(i => i.ConsultationId == consultation.Id).ToListAsync();
        //        List<PatientPrescribeMedicationModel> prescribeMedList = new List<PatientPrescribeMedicationModel>();
        //        foreach (var prescribeMed in prescribeMeds)
        //        {
        //            PatientPrescribeMedicationModel prescribeMedModel = new PatientPrescribeMedicationModel()
        //            {
        //                Id = prescribeMed.Id,
        //                Amount = prescribeMed.Amount.GetValueOrDefault(0m),
        //                Medication = prescribeMed.Medication,
        //                Remarks = prescribeMed.Remarks,
        //                Quantity = prescribeMed.Quantity.GetValueOrDefault(0),
        //                ConsultationId = consultation.Id
        //            };

        //            prescribeMedList.Add(prescribeMedModel);
        //        }


        //        var otherServices = await db.ConsultationsOtherServices.Where(i => i.ConsultationId == consultation.Id).ToListAsync();
        //        List<PatientOtherServiceModel> serviceList = new List<PatientOtherServiceModel>();
        //        foreach (var otherService in otherServices)
        //        {
        //            PatientOtherServiceModel serviceModel = new PatientOtherServiceModel()
        //            {
        //                Id = otherService.Id,
        //                Amount = otherService.Amount.GetValueOrDefault(0m),
        //                Description = otherService.Description,
        //                Remarks = otherService.Remarks,
        //                ConsultationId = consultation.Id
        //            };

        //            serviceList.Add(serviceModel);
        //        }


        //        var labs = await db.LabResults.Where(i => i.ConsultationId == consultation.Id).ToListAsync();
        //        List<PatientSummaryLabModel> labList = new List<PatientSummaryLabModel>();
        //        foreach (var lab in labs)
        //        {
        //            PatientSummaryLabModel serviceModel = new PatientSummaryLabModel()
        //            {
        //                Id = lab.Id,
        //                RecordTypeName = lab.RecordType,
        //                RecordValue = lab.RecordValue,
        //                Remarks = lab.Remarks,
        //                ConsultationId = consultation.Id

        //            };

        //            labList.Add(serviceModel);
        //        }

        //        ConsultationModel ConModel = new ConsultationModel()
        //        {
        //            AspNetUserId = User.Identity.GetUserId(),
        //            PatientId = consultation.PatientId,
        //            TransactonDate = consultation.TransactionDate,
        //            Id = consultation.Id,
        //            PatientfullName = consultation.Patient.FirstName + " " + consultation.Patient.MiddleName + " " + consultation.Patient.LastName,
        //            DiagnosisList = diagnosisList,
        //            RecordList = recordsList,
        //            PrescribeMedicationList = prescribeMedList,
        //            OtherServiceList = serviceList,
        //            LabModelList = labList


        //        };

        //        thelist.Add(ConModel);

        //    }


        //    var x = await db.Patients.FindAsync(PatientId);

        //    DateTime dob = x.BirthDate;
        //    DateTime PresentYear = DateTime.Now;
        //    TimeSpan ts = PresentYear - dob;
        //    DateTime Age = DateTime.MinValue.AddDays(ts.Days);

        //    PatientModel model = new PatientModel()
        //    {

        //        Id = x.Id,
        //        Address1 = x.Address1,
        //        Address2 = x.Address2,
        //        Age = Age.Year - 1,
        //        BirthDate = x.BirthDate,
        //        BloodType = x.BloodType,
        //        ContactNumber1 = x.ContactNumber1,
        //        ContactNumber2 = x.ContactNumber2,
        //        FirstName = x.FirstName,
        //        MiddleName = x.MiddleName,
        //        LastName = x.LastName,
        //        Gender = x.Gender,
        //        Consultations = thelist
                
        //    };

        //    return Json(model, JsonRequestBehavior.AllowGet);

        //}


        public async Task<JsonResult> getIncome(DateTime FromDate, DateTime ToDate)
        {
            //from medication, consultation and other service

            return Json("", JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> getInsurancePaidTransaction(DateTime FromDate, DateTime ToDate)
        {

            return Json("", JsonRequestBehavior.AllowGet);

        }


    }
}