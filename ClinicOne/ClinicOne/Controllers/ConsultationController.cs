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
                var diagnosis = await db.ConsultationsDiagnosis.Where(i => i.ConsultationId == consultaion.Id).ToListAsync();
               
                foreach (var diagnost in diagnosis)
                {

                    ConsultationModel model = new ConsultationModel()
                    {
                        AspNetUserId = User.Identity.GetUserId(),
                        PatientId = consultaion.PatientId,
                        TransactonDate = consultaion.TransactionDate,
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
                    ConsultationId = consultation.Id
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


            var labs = await db.LabResults.Where(i => i.ConsultationId == consultation.Id).ToListAsync();
            List<PatientLabModel> labList = new List<PatientLabModel>();
            foreach (var lab in labs)
            {
                PatientLabModel serviceModel = new PatientLabModel()
                {
                    Id = lab.Id,
                    RecordTypeName = lab.RecordType,
                    RecordValue = lab.RecordValue,
                    Remarks = lab.Remarks,
                    ConsultationId = consultation.Id

                };

                labList.Add(serviceModel);
            }

            
            ConsultationModel model = new ConsultationModel()
            {
                AspNetUserId = User.Identity.GetUserId(),
                PatientId = consultation.PatientId,
                TransactonDate = consultation.TransactionDate,
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


        public async Task<JsonResult> NewConsultation(Guid patientId)
        {

            Consultation model = new Consultation()
            {
                AspNetUserId = User.Identity.GetUserId(),
                TransactionDate = DateTime.Now,
                PatientId = patientId

            };

            db.Consultations.Add(model);    
            await db.SaveChangesAsync();
                     
            ConsultationModel resultModel = new ConsultationModel()
            {
                AspNetUserId = model.AspNetUserId,
                PatientId = model.PatientId,
                TransactonDate = model.TransactionDate,
                Id = model.Id,
                PatientfullName = model.Patient.FirstName + " " + model.Patient.MiddleName + " " + model.Patient.LastName,

            };

            return Json(resultModel, JsonRequestBehavior.AllowGet);

        }


        public async Task<JsonResult> AddConsultation(ConsultationModel consultaion)
        {

            Consultation model = new Consultation()
            {
                AspNetUserId = User.Identity.GetUserId(),
                TransactionDate = DateTime.Now,
                PatientId = consultaion.PatientId
                
            };

            db.Consultations.Add(model);

            if (consultaion.DiagnosisList != null)
            {

                foreach (var x in consultaion.DiagnosisList)
                {
                    ConsultationsDiagnosi diagnosisModel = new ConsultationsDiagnosi()
                    {
                        ConsultationId = model.Id,
                        Amount = x.Amount,
                        Diagnosis = x.Diagnosis,
                        Remarks = x.Remarks,

                    };

                    db.ConsultationsDiagnosis.Add(diagnosisModel);

                }


            }

            if (consultaion.PrescribeMedicationList != null)
            {

                foreach (var x in consultaion.PrescribeMedicationList)
                {
                    PrescribedMedication medsModel = new PrescribedMedication()
                    {
                        ConsultationId = model.Id,
                        Amount = x.Amount,
                        Medication = x.Medication,
                        Remarks = x.Remarks,
                        Quantity = x.Quantity,
                        Frequency = x.Frequency,
                        Route = x.Route,
                        Strength = x.Strength,
                        Volume = x.Volume

                    };

                    db.PrescribedMedications.Add(medsModel);

                }


            }

            if (consultaion.RecordList != null)
            {

                foreach (var x in consultaion.RecordList)
                {
                    PatientsRecord recordsModel = new PatientsRecord()
                    {
                        ConsultationId = model.Id,
                        RecordType = x.RecordTypeName,
                        RecordValue = x.RecordValue

                    };

                    db.PatientsRecords.Add(recordsModel);

                }


            }

            var res = await db.Waitings.Where(i => i.IsAdmitted == true).SingleAsync();
            db.Waitings.Remove(res);

            await db.SaveChangesAsync();

           
            return Json(model.Id, JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> EditConsultation(ConsultationModel consultaion)
        {
            var res = await db.Consultations.FindAsync(consultaion.Id);

            res.TransactionDate = consultaion.TransactonDate;


            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);

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

        
        public async Task<JsonResult> AddRecord(PatientsRecord record)
        {
            PatientsRecord model = new PatientsRecord()
            {
                ConsultationId = record.ConsultationId,
                RecordType = record.RecordType,
                RecordValue = record.RecordValue

            };

            db.PatientsRecords.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EditRecord(RecordTypeModel record)
        {

            var res = await db.PatientsRecords.FindAsync(record.Id);

            res.RecordType = record.RecordTypeName;
            res.RecordValue = record.RecordTypeName;


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
                Amount = medication.Amount

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

        public async Task<JsonResult> AddLab(PatientLabModel lab)
        {
            LabResult model = new LabResult()
            {
                ConsultationId = lab.ConsultationId,
                Remarks = lab.Remarks,
                RecordType = lab.RecordTypeName,
                RecordValue = lab.RecordValue


            };

            db.LabResults.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EditLab(PatientLabModel lab)
        {

            var res = await db.LabResults.FindAsync(lab.Id);

            res.RecordValue = lab.RecordValue;
            res.RecordType = lab.RecordTypeName;
            res.Remarks = lab.Remarks;



            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteLab(Guid id)
        {

            var res = await db.LabResults.FindAsync(id);

            db.LabResults.Remove(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }


    }
}