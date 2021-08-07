using QvertzDBLink;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class CLSCommen
    {
       

        public static bool isCodeExist(string Code) // Code Exist Check
        {
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("login_Id ", Code, DbType.String));
            DataTable tblItems = DbController.ExecuteDataTable("SP_CheckLogin_IDExistance", ParamList);
            if (tblItems != null && tblItems.Rows.Count > 0)
            {
                return true; //code is not available
            }
            return false; //code is available
        }

       
    }
}