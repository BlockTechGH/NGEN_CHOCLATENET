using QvertzDBLink;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class Agent_QueueRepository
    {
        public static long SaveAgentQueue(Agent_Queue obj) //Save Agent
        {
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("flag", "Insert", DbType.String));
            ParamList.Add(new DbParameterList("ID", 0, DbType.Int64));
            ParamList.Add(new DbParameterList("name", obj.Name, DbType.String));
            ParamList.Add(new DbParameterList("code", obj.Code, DbType.String));
            if(obj.Type=="Agent")
            {
                obj.Type = "A";
            }
            else 
            {
                obj.Type = "Q";
            }
            ParamList.Add(new DbParameterList("type", obj.Type, DbType.String));
            object s = DbController.ExecuteScalar("SP_AgentQueueCreation", ParamList);
            long isSuccess = Convert.ToInt64(s);
            return isSuccess;
        }
        public static long UpdateAgentQueue(Agent_Queue obj) //Update Agent
        {
            if (obj.Type == "A")
            {
                obj.Type = "A";
            }
            else
            {
                obj.Type = "Q";
            }
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("id", obj.Id, DbType.Int32));
            ParamList.Add(new DbParameterList("flag", "Update", DbType.String));
            ParamList.Add(new DbParameterList("name", obj.Name, DbType.String));
            ParamList.Add(new DbParameterList("code", obj.Code, DbType.String));
            ParamList.Add(new DbParameterList("type", obj.Type, DbType.String));
            object s = DbController.ExecuteScalar("SP_AgentQueueCreation", ParamList);
            long isSuccess = Convert.ToInt64(s);
            return isSuccess;
        }
        public static long DeleteAgentQueue(long id) //Delete Role
        {
            Agent_Queue obj = getByID(id);
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("id", id, DbType.Int32));
            ParamList.Add(new DbParameterList("code", obj.Code, DbType.String));
            ParamList.Add(new DbParameterList("flag", "Delete", DbType.String));
            object s= DbController.ExecuteScalar("SP_AgentQueueCreation", ParamList);
            long isSuccess = Convert.ToInt64(s);
            return isSuccess;
        }
        public static List<Agent_Queue> getAllAgentQueue() //Get All Roles
        {
            List<DbParameterList> ParamList = new List<DbParameterList>();
            List<Agent_Queue> RoleList = new List<Agent_Queue>();
            Agent_Queue obj;
            ParamList.Add(new DbParameterList("flag", "All", DbType.String));
            DataTable tblItems = DbController.ExecuteDataTable("SP_AgentQueueCreation", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Agent_Queue();
                obj.Id = Convert.ToInt32(item["id"]);
                obj.Code = item["Code"].ToString();
                obj.Name = item["Name"].ToString();
                RoleList.Add(obj);
            }
            return RoleList;
        }
        public static Agent_Queue getByID(long ID) //Get Role Details by ID
        {
            List<DbParameterList> ParamList = new List<DbParameterList>();
            Agent_Queue obj = new Agent_Queue();
            ParamList.Add(new DbParameterList("id", ID, DbType.Int32));
            ParamList.Add(new DbParameterList("flag", "Id", DbType.String));
            DataTable tblItems = DbController.ExecuteDataTable("SP_AgentQueueCreation", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj.Id = Convert.ToInt32(item["Id"]);
                obj.Name = item["Name"].ToString();
                obj.Code = item["Code"].ToString();
                obj.Type = item["Type"].ToString();
                obj.isEdit = true;
                obj.InitialCode = obj.Code;
            }

            obj.Agentlist = getAllAgentQueue();
            //obj.isEdit = true;
            return obj;
        }
        public static bool isCodeExist(string Code) //Role Code Exist Check
        {
            bool isSExist = false;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            Agent_Queue obj = new Agent_Queue();
            ParamList.Add(new DbParameterList("code", Code, DbType.String));
            ParamList.Add(new DbParameterList("flag", "Code", DbType.String));
            DataTable tblItems = DbController.ExecuteDataTable("SP_AgentQueueCreation", ParamList);
            if(tblItems.Rows.Count>0)
            {
                isSExist = true;
            }
            else { isSExist = false; }
            return isSExist;
        }
    }
}