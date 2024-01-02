using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class UserMenuAccessNgen
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public string MenuIDs { get; set; }
    }
}