using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ClinicOne.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public async Task<ActionResult> Index()
        {
            EmailServer email = new EmailServer();

            MailMessage mail = new MailMessage();
            mail.Subject = "test";
            mail.To.Add("silence.kim@live.com");
            mail.Body = "Hello Email";

           await email.SendEmail(mail);


            return View();

        }

      

    }
}