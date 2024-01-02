using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class Connection
    {
        //public string ConnectionString { get; } = @"Server=IT-DESKTOP\SQLEXPRESS;Database=3CX Competitive Wallboard;User Id=user;Password=password;MultipleActiveResultSets=true";//wecare
        //public string ConnectionString { get; } = @"Server=LAPTOP-EBN4GVUL\SQLEXPRESS;Database=3CX Competitive Wallboard;Trusted_Connection=True;MultipleActiveResultSets=true;timeout=3000";//dev

        #if DEBUG
                public string ConnectionString { get; } = @"Server=LAPTOP-EBN4GVUL\SQLEXPRESS;Database=3CX Competitive Wallboard;Trusted_Connection=True;MultipleActiveResultSets=true;timeout=3000"; //local

        #else
                public string ConnectionString { get; } = @"Server=IT-DESKTOP\SQLEXPRESS;Database=3CX Competitive Wallboard;User Id=user;Password=password;MultipleActiveResultSets=true"; //wecare
        #endif

    }
}