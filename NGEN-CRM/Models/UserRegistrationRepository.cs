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
        public static long SaveUser(UserRegistration obj) //Save User
        {

            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("flag", "Insert", DbType.String));
            ParamList.Add(new DbParameterList("Id", obj.Id, DbType.Int32));
            ParamList.Add(new DbParameterList("name", obj.Name, DbType.String));
            ParamList.Add(new DbParameterList("login_Id", obj.Login_ID, DbType.String));
            ParamList.Add(new DbParameterList("password", obj.Password, DbType.String));
            ParamList.Add(new DbParameterList("email",obj.Email, DbType.String));
            ParamList.Add(new DbParameterList("role", obj.RoleName, DbType.String));
            ParamList.Add(new DbParameterList("menu_Ids", obj.MenuIds, DbType.String));
            object s = DbController.ExecuteScalar("SP_UserCreation", ParamList);
            long isSuccess = Convert.ToInt64(s);
            return isSuccess;
            
        }
        public static long UpdateUser(UserRegistration obj) //Update User
        {

            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("flag", "Update", DbType.String));
            ParamList.Add(new DbParameterList("Id", obj.Id, DbType.Int32));
            ParamList.Add(new DbParameterList("name", obj.Name, DbType.String));
            ParamList.Add(new DbParameterList("login_Id", obj.Login_ID, DbType.String));
            ParamList.Add(new DbParameterList("password", obj.Password, DbType.String));
            ParamList.Add(new DbParameterList("email", obj.Email, DbType.String));
            ParamList.Add(new DbParameterList("role", obj.RoleName, DbType.String));
            ParamList.Add(new DbParameterList("menu_Ids", obj.MenuIds, DbType.String));
            object s = DbController.ExecuteScalar("SP_UserCreation", ParamList);
            long isSuccess = Convert.ToInt64(s);
            return isSuccess;

        }
        public static long DeleteUser(long Userid) //Delete Role
        {
           
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("id", Userid, DbType.Int32));
            ParamList.Add(new DbParameterList("flag", "Delete", DbType.String));
            object s = DbController.ExecuteScalar("SP_UserCreation", ParamList);
            long isSuccess = Convert.ToInt64(s);
            return isSuccess;
        }
        public static UserRegistration getByID(long ID) //Get Role Details by ID
        {
            List<DbParameterList> ParamList = new List<DbParameterList>();
            UserRegistration obj = new UserRegistration();
            ParamList.Add(new DbParameterList("id", ID, DbType.Int32));
            ParamList.Add(new DbParameterList("flag", "Id", DbType.String));
            //DataTable tblItems = DbController.ExecuteDataTable("SP_UserCreation", ParamList);
            //foreach (DataRow item in tblItems.Rows)
            //{
            //    obj.Id = Convert.ToInt32(item["Id"]);
            //    obj.Name = item["User_Name"].ToString();
            //    obj.Password = item["User_Pswd"].ToString();
            //    obj.Login_ID = item["User_LoginID"].ToString();
            //    obj.InitialUserLoginID= item["User_LoginID"].ToString();
            //    obj.Email = item["User_Email"].ToString();
            //    obj.RoleName = item["User_RoleID"].ToString();
            //    obj.isEdit = true;

            //}

            DataSet ds = DbController.ExecuteDataSet("SP_UserCreation", ParamList);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable tblUser = ds.Tables[0];  // Assuming the first table is for user details

                foreach (DataRow item in tblUser.Rows)
                {
                    obj.Id = Convert.ToInt32(item["Id"]);
                    obj.Name = item["User_Name"].ToString();
                    obj.Password = item["User_Pswd"].ToString();
                    obj.Login_ID = item["User_LoginID"].ToString();
                    obj.InitialUserLoginID = item["User_LoginID"].ToString();
                    obj.Email = item["User_Email"].ToString();
                    obj.RoleName = item["User_RoleID"].ToString();
                    obj.isEdit = true;
                }

                // Now, you can process other tables if needed.
                if (ds.Tables.Count > 1)
                {
                    DataTable tblMenuAccess = ds.Tables[1];  // Assuming the second table is for other details
                    if(tblMenuAccess.Rows.Count != 0)
                    {
                        foreach (DataRow item in tblMenuAccess.Rows)
                        {
                            obj.MenuIds = item["MenuIDs"].ToString();
                            var userid = item["UserID"].ToString();
                            var id = item["ID"].ToString();
                            //obj.isEdit = true;
                        }
                    }
                }
            }
            obj.UserList = getAllUser();
            return obj;
        }
        public static List<UserRegistration> getAllUser() //Get All Roles
        {
            List<DbParameterList> ParamList = new List<DbParameterList>();
            List<UserRegistration> UserList = new List<UserRegistration>();
            UserRegistration obj;
            ParamList.Add(new DbParameterList("flag", "All", DbType.String));
            DataTable tblItems = DbController.ExecuteDataTable("SP_UserCreation", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new UserRegistration();
                obj.Id = Convert.ToInt32(item["id"]);
                obj.Name = item["User_Name"].ToString();
                UserList.Add(obj);
            }
            return UserList;
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