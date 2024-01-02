using DocumentFormat.OpenXml.Wordprocessing;
using QvertzDBLink;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;

namespace NGEN_CRM.Models
{
    public class CLSCommen
    {
        private Connection sqlConnectionString = new Connection();

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

        public MenuAndUname GetUserMenu(int currentUserId)
        {
            using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                MenuAndUname obj = new MenuAndUname();
                conn.Open();
                var menuIds = conn.Query<string>($"select MenuIDs from UserMenuAccessNgen where UserID={currentUserId}").FirstOrDefault();
                obj.sideMenuNgen = conn.Query<SideMenuNgen>($"select * from SideMenuNgen where MenuID in ({menuIds})").ToList();
                var UserRoleID = conn.Query<int>($"select User_RoleID from User_Master where ID={currentUserId}").FirstOrDefault();
                obj.UserRole = conn.Query<string>($"select Role_Name from Role_Master where ID={UserRoleID}").FirstOrDefault();
                conn.Close();
                return obj;
            }
        }
       
    }
}