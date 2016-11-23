using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicOne.Models
{
    public class AdminModels
    {
    }

    public class UserModel {

        public string Id { get; set; }
        public string Username { get; set; }

    }



    public class UserRolesModel
    {

        public string UserId { get; set; }
        public string RoleId { get; set; }

    }


}