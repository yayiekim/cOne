using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClinicOne.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace ClinicOne.Controllers
{
    public class ConsultationController : Controller
    {
        ClinicOneEntities db = new ClinicOneEntities();

        // GET: Consultation
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> getAdmited()
        {
            var res = await db.Waitings.Where(i => i.IsAdmitted == true).SingleAsync();

            return Json("", JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> getConsultaions(DateTime date)
        {
            var consultations = await db.Consultations.Where(i => i.TransactionDate == date).ToListAsync();

            List<ConsultationModel> consultationList = new List<ConsultationModel>();

            foreach (var consultaion in consultations)
            {
                var diagnosis = await db.ConsultationsDiagnosis.Where(i => i.ConsultationId == consultaion.Id).ToListAsync();
                List<ConsultationDiagnosisModel> diagnosisList = new List<ConsultationDiagnosisModel>();
                foreach (var diagnost in diagnosis)
                {
                    ConsultationDiagnosisModel diagnostModel = new ConsultationDiagnosisModel()
                    {
                        Id = diagnost.Id,
                        Amount = diagnost.Amount.GetValueOrDefault(0m),
                        Diagnosis = diagnost.Diagnosis,
                        Remarks = diagnost.Remarks,
                        ConsultationId = consultaion.Id
                    };

                    diagnosisList.Add(diagnostModel);
                }


                var records = await db.PatientsRecords.Where(i => i.ConsultationId == consultaion.Id).ToListAsync();
                List<PatientRecordModel> recordsList = new List<PatientRecordModel>();
                foreach (var record in records)
                {
                    PatientRecordModel recordModel = new PatientRecordModel()
                    {
                        Id = record.Id,
                        ConsultationId = consultaion.Id,
                        RecordTypeName = record.RecordType,
                        RecordValue = record.RecordValue
                       

                    };

                    recordsList.Add(recordModel);
                }



                var prescribeMeds = await db.PrescribedMedications.Where(i => i.ConsultationId == consultaion.Id).ToListAsync();
                List<PatientPrescribeMedicationModel> prescribeMedList = new List<PatientPrescribeMedicationModel>();
                foreach (var prescribeMed in prescribeMeds)
                {
                    PatientPrescribeMedicationModel prescribeMedModel = new PatientPrescribeMedicationModel()
                    {
                        Id = prescribeMed.Id,
                        Amount = prescribeMed.Amount.GetValueOrDefault(0m),
                        Medication = prescribeMed.Medication,
                        Remarks = prescribeMed.Remarks,
                        ConsultationId = consultaion.Id
                    };

                    prescribeMedList.Add(prescribeMedModel);
                }


                var otherServices = await db.ConsultationsOtherServices.Where(i => i.ConsultationId == consultaion.Id).ToListAsync();
                List<PatientOtherServiceModel> serviceList = new List<PatientOtherServiceModel>();
                foreach (var otherService in otherServices)
                {
                    PatientOtherServiceModel serviceModel = new PatientOtherServiceModel()
                    {
                        Id = otherService.Id,
                        Amount = otherService.Amount.GetValueOrDefault(0m),
                        Description = otherService.Description,
                        Remarks = otherService.Remarks,
                        ConsultationId = consultaion.Id
                    };

                    serviceList.Add(serviceModel);
                }

                
                var labs = await db.LabResults.Where(i => i.ConsultationId == consultaion.Id).ToListAsync();
                List<PatientLabModel> labList = new List<PatientLabModel>();
                foreach (var lab in labs)
                {
                    PatientLabModel serviceModel = new PatientLabModel()
                    {
                        Id = lab.Id,
                        RecordTypeName = lab.RecordType,
                        RecordValue = lab.RecordValue,
                        Remarks = lab.Remarks,
                        ConsultationId = consultaion.Id
                        
                    };

                    labList.Add(serviceModel);
                }





                ConsultationModel model = new ConsultationModel()
                {
                    AspNetUserId = User.Identity.GetUserId(),
                    PatientId = consultaion.PatientId,
                    TransactonDate = consultaion.TransactionDate,
                    Id = consultaion.Id,
                    PatientfullName =  consultaion.Patient.FirstName + " " + consultaion.Patient.MiddleName + " " + consultaion.Patient.LastName,
                    DiagnosisList = diagnosisList,
                    RecordList = recordsList,
                    PrescribeMedicationList = prescribeMedList,
                    OtherServiceList = serviceList,
                    LabModelList = labList                   


                };

                consultationList.Add(model);


            }


            return Json(consultationList,JsonRequestBehavior.AllowGet);


        }
        public async Task<JsonResult> AddConsultation(ConsultationModel consultaion)
        {
            Consultation model = new Consultation()
            {
                AspNetUserId = User.Identity.GetUserId(),
                TransactionDate = consultaion.TransactonDate,
                PatientId = consultaion.PatientId
                
            };

            db.Consultations.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);

        }

        

    }
}