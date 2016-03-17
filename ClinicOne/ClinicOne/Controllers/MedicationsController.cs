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
    public class MedicationsController : Controller
    {
        ClinicOneEntities db = new ClinicOneEntities();


        // GET: Medications
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MedicationCategories()
        {
            return View();

        }

        public async Task<JsonResult> getMedicationCategories()
        {
            List<MedicationCategoryModel> thelist = new List<MedicationCategoryModel>();

            var userId = User.Identity.GetUserId();

            var res = await db.DrugsCategories.Where(i=>i.AspNetUserId == userId ).ToListAsync();

            foreach (var x in res)
            {
                MedicationCategoryModel model = new MedicationCategoryModel()
                {
                    Id = x.Id,
                    CategoryName = x.Name
                    
                };

                thelist.Add(model);
            }

            return Json(thelist, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> addMedicationCategory(MedicationCategoryModel category)
        {
            DrugsCategory model = new DrugsCategory()
            {
                Name = category.CategoryName,
                AspNetUserId = User.Identity.GetUserId()
            };

            db.DrugsCategories.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> editMedicationCategory(MedicationCategoryModel category)
        {
            var res = await db.DrugsCategories.FindAsync(category.Id);

            res.Name = category.CategoryName;

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> deleteMedicationCategory(Guid id)
        {
            var res = await db.DrugsCategories.FindAsync(id);

            db.DrugsCategories.Remove(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }


        //Drugs
        public async Task<JsonResult> getMedications()
        {
            List<MedicationModel> thelist = new List<MedicationModel>();

            var res = await db.Drugs.ToListAsync();

            foreach (var x in res)
            {

                MedicationModel model = new MedicationModel() {

                    Id = x.Id,
                    MedicationName = x.MedicineName,
                    Description = x.Description,
                    Dosage = x.Dosage,
                    Code = x.Code,
                    Amount = x.Amount.GetValueOrDefault(0),
                    CategoryId = x.DrugCatergoryId

                };

                thelist.Add(model);
            }
            
            return Json(thelist, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> addMedication(MedicationModel medication)
        {
            Drug model = new Drug()
            {
                MedicineName = medication.MedicationName,
                Amount = medication.Amount,
                Code = medication.Code,
                Description = medication.Description,
                Dosage = medication.Dosage,
                DrugCatergoryId = medication.CategoryId
                
            };

            db.Drugs.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> editMedication(MedicationModel medication)
        {
            var res = await db.Drugs.FindAsync();

            res.MedicineName = medication.MedicationName;
            res.Amount = medication.Amount;
            res.Code = medication.Code;
            res.Description = medication.Description;
            res.Dosage = medication.Dosage;
            res.DrugCatergoryId = medication.CategoryId;

            await db.SaveChangesAsync();
            
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> deleteMedication(Guid id)
        {
            var res = await db.Drugs.FindAsync(id);

            db.Drugs.Remove(res);
            await db.SaveChangesAsync();


            return Json("ok", JsonRequestBehavior.AllowGet);
        }



    }


}



