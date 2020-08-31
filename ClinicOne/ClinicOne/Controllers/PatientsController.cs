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

        /// <summary>  
        /// For calculating age  
        /// </summary>  
        /// <param name="Dob">Enter Date of Birth to Calculate the age</param>  
        /// <returns> years, months,days, hours...</returns>  
        static string CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;

            if (Now < Dob) {
                return "Invalid birthday";
            }


            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;

            if (Years < 1 && Months > 0)
            {
                return Months.ToString() + " Month(s) Old";
            }
            else if (Years < 1 && Months < 1)
            {
                return Days.ToString() + " Day(s) Old";

            }
            else
            {
                return Years.ToString();
            }


        }

        public async Task<JsonResult> getPatients(string key)
        {
            List<PatientModel> thelist = new List<PatientModel>();


            var res = await db.Patients.Where(i => i.FirstName.Contains(key) || i.LastName.Contains(key)).ToListAsync();


            foreach (var x in res)
            {
                DateTime dob = x.BirthDate;
                PatientModel model = new PatientModel()
                {
                    Id = x.Id,
                    Age = CalculateYourAge(x.BirthDate),
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
            List<PatientModel> thelist = new List<PatientModel>();

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
                                 Address2 = r.Address2,


                             }).ToListAsync();

            foreach (var x in res)
            {

                DateTime dob = x.BirthDate;
               

                PatientModel model = new PatientModel()
                {
                    Id = x.Id,
                    Age = CalculateYourAge(x.BirthDate),
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


        public async Task<JsonResult> getWaitingPatients()
        {
            List<PatientModel> thelist = new List<PatientModel>();
            var res = await (from w in db.Waitings
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
                                 Address2 = r.Address2,
                                 Remarks = w.Remarks,
                                 WaitingListId = w.Id.ToString(),



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

                PatientModel model = new PatientModel()
                {
                    Id = x.Id,
                    Age = CalculateYourAge(x.BirthDate),
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
                    Address2 = x.Address2,
                    Remarks = x.Remarks,
                    WaitingListId = x.WaitingListId
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


            patient.Age = CalculateYourAge(model.BirthDate);
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