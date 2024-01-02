using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class UserRegistration
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string Login_ID { get; set; }
        public long Id { get; set; }
        public string MenuIds { get; set; }
        public List<string> MenuId { get; set; }
        public bool isEdit { get; set; }
        public string InitialUserLoginID { get; set; }
        public List<UserRegistration> UserList { get; set; }
    }
}