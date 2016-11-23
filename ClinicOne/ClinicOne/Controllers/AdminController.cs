using ClinicOne.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ClinicOne.Controllers
{
    public class AdminController : Controller
    {
        ClinicOneEntities db = new ClinicOne.ClinicOneEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserRoles()
        {
            return View();
        }

        public JsonResult getUsers()
        {

            List<UserModel> theList = new List<Models.UserModel>();

            foreach (var x in db.AspNetUsers)
            {
                UserModel model = new Models.UserModel()
                {
                    Id = x.Id,
                    Username =x.UserName

                };

                theList.Add(model);
            }



            return Json(theList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getRoles()
        {


            return Json(db.AspNetRoles, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getUsersInRoles(string userName)
        {
            var roles = Roles.GetRolesForUser(userName);


            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult deleteUsersInRoles(string Username, string Roles)
        //{
        //    var UserManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(context));

        //    UserManager.RemoveFromRole(Username, Roles);


        //    return Json("Ok", JsonRequestBehavior.AllowGet);
        //}


        //public ActionResult UserRoles()
        //{

        //    return View();

        //}

        //public JsonResult AddUserInRoles(string Username, string Roles)
        //{
        //    Security s = new Security();
        //      s.AddUserToRole(Username,Roles);


        //    return Json("Ok", JsonRequestBehavior.AllowGet);
        //}

    }
}