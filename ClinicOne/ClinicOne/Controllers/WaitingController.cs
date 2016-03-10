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
    public class WaitingController : AsyncController
    {

        ClinicOneEntities db = new ClinicOneEntities();

        // GET: Waiting
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> getWaitingPatients()
        {
            List<WaitingPatient> thelist = new List<WaitingPatient>();

            var userId = User.Identity.GetUserId();
            var res = await db.Waitings.Where(i => i.AspNetUserId == userId).ToListAsync();

            foreach (var x in res)
            {
                WaitingPatient model = new WaitingPatient()
                {
                    Id = x.Id,
                    PatientFullName = x.Patient.FirstName + " " + x.Patient.MiddleName + " " + x.Patient.LastName,
                    PatientId = x.PatientId,
                    Schedule = x.Schedule,
                    Remarks = x.Remarks
                    
                };

                thelist.Add(model);

            }

            return Json(thelist, JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> addWaitingPatient(WaitingPatient patient)
        {
            Waiting model = new Waiting()
            {
                Id = patient.Id,
                PatientId = patient.PatientId,
                Schedule = DateTime.Now,
                Remarks = patient.Remarks,
                AspNetUserId = User.Identity.GetUserId()

            };         


            db.Waitings.Add(model);
            await db.SaveChangesAsync();

            var res = await db.Patients.FindAsync(patient.PatientId);

        
                patient.Id = model.Id;
                patient.PatientFullName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                patient.PatientId = model.PatientId;
                patient.Schedule = model.Schedule;
                patient.Remarks = model.Remarks;

          
            
            return Json(patient, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> editWaitingPatient(Waiting patient)
        {
            var res = await db.Waitings.FindAsync(patient.Id);

            res.Remarks = patient.Remarks;
            res.Schedule = patient.Schedule;

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> deleteWaitingPatient(Guid id)
        {
            var res = await db.Waitings.FindAsync(id);

            db.Waitings.Remove(res);
            await db.SaveChangesAsync();


            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> admitPatient(Guid id)
        {
            var res = await db.Waitings.FindAsync(id);

            res.IsAdmitted = true;
                      
            await db.SaveChangesAsync();


            return Json("ok", JsonRequestBehavior.AllowGet);
        }

    }
}