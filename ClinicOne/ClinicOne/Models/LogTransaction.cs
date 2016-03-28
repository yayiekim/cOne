using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ClinicOne.Models
{
    public class LogTransaction
    {
        ClinicOneEntities db = new ClinicOneEntities();

        public async Task<bool> LogJob(SecurityLog securityLog)
        {
            db.SecurityLogs.Add(securityLog);
            await db.SaveChangesAsync();

            return true;
        }


    }
}