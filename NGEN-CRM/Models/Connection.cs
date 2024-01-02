using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class Connection
    {
        //wecare
        //public string ConnectionString { get; } = @"Server=IT-DESKTOP\SQLEXPRESS;Database=3CX Competitive Wallboard;User Id=user;Password=password;MultipleActiveResultSets=true";

        //developer
        //public string ConnectionString { get; } = @"Server=LAPTOP-EBN4GVUL\SQLEXPRESS;Database=3CX Competitive Wallboard;Trusted_Connection=True;MultipleActiveResultSets=true;timeout=3000";

        //chocolateness
        //public string ConnectionString { get; } = @"Server=DC1\SQLEXPRESS;Database=3CX Competitive Wallboard;User Id=btadmin;Password=btadmin;MultipleActiveResultSets=true";

#if DEBUG
        //developer
        public string ConnectionString { get; } = @"Server=LAPTOP-EBN4GVUL\SQLEXPRESS;Database=3CX Competitive Wallboard;Trusted_Connection=True;MultipleActiveResultSets=true;timeout=3000";

#else
        //wecare
        public string ConnectionString { get; } = @"Server=IT-DESKTOP\SQLEXPRESS;Database=3CX Competitive Wallboard;User Id=user;Password=password;MultipleActiveResultSets=true";

        //chocolateness
        //public string ConnectionString { get; } = @"Server=IT-DESKTOP\SQLEXPRESS;Database=3CX Competitive Wallboard;User Id=user;Password=password;MultipleActiveResultSets=true";
#endif

    }
}