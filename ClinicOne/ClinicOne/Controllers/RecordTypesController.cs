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
    public class RecordTypesController : Controller
    {
        ClinicOneEntities db = new ClinicOneEntities();

        // GET: RecordCATEGORIES
        public ActionResult RecordTypeCategories()
        {
            return View();
        }

        public ActionResult RecordTypes()
        {
            return View();
        }


        public async Task<JsonResult> geRecordCategories()
        {

            List<RecordCategoryModel> thelist = new List<RecordCategoryModel>();

            var res = await db.RecordTypesCategories.ToListAsync();

            foreach (var x in res)
            {
                RecordCategoryModel  model = new RecordCategoryModel()
                {
                    Id = x.Id,
                    Category = x.Name

                };

                thelist.Add(model);

            }
            
            return Json(thelist, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> addRecordCategory(RecordCategoryModel category)
        {
            RecordTypesCategory model = new RecordTypesCategory()
            {
                Name = category.Category,
                AspNetUserId = User.Identity.GetUserId()
                
            };

            db.RecordTypesCategories.Add(model);
            await db.SaveChangesAsync();


            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> editRecordCategory(RecordCategoryModel category)
        {

            var res = await db.RecordTypesCategories.FindAsync(category.Id);
            res.Name = category.Category;

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> delete(Guid id)
        {
            var res = await db.RecordTypesCategories.FindAsync(id);

            db.RecordTypesCategories.Remove(res);

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> geRecordTypes()
        {

            var UserId =  User.Identity.GetUserId();

            List<RecordTypeModel> thelist = new List<RecordTypeModel>();

            var res = await db.RecordTypes.Where(i=>i.RecordTypesCategory.AspNetUserId == UserId ).ToListAsync();

            foreach (var x in res)
            {
                RecordTypeModel model = new RecordTypeModel()
                {
                    Id = x.Id,
                    RecordTypeName = x.Name

                };

                thelist.Add(model);


            }

            return Json(thelist, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> addRecordType(RecordTypeModel recordType)
        {
            RecordType model = new RecordType()
            {
                Name = recordType.RecordTypeName,
                RecordTypesCategoryId = recordType.RecordTypeCategoryId

            };

            db.RecordTypes.Add(model);
            await db.SaveChangesAsync();


            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> editRecordType(RecordTypeModel recordType)
        {

            var res = await db.RecordTypes.FindAsync(recordType.Id);
            res.Name = recordType.RecordTypeName;

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> deleteRecordType(Guid id)
        {
            var res = await db.RecordTypes.FindAsync(id);

            db.RecordTypes.Remove(res);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }



    }

}
