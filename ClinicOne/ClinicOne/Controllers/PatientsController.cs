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


        public async Task<JsonResult> getPatients()
        {
            List<PatientModel> thelist = new List<PatientModel>();

            var userId = User.Identity.GetUserId();

            var res = await db.Patients.Where(i => i.OriginAspNetUserId == userId).ToListAsync();


            foreach (var x in res)
            {

                DateTime dob = x.BirthDate;
                DateTime PresentYear = DateTime.Now;
                TimeSpan ts = PresentYear - dob;
                DateTime Age = DateTime.MinValue.AddDays(ts.Days);

                PatientModel model = new PatientModel()
                {
                    Id = x.Id,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    Age = Age.Year - 1,
                    BirthDate = x.BirthDate,
                    BloodType = x.BloodType,
                    ContactNumber1 = x.ContactNumber1,
                    ContactNumber2 = x.ContactNumber2,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Gender = x.Gender
                    
                    

                };

                thelist.Add(model);

            }


            return Json(thelist, JsonRequestBehavior.AllowGet);
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