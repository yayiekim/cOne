using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClinicOne.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Globalization;

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

        public ActionResult ConsultationMain()
        {
            return View();
        }

        public ActionResult ConsultationList()
        {
            return View();
        }

        public async Task<JsonResult> getAdmited()
        {
            var res = await db.Waitings.Where(i => i.IsAdmitted == true).SingleAsync();

            var x = await db.Patients.FindAsync(res.PatientId);

            DateTime dob = x.BirthDate;
            DateTime PresentYear = DateTime.Now;
            TimeSpan ts = PresentYear - dob;
            DateTime Age = DateTime.MinValue.AddDays(ts.Days);

            PatientModel model = new PatientModel()
            {
                Id = x.Id,
                Age = Age.Year - 1,
                BirthDate = dob,
                BloodType = x.BloodType,
                ContactNumber1 = x.ContactNumber1,
                ContactNumber2 = x.ContactNumber2,
                FullName = x.FirstName + " " + x.MiddleName + " " + x.LastName,
                Gender = x.Gender,
                FullAddress = x.Address1 + ", " + x.Address2

            };


            return Json(model, JsonRequestBehavior.AllowGet);

        }


        public async Task<JsonResult> cancelAdmitted(Guid id)
        {
            var res = await db.Waitings.Where(i=>i.PatientId == id).SingleAsync();

            res.IsAdmitted = false;

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> endConsultation(Guid PatientId)
        {

            var res = await db.Waitings.Where(i => i.IsAdmitted == true).SingleAsync();


            db.Waitings.Remove(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> getConsultaions(Guid PatientId)
        {
          
           var consultations = await db.Consultations.Where(i=>i.PatientId == PatientId).ToListAsync();

            List<ConsultationModel> consultationList = new List<ConsultationModel>();

            foreach (var consultaion in consultations)
            {
                var consulatation = await db.Consultations.Where(i => i.Id == consultaion.Id).ToListAsync();
               
                foreach (var diagnost in consulatation)
                {

                    ConsultationModel model = new ConsultationModel()
                    {
                        AspNetUserId = User.Identity.GetUserId(),
                        PatientId = consultaion.PatientId,
                        TransactionDate = consultaion.TransactionDate,
                        Id = consultaion.Id,
                        PatientfullName = consultaion.Patient.FirstName + " " + consultaion.Patient.MiddleName + " " + consultaion.Patient.LastName,
                      
                    };

                    consultationList.Add(model);


                }


            }

            return Json(consultationList, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> getConsultaionDetail(Guid ConsultationId)
        {

            var consultation = await db.Consultations.FindAsync(ConsultationId);


            var diagnosis = await db.ConsultationsDiagnosis.Where(i => i.ConsultationId == consultation.Id).ToListAsync();
            List<ConsultationDiagnosisModel> diagnosisList = new List<ConsultationDiagnosisModel>();
            foreach (var diagnost in diagnosis)
            {
                ConsultationDiagnosisModel diagnostModel = new ConsultationDiagnosisModel()
                {
                    Id = diagnost.Id,
                    Amount = diagnost.Amount.GetValueOrDefault(0m),
                    Diagnosis = diagnost.Diagnosis,
                    Remarks = diagnost.Remarks,
                    ConsultationId = consultation.Id
                };

                diagnosisList.Add(diagnostModel);
            }


            var records = await db.PatientsRecords.Where(i => i.ConsultationId == consultation.Id).ToListAsync();
            List<PatientRecordModel> recordsList = new List<PatientRecordModel>();
            foreach (var record in records)
            {
                PatientRecordModel recordModel = new PatientRecordModel()
                {
                    Id = record.Id,
                    ConsultationId = consultation.Id,
                    RecordTypeName = record.RecordType,
                    RecordValue = record.RecordValue


                };

                recordsList.Add(recordModel);
            }



            var prescribeMeds = await db.PrescribedMedications.Where(i => i.ConsultationId == consultation.Id).ToListAsync();
            List<PatientPrescribeMedicationModel> prescribeMedList = new List<PatientPrescribeMedicationModel>();
            foreach (var prescribeMed in prescribeMeds)
            {
                PatientPrescribeMedicationModel prescribeMedModel = new PatientPrescribeMedicationModel()
                {
                    Id = prescribeMed.Id,
                    Amount = prescribeMed.Amount.GetValueOrDefault(0m),
                    Medication = prescribeMed.Medication,
                    Remarks = prescribeMed.Remarks,
                    Quantity = prescribeMed.Quantity.GetValueOrDefault(0),
                    ConsultationId = consultation.Id,
                    Frequency = prescribeMed.Frequency,
                    Route = prescribeMed.Route,
                    Strength = prescribeMed.Strength,
                    Volume = prescribeMed.Volume
                };

                prescribeMedList.Add(prescribeMedModel);
            }


            var otherServices = await db.ConsultationsOtherServices.Where(i => i.ConsultationId == consultation.Id).ToListAsync();
            List<PatientOtherServiceModel> serviceList = new List<PatientOtherServiceModel>();
            foreach (var otherService in otherServices)
            {
                PatientOtherServiceModel serviceModel = new PatientOtherServiceModel()
                {
                    Id = otherService.Id,
                    Amount = otherService.Amount.GetValueOrDefault(0m),
                    Description = otherService.Description,
                    Remarks = otherService.Remarks,
                    ConsultationId = consultation.Id
                };

                serviceList.Add(serviceModel);
            }


            var labs = await db.LabResults.Where(i => i.ConsultationId == consultation.Id).GroupBy(i=> new  {

                ConsultationId = i.ConsultationId,
                RecordCategoryName = i.RecordCategoryName

            }).ToListAsync();


            List<PatientSummaryLabModel> labList = new List<PatientSummaryLabModel>();

            foreach (var lab in labs)
            {
                PatientSummaryLabModel serviceModel = new PatientSummaryLabModel()
                {
                    ConsultationId = lab.Key.ConsultationId,
                    RecordCategoryName = lab.Key.RecordCategoryName,
                    Remarks = ""
                    

                };

                labList.Add(serviceModel);
            }

            
            ConsultationModel model = new ConsultationModel()
            {
                AspNetUserId = User.Identity.GetUserId(),
                PatientId = consultation.PatientId,
                TransactionDate = consultation.TransactionDate,
                Id = consultation.Id,
                PatientfullName = consultation.Patient.FirstName + " " + consultation.Patient.MiddleName + " " + consultation.Patient.LastName,
                DiagnosisList = diagnosisList,
                RecordList = recordsList,
                PrescribeMedicationList = prescribeMedList,
                OtherServiceList = serviceList,
                LabModelList = labList


            };




            return Json(model, JsonRequestBehavior.AllowGet);


        }


        public async Task<JsonResult> AddConsultation(ConsultationModel consultation)
        {
           

            Consultation model = new Consultation()
            {
                AspNetUserId = User.Identity.GetUserId(),
                TransactionDate = consultation.TransactionDate,
                PatientId = consultation.PatientId

            };

            db.Consultations.Add(model);    
            await db.SaveChangesAsync();

            consultation.Id = model.Id;
            consultation.TransactionDate = model.TransactionDate;

            return Json(consultation, JsonRequestBehavior.AllowGet);

        }


      
        public async Task<JsonResult> EditConsultation(ConsultationModel consultation)
        {
            var res = await db.Consultations.FindAsync(consultation.Id);

            res.TransactionDate = consultation.TransactionDate;


            await db.SaveChangesAsync();

            return Json(res.TransactionDate, JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> DeleteConsultation(Guid id)
        {
            var res = await db.Consultations.FindAsync(id);


            db.Consultations.Remove(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> AddDiagnosis(ConsultationDiagnosisModel diagnosis)
        {
            ConsultationsDiagnosi model = new ConsultationsDiagnosi()
            {
                ConsultationId = diagnosis.ConsultationId,
                Remarks = diagnosis.Remarks,
                Diagnosis = diagnosis.Diagnosis


            };

            db.ConsultationsDiagnosis.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EditDiagnosis(ConsultationDiagnosisModel diagnosis)
        {

            var res = await db.ConsultationsDiagnosis.FindAsync(diagnosis.Id);

            res.Remarks = diagnosis.Remarks;
            res.Diagnosis = diagnosis.Diagnosis;


            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteDiagnosis(Guid id)
        {

            var res = await db.ConsultationsDiagnosis.FindAsync(id);

            db.ConsultationsDiagnosis.Remove(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        
        public async Task<JsonResult> AddRecord(PatientRecordModel record)
        {
            PatientsRecord model = new PatientsRecord()
            {
                ConsultationId = record.ConsultationId,
                RecordType = record.RecordTypeName,
                RecordValue = record.RecordValue

            };

            db.PatientsRecords.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EditRecord(PatientRecordModel record)
        {

            var res = await db.PatientsRecords.FindAsync(record.Id);

            res.RecordType = record.RecordTypeName;
            res.RecordValue = record.RecordValue;


            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteRecord(Guid id)
        {

            var res = await db.PatientsRecords.FindAsync(id);

            db.PatientsRecords.Remove(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddPrescribeMed(PatientPrescribeMedicationModel medication)
        {
            PrescribedMedication model = new PrescribedMedication()
            {
                ConsultationId = medication.ConsultationId,
                Medication = medication.Medication,
                Quantity = medication.Quantity,
                Remarks = medication.Remarks,
                Amount = medication.Amount,
                Route = medication.Route,
                Frequency = medication.Frequency,
                Strength = medication.Strength,
                Volume = medication.Volume
                
                
            };

            db.PrescribedMedications.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EditPrescribeMed(PatientPrescribeMedicationModel medication)
        {

            var res = await db.PrescribedMedications.FindAsync(medication.Id);

            res.Medication = medication.Medication;
            res.Quantity = medication.Quantity;
            res.Remarks = medication.Remarks;
            res.Amount = medication.Amount;
            res.Route = medication.Route;
            res.Frequency = medication.Frequency;
            res.Strength = medication.Strength;
            res.Volume = medication.Volume;

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeletePrescribeMed(Guid id)
        {

            var res = await db.PrescribedMedications.FindAsync(id);

            db.PrescribedMedications.Remove(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddService(PatientOtherServiceModel service)
        {
            ConsultationsOtherService model = new ConsultationsOtherService()
            {
                ConsultationId = service.ConsultationId,
                Description = service.Description,
                Remarks = service.Remarks,
                Amount = service.Amount


            };

            db.ConsultationsOtherServices.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EditService(PatientOtherServiceModel service)
        {

            var res = await db.ConsultationsOtherServices.FindAsync(service.Id);

            res.Description = service.Description;
            res.Remarks = service.Remarks;
            res.Amount = service.Amount;



            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteService(Guid id)
        {

            var res = await db.ConsultationsOtherServices.FindAsync(id);

            db.ConsultationsOtherServices.Remove(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddLab(List<PatientLabModel> labs)
        {
            foreach (var x in labs)
            {

                LabResult model = new LabResult()
                {
                    ConsultationId = x.ConsultationId,
                    Remarks = x.Remarks,
                    RecordType = x.RecordTypeName,
                    RecordValue = x.RecordValue,
                    RecordCategoryName = x.RecordCategoryName


                };

                db.LabResults.Add(model);
            }
            
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EditLab(List<PatientLabModel> labs)
        {
            var consultationId = labs.First().ConsultationId;

            

            foreach (var x in labs)
            {
                var res = await db.LabResults.FindAsync(x.Id);
                
                res.RecordValue = x.RecordValue;
                res.RecordType = x.RecordTypeName;
                res.Remarks = x.Remarks;


            }
            
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteLab(Guid ConsultationId, string CategoryName)
        {

            var res = await db.LabResults.Where(i=>i.RecordCategoryName == CategoryName && i.ConsultationId == ConsultationId).ToListAsync();

            db.LabResults.RemoveRange(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> GetLab(Guid ConsultationId, string RecordCategory)
        {

            var labs = await db.LabResults.Where(i => i.ConsultationId == ConsultationId && i.RecordCategoryName == RecordCategory).ToListAsync();
            List<PatientLabModel> labList = new List<PatientLabModel>();

            foreach (var lab in labs)
            {
                PatientLabModel serviceModel = new PatientLabModel()
                {
                    Id = lab.Id,
                    RecordTypeName = lab.RecordType,
                    RecordValue = lab.RecordValue,
                    Remarks = lab.Remarks,
                    ConsultationId = ConsultationId,
                    RecordCategoryName = lab.RecordCategoryName
                    
                };

                labList.Add(serviceModel);
            }


            return Json(labList, JsonRequestBehavior.AllowGet);
        }


    }
}