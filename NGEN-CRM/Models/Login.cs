using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class Login
    {
        public long UsmID { get; set; }
        public string UsmCode { get; set; }
        public string UsmPassword { get; set; }
        public string Usmname { get; set; }
        public string UsmFirstName { get; set; }
        public string StatusMesg { get; set; }
        public long RomID { get; set; }
        public long CpmIDPtr { get; set; }
        public long CustomerID { get; set; }
        public bool isValid { get; set; }
        public Int64 UnitId { get; set; }
    }
}