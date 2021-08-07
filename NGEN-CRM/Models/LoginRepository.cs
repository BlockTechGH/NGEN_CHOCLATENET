using QvertzDBLink;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class LoginRepository
    {
        public static Login isValidUser(Login obj) //check user is valid
        {
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("LoginID", obj.Usmname, DbType.String));
            ParamList.Add(new DbParameterList("Pswd", obj.UsmPassword, DbType.String));
            DataTable tblItems = DbController.ExecuteDataTable("User_Validate_Sp", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj.StatusMesg = item["StatusMsg"].ToString();
                if (obj.StatusMesg == "Login Sucessfully")
                {
                    obj.isValid = true;
                    List<DbParameterList> ParamList1 = new List<DbParameterList>();
                    ParamList1.Add(new DbParameterList("username", obj.Usmname, DbType.String));
                    ParamList1.Add(new DbParameterList("password", obj.UsmPassword, DbType.String));
                    DataTable tblItem = DbController.ExecuteDataTable("Login_Sp", ParamList1);
                    foreach (DataRow items in tblItem.Rows)
                    {
                        obj.Usmname = items["User_LoginID"].ToString();
                        obj.UsmID = Convert.ToInt32(items["ID"]);
                        obj.RomID = Convert.ToInt32(items["User_RoleID"]);
                        //obj.BrmCode =Convert.ToInt32( item["User_UnitID"].ToString());
                    }

                }
                else
                    obj.isValid = false;
            }
            return obj;
        }
    }
}