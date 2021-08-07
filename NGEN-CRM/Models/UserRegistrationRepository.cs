using QvertzDBLink;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class UserRegistrationRepository
    {
        public static Int16 SaveUser(UserRegistration obj) //Save User
        {

            List<DbParameterList> ParamList = new List<DbParameterList>();
            //ParamList.Add(new DbParameterList("ID", obj.UsmID, DbType.String));
            ParamList.Add(new DbParameterList("name", obj.Name, DbType.String));
            ParamList.Add(new DbParameterList("login_Id", obj.Login_ID, DbType.String));
            ParamList.Add(new DbParameterList("password", obj.Password, DbType.String));
            ParamList.Add(new DbParameterList("email",obj.Email, DbType.String));
            ParamList.Add(new DbParameterList("role", obj.RoleName, DbType.String));
            DbController.ExecuteNonQuery("InsertUser", ParamList);
            return 1;
        }
        public static List<RoleMaster> getAllRole() //Get All Roles
        {
            List<DbParameterList> ParamList = new List<DbParameterList>();
            List<RoleMaster> RoleList = new List<RoleMaster>();
            RoleMaster obj;
            DataTable tblItems = DbController.ExecuteDataTable("SP_GetAllRole", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new RoleMaster();
                obj.RoleId =(item["ID"]).ToString();
                obj.Rolename = item["Role_Name"].ToString();
                RoleList.Add(obj);
            }
            return RoleList;
        }
    }
}