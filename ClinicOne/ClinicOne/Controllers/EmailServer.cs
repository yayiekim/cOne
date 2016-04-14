using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ClinicOne.Controllers
{
    public class EmailServer
    {

        public async Task<string> SendEmail(MailMessage mail)
        {

            SmtpClient smtpServer = new SmtpClient("smtp.live.com");
            smtpServer.Credentials = new System.Net.NetworkCredential("kimberly_abacco@live.com", "k91y27");
            smtpServer.Port = 587; // Gmail works on this port
            smtpServer.EnableSsl = true;

            mail.From = new MailAddress("kimberly_abacco@live.com");
       

           await smtpServer.SendMailAsync(mail);

            return "ok";

        }


    }
}