﻿using ClinicOne.Models;
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

        public async Task<JsonResult> getValueTypes()
        {

            List<ValueTypeModel> thelist = new List<ValueTypeModel>();

            var res = await db.ValueTypes.ToListAsync();

            foreach (var x in res)
            {
                ValueTypeModel model = new ValueTypeModel()
                {
                    Id = x.Id,
                    ValueTypeName = x.ValueType1

                };

                thelist.Add(model);

            }

            return Json(thelist, JsonRequestBehavior.AllowGet);
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

            var res = await db.RecordTypes.Where(i=>i.RecordTypesCategory.AspNetUserId == UserId).ToListAsync();

            foreach (var x in res)
            {
                RecordTypeModel model = new RecordTypeModel()
                {
                    Id = x.Id,
                    RecordTypeName = x.Name,
                    RecordTypeCategoryName = x.RecordTypesCategory.Name,
                    RecordTypeCategoryId = x.RecordTypesCategoryId,
                    ValueTypeName = x.ValueType.ValueType1,
                    ValueTypeId  = x.ValueTypeId
                    
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
                RecordTypesCategoryId = recordType.RecordTypeCategoryId,
                ValueTypeId = recordType.ValueTypeId

            };

            db.RecordTypes.Add(model);
            await db.SaveChangesAsync();


            var catRes = await db.RecordTypesCategories.FindAsync(model.RecordTypesCategoryId);
            var valRes = await db.ValueTypes.FindAsync(model.ValueTypeId);

            RecordTypeModel resModel = new RecordTypeModel()
            {
                Id = model.Id,
                ValueTypeId = model.ValueTypeId,
                ValueTypeName = valRes.ValueType1,
                RecordTypeName = model.Name,
                RecordTypeCategoryId = model.RecordTypesCategoryId,
                RecordTypeCategoryName = catRes.Name

            };


            return Json(resModel, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> editRecordType(RecordTypeModel recordType)
        {

            var res = await db.RecordTypes.FindAsync(recordType.Id);
            res.Name = recordType.RecordTypeName;
            res.RecordTypesCategoryId = recordType.RecordTypeCategoryId;
            res.ValueTypeId = recordType.ValueTypeId;
            
            await db.SaveChangesAsync();

            recordType.RecordTypeCategoryName = res.RecordTypesCategory.Name;
            recordType.ValueTypeName = res.ValueType .ValueType1;

            return Json(recordType, JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> deleteRecordType(Guid id)
        {
            var res = await db.RecordTypes.FindAsync(id);

            db.RecordTypes.Remove(res);

            await db.SaveChangesAsync();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> getRecordClasses()
        {

         
            List<RecordTypeClassModel> thelist = new List<RecordTypeClassModel>();

            var res = await db.RecordTypesCategoryClasses.ToListAsync();

            foreach (var x in res)
            {
                RecordTypeClassModel model = new RecordTypeClassModel()
                {
                    Id = x.Id,
                   ClassName = x.Name

                };

                thelist.Add(model);


            }

            return Json(thelist, JsonRequestBehavior.AllowGet);
        }


    }

}
