using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClinicOne.Models;
using System.Data.Entity;

namespace ClinicOne.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        ClinicOneEntities db = new ClinicOneEntities();

        public async Task<JsonResult> getInputCommon(string Id)
        {
            List<CommonModel> thelist = new List<CommonModel>();


            var res = await db.DropDownCommons.Where(i => i.InputFieldId == Id).ToListAsync();

            foreach (var x in res)
            {
                CommonModel model = new CommonModel()
                {
                    InputFieldId = x.InputFieldId,
                    InputValue = x.Value

                };

                thelist.Add(model);
            }

            return Json(thelist, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> updateCommon(List<CommonModel> cmn)
        {
            foreach (var x in cmn)
            {
                var common = await db.DropDownCommons.Where(i => i.InputFieldId == x.InputFieldId && i.Value == x.InputValue).ToListAsync();

                if (!common.Any())
                {
                    DropDownCommon model = new DropDownCommon()
                    {
                        InputFieldId = x.InputFieldId,
                        Value = x.InputValue
                    };

                    db.DropDownCommons.Add(model);
                }
            }
            
            await db.SaveChangesAsync();

            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}