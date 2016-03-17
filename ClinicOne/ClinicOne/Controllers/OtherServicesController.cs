using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClinicOne.Controllers
{
    public class OtherServicesController : Controller
    {
        ClinicOneEntities db = new ClinicOneEntities();
        // GET: OtherServices
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServicesCategories()
        {
            return View();
        }
        public async Task<JsonResult> getCategoriesList()
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> add()
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> edit()
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> delete()
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }



    }


}
}