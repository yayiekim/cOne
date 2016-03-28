using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ClinicOne.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> getPatientRecord(Guid PatientId)
        {

            return Json("", JsonRequestBehavior.AllowGet);

        }

      
        public async Task<JsonResult> getIncome(DateTime FromDate, DateTime ToDate)
        {
            //from medication, consultation and other service

            return Json("", JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> getInsurancePaidTransaction(DateTime FromDate, DateTime ToDate)
        {

            return Json("", JsonRequestBehavior.AllowGet);

        }


    }
}