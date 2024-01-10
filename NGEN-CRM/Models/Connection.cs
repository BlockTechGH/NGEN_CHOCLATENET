using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class Connection
    {
        //developer
        //public string ConnectionString { get; } = @"Server=LAPTOP-EBN4GVUL\SQLEXPRESS;Database=3CX Competitive Wallboard;Trusted_Connection=True;MultipleActiveResultSets=true;timeout=3000";

        //mais
        //public string ConnectionString { get; } = @"Server=DESKTOP-JA66V0J\SQLEXPRESS;Database=3CX Competitive Wallboard;User Id=sa;Password=password;MultipleActiveResultSets=true";

#if DEBUG
        //developer
        public string ConnectionString { get; } = @"Server=LAPTOP-EBN4GVUL\SQLEXPRESS;Database=3CX Competitive Wallboard;Trusted_Connection=True;MultipleActiveResultSets=true;timeout=3000";

#else
        //mais
        public string ConnectionString { get; } = @"Server=DESKTOP-JA66V0J\SQLEXPRESS;Database=3CX Competitive Wallboard;User Id=sa;Password=password;MultipleActiveResultSets=true";
#endif

    }
}