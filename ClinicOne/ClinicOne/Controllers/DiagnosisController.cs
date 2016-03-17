using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClinicOne.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace ClinicOne.Controllers
{
    public class DiagnosisController : Controller
    {
        ClinicOneEntities db = new ClinicOneEntities();


        public ActionResult Index()
        {

            return View();
        }
        // GET: Diagnosis
        public ActionResult DiagnosisCategories()
        {
            return View();
        }

        public async Task<JsonResult> getDiagnosisCategories()
        {
            List<DiagnostCategoryModel> thelist = new List<DiagnostCategoryModel>();

            var userId = User.Identity.GetUserId();

            var res = await db.DiagnosisCategories.Where(i => i.AspNetUserId == userId).ToListAsync();

            foreach (var x in res)
            {
                DiagnostCategoryModel model = new DiagnostCategoryModel()
                {
                    Id = x.Id,
                    CategoryName = x.Name

                };

                thelist.Add(model);
            }

            return Json(thelist, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> addDiagnosisCategory(DiagnostCategoryModel category)
        {
            DiagnosisCategory model = new DiagnosisCategory()
            {
                Name = category.CategoryName,
                AspNetUserId = User.Identity.GetUserId()
            };

            db.DiagnosisCategories.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> editDiagnosisCategory(DiagnostCategoryModel category)
        {
            var res = await db.DiagnosisCategories.FindAsync(category.Id);

            res.Name = category.CategoryName;

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> deleteDiagnosisCategory(Guid id)
        {
            var res = await db.DiagnosisCategories.FindAsync(id);

            db.DiagnosisCategories.Remove(res);
            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }


        //Drugs
        public async Task<JsonResult> getDiagnosis()
        {
            List<DiagnosisModel> thelist = new List<DiagnosisModel>();

            var res = await db.Drugs.ToListAsync();

            foreach (var x in res)
            {

                DiagnosisModel model = new DiagnosisModel()
                {

                    Id = x.Id,
                    DiagnosisName = x.MedicineName,
                    Description = x.Description,
                    CategoryId = x.DrugCatergoryId

                };

                thelist.Add(model);
            }

            return Json(thelist, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> addDiagnosis(DiagnosisModel diagnosis)
        {
            Diagnosi model = new Diagnosi()
            {
                Diagnosis = diagnosis.DiagnosisName,
                Description = diagnosis.Description,
                DiagnosisCategoryId = diagnosis.CategoryId

            };

            db.Diagnosis.Add(model);
            await db.SaveChangesAsync();

            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> editDiagsosis(DiagnosisModel diagnosis)
        {
            var res = await db.Diagnosis.FindAsync();

            res.Diagnosis = diagnosis.DiagnosisName;
            res.Description = diagnosis.Description;
            res.DiagnosisCategoryId = diagnosis.CategoryId;

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> deleteDiagnosis(Guid id)
        {
            var res = await db.Diagnosis.FindAsync(id);

            db.Diagnosis.Remove(res);
            await db.SaveChangesAsync();


            return Json("ok", JsonRequestBehavior.AllowGet);
        }




    }
}