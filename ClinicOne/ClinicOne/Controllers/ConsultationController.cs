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
    public class ConsultationController : Controller
    {
        ClinicOneEntities db = new ClinicOneEntities();

        // GET: Consultation
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> getAdmited()
        {
            var res = await db.Waitings.Where(i => i.IsAdmitted == true).SingleAsync();

            return Json("", JsonRequestBehavior.AllowGet);

        }


    }
}