using ClinicOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace ClinicOne.Controllers
{
    public class PatientsController : AsyncController
    {
        ClinicOneEntities db = new ClinicOneEntities();
       
        // GET: Patients
        public ActionResult Index()
        {
            return View();
        }


        public async Task<JsonResult> getPatients(string key)
        {
            List<PatientModel> thelist = new List<PatientModel>();


            var res = await db.Patients.Where(i=>i.FirstName.Contains(key) || i.LastName.Contains(key)).ToListAsync();


            foreach (var x in res)
            {

                DateTime dob = x.BirthDate;
                DateTime PresentYear = DateTime.Now;
                TimeSpan ts = PresentYear - dob;


                int Age;

                try
                {

                    Age = DateTime.MinValue.AddDays(ts.Days).Year - 1;
                }
                catch {
                    Age = 0;
                }

                PatientModel model = new PatientModel()
                {
                    Id = x.Id,
                    Age = Age,
                    BirthDate = dob,
                    BloodType = x.BloodType,
                    ContactNumber1 = x.ContactNumber1,
                    ContactNumber2 = x.ContactNumber2,
                    FullName = x.FirstName + " " + x.MiddleName + " " + x.LastName,
                    Gender = x.Gender,
                    FullAddress = x.Address1 + ", " + x.Address2,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Address1 = x.Address1,
                    Address2 = x.Address2
                    
                };

                thelist.Add(model);

            }


            return Json(thelist, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> getPatientByDate(DateTime date)
        {
            var res = await (from w in db.Consultations.Where(i => i.TransactionDate == date)
                             join o in db.Patients on w.PatientId equals o.Id into ow
                             from r in ow
                             select new PatientModel
                             {

                                 Id = r.Id,
                                 BloodType = r.BloodType,
                                 BirthDate = r.BirthDate,
                                 ContactNumber1 = r.ContactNumber1,
                                 ContactNumber2 = r.ContactNumber2,
                                 FullName = r.FirstName + " " + r.MiddleName + " " + r.LastName,
                                 Gender = r.Gender,
                                 FullAddress = r.Address1 + ", " + r.Address2,
                                 FirstName = r.FirstName,
                                 MiddleName = r.MiddleName,
                                 LastName = r.LastName,
                                 Address1 = r.Address1,
                                 Address2 = r.Address2
                                

                             }).ToListAsync();

            foreach (var x in res)
            {

                DateTime dob = x.BirthDate;
                DateTime PresentYear = DateTime.Now;
                TimeSpan ts = PresentYear - dob;


                int Age;

                try
                {

                    Age = DateTime.MinValue.AddDays(ts.Days).Year - 1;
                }
                catch
                {
                    Age = 0;
                }

                x.Age = Age;
                x.BirthDate = dob;

            }


            return Json(res, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> getWaitingPatients()
        {
            
            var res = await (from w in db.Waitings
                             join o in db.Patients on w.PatientId equals o.Id into ow
                             from r in ow
                             select new PatientModel {

                                 Id = r.Id,                                 
                                 BloodType = r.BloodType,
                                 BirthDate = r.BirthDate,
                                 ContactNumber1 = r.ContactNumber1,
                                 ContactNumber2 = r.ContactNumber2,
                                 FullName = r.FirstName + " " + r.MiddleName + " " + r.LastName,
                                 Gender = r.Gender,
                                 FullAddress = r.Address1 + ", " + r.Address2,
                                 FirstName = r.FirstName,
                                 MiddleName = r.MiddleName,
                                 LastName = r.LastName,
                                 Address1 = r.Address1,
                                 Address2 = r.Address2,
                                 Remarks = w.Remarks,
                                 WaitingListId = w.Id.ToString()


                             }).ToListAsync();


            foreach (var x in res)
            {

                DateTime dob = x.BirthDate;
                DateTime PresentYear = DateTime.Now;
                TimeSpan ts = PresentYear - dob;


                int Age;

                try
                {

                    Age = DateTime.MinValue.AddDays(ts.Days).Year - 1;
                }
                catch
                {
                    Age = 0;
                }

                x.Age = Age;
                x.BirthDate = dob;

            }


            return Json(res, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> addPatient(PatientModel patient)
        {

            var userId = User.Identity.GetUserId();



            Patient model = new Patient()
            {

                Address1 = patient.Address1,
                Address2 = patient.Address2,
                BirthDate = patient.BirthDate,
                BloodType = patient.BloodType,
                ContactNumber1 = patient.ContactNumber1,
                ContactNumber2 = patient.ContactNumber2,
                FirstName = patient.FirstName,
                MiddleName = patient.MiddleName,
                LastName = patient.LastName,
                Gender = patient.Gender,
                OriginAspNetUserId = userId

            };

            db.Patients.Add(model);
            await db.SaveChangesAsync();

            DateTime dob = model.BirthDate;
            DateTime PresentYear = DateTime.Now;
            TimeSpan ts = PresentYear - dob;
            DateTime Age = DateTime.MinValue.AddDays(ts.Days);

            patient.Age = Age.Year - 1;
            patient.Id = model.Id;
            
            return Json(patient, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> editPatient(PatientModel patient)
        {
            var res = await db.Patients.FindAsync(patient.Id);

            res.Address1 = patient.Address1;
            res.Address2 = patient.Address2;
            res.BirthDate = patient.BirthDate;
            res.BloodType = patient.BloodType;
            res.ContactNumber1 = patient.ContactNumber1;
            res.ContactNumber2 = patient.ContactNumber2;
            res.FirstName = patient.FirstName;
            res.MiddleName = patient.MiddleName;
            res.LastName = patient.LastName;
            res.Gender = patient.Gender;

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> delete(Guid id)
        {
            var res = await db.Patients.FindAsync(id);

            db.Patients.Remove(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        
    }
}