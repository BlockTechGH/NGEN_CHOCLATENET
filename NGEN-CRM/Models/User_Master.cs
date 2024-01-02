using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class User_Master
    {
        public decimal ID { get; set; }

        public string User_LoginID { get; set; }

        public string User_Name { get; set; }

        public string User_Email { get; set; }

        public decimal? User_RoleID { get; set; }

        public string User_Pswd { get; set; }
    }
}