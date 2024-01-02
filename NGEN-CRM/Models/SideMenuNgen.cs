using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class SideMenuNgen
    {
        public int MenuID { get; set; }

        public string MenuName { get; set; }

        public string MenuType { get; set; }

        public string MenuUrl { get; set; }

        public string MenuIcon { get; set; }
    }
}