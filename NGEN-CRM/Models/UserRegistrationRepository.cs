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
            object s = DbController.ExecuteScalar("SP_UserCreation", ParamList);
            long isSuccess = Convert.ToInt64(s);
            return isSuccess;
            
        }
        public static long UpdateUser(UserRegistration obj) //Save User
        {

            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("flag", "Update", DbType.String));
            ParamList.Add(new DbParameterList("Id", obj.Id, DbType.Int32));
            ParamList.Add(new DbParameterList("name", obj.Name, DbType.String));
            ParamList.Add(new DbParameterList("login_Id", obj.Login_ID, DbType.String));
            ParamList.Add(new DbParameterList("password", obj.Password, DbType.String));
            ParamList.Add(new DbParameterList("email", obj.Email, DbType.String));
            ParamList.Add(new DbParameterList("role", obj.RoleName, DbType.String));
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
            DataTable tblItems = DbController.ExecuteDataTable("SP_UserCreation", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj.Id = Convert.ToInt32(item["Id"]);
                obj.Name = item["User_Name"].ToString();
                obj.Password = item["User_Pswd"].ToString();
                obj.Login_ID = item["User_LoginID"].ToString();
                obj.InitialUserLoginID= item["User_LoginID"].ToString();
                obj.Email = item["User_Email"].ToString();
                obj.RoleName = item["User_RoleID"].ToString();
                obj.isEdit = true;
              
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