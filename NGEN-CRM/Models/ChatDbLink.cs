using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NGEN_CRM.Models
{
    public class ChatDbLink
    {
        SqlConnection sqlCon = null;
        String SqlconString = ConfigurationManager.ConnectionStrings["DefaultConnection1"].ConnectionString;
        public Chat GetWidget(Chat obj)
        {
            Chat obj1 = new Chat();
            using (sqlCon = new SqlConnection(SqlconString))
            {
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("[SP_GetChatSummary]", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@FromDate", SqlDbType.NVarChar).Value = obj.FromDate;
                sql_cmnd.Parameters.AddWithValue("@ToDate", SqlDbType.NVarChar).Value = obj.ToDate;
                using (var da = new SqlDataAdapter(sql_cmnd))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                sqlCon.Close();
                foreach (DataRow item in dt.Rows)
                {

                    obj1.TotalConversation = item["TConv"].ToString();
                    obj1.TotalMsg = item["TMsg"].ToString();
                    obj1.TotalAvgRespTime = item["TAvrt"].ToString();
                    TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(obj1.TotalAvgRespTime));
                    obj1.TotalAvgRespTime = t.Minutes.ToString();
                    obj1.FromDate = obj.FromDate;
                    obj1.ToDate = obj.ToDate;
                }
                return obj1;
            }
        }
        public  List<Chat> GetChatSummary(Chat obj)
        {
            List<Chat> ChatList = new List<Chat>();
            Chat obj1;

            using (sqlCon = new SqlConnection(SqlconString))
            {
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("[SP_GetChatSummary]", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@FromDate", SqlDbType.NVarChar).Value = obj.FromDate;
                sql_cmnd.Parameters.AddWithValue("@ToDate", SqlDbType.NVarChar).Value = obj.ToDate;
                using (var da = new SqlDataAdapter(sql_cmnd))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                sqlCon.Close();
                foreach (DataRow item in dt.Rows)
                {
                    obj1 = new Chat();
                    obj1.TotalConversation = item["TConv"].ToString();
                    obj1.TotalMsg = item["TMsg"].ToString();
                    obj1.TotalAvgRespTime = item["TAvrt"].ToString();
                    TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(obj1.TotalAvgRespTime));
                    obj1.TotalAvgRespTime = t.Minutes.ToString();
                    obj1.InitRespTime= item["TInirt"].ToString();
                    TimeSpan t1 = TimeSpan.FromSeconds(Convert.ToDouble(obj1.InitRespTime));
                    obj1.InitRespTime = t1.Minutes.ToString();
                    obj1.FromDate = obj.FromDate;
                    obj1.ToDate = obj.ToDate;
                    ChatList.Add(obj1);
                }
                return ChatList;
            }
        }
        public  List<Chat> GetAgentData(Chat Obj) //Get All Roles
        {
            List<Chat> ChatList = new List<Chat>();
            Chat obj1;

            using (sqlCon = new SqlConnection(SqlconString))
            {
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("[SP_GetAgentChatSummary]", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@FromDate", SqlDbType.NVarChar).Value = Obj.FromDate;
                sql_cmnd.Parameters.AddWithValue("@ToDate", SqlDbType.NVarChar).Value = Obj.ToDate;
                using (var da = new SqlDataAdapter(sql_cmnd))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                sqlCon.Close();
                foreach (DataRow item in dt.Rows)
                {
                    obj1 = new Chat();
                    obj1.AgentName = item["Employee"].ToString();
                    obj1.TotalConversation = item["TotalCon"].ToString();
                    obj1.InbConv = item["iCnonv"].ToString();
                    obj1.OutConv = item["OutConv"].ToString();
                    obj1.Messages = item["TMsg"].ToString();
                    obj1.AvgRespTime = item["AvgResTime"].ToString();
                    TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(obj1.AvgRespTime));
                    obj1.AvgRespTime = t.Minutes.ToString();

                    obj1.InitRespTime = item["IniResTime"].ToString();
                    TimeSpan t1 = TimeSpan.FromSeconds(Convert.ToDouble(obj1.InitRespTime));
                    obj1.InitRespTime = t1.Minutes.ToString();
                    obj1.SuprRating = Convert.ToDouble( item["SuperWRate"]);
                    //Obj.SuprRating = Math.Round(Convert.ToDecimal(obj1.SuprRating), 2).ToString();
                    double rating = Math.Round(Convert.ToDouble(obj1.SuprRating), 1);
                    obj1.SuprRating = rating;
                    obj1.TotalRating = item["TotalRating"].ToString();
                    obj1.FromDate = Obj.FromDate;
                    obj1.ToDate = Obj.ToDate;
                    ChatList.Add(obj1);
                }
               
            }
            return ChatList;
        }
        public List<Channel> GetChannelData(Chat Obj) //Get All Roles
        {
            List<Channel> ChatList = new List<Channel>();
            Channel obj1;

            using (sqlCon = new SqlConnection(SqlconString))
            {
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("[SP_GetAgentChatSummary]", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@FromDate", SqlDbType.NVarChar).Value = Obj.FromDate;
                sql_cmnd.Parameters.AddWithValue("@ToDate", SqlDbType.NVarChar).Value = Obj.ToDate;
                using (var da = new SqlDataAdapter(sql_cmnd))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                sqlCon.Close();
                foreach (DataRow item in dt.Rows)
                {
                    obj1 = new Channel();
                    obj1.ChannelName = item["Employee"].ToString();
                    obj1.T_Conv = item["TotalCon"].ToString();
                    obj1.T_Msg= item["TMsg"].ToString();
                    obj1.T_AvgResp = item["AvgResTime"].ToString();
                    TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(obj1.T_AvgResp));
                    obj1.T_AvgResp = t.Minutes.ToString();
                    //obj1.InitRespTime = item["IniResTime"].ToString();
                    //obj1.SuprRating = Convert.ToDouble(item["SuperWRate"]);
                    ////Obj.SuprRating = Math.Round(Convert.ToDecimal(obj1.SuprRating), 2).ToString();
                    //double rating = Math.Round(Convert.ToDouble(obj1.SuprRating), 1);
                    //obj1.SuprRating = rating;
                    //obj1.TotalRating = item["TotalRating"].ToString();
                    //obj1.FromDate = Obj.FromDate;
                    //obj1.ToDate = Obj.ToDate;
                    ChatList.Add(obj1);
                }

            }
            return ChatList;
        }
        public  List<Agent> getAllAgent() //Get All data in Branch
        {
           
            List<Agent> CLAgntlist = new List<Agent>();
            Agent obj;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("[SP_GetAgents]", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                using (var da = new SqlDataAdapter(sql_cmnd))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                sqlCon.Close();
                foreach (DataRow item in dt.Rows)
                {
                    obj = new Agent();
                    obj.AgentName = (item["Employee"]).ToString();
                    CLAgntlist.Add(obj);
                }

            }
           
            return CLAgntlist;
        }
        public List<Agent> getAllQueues() //Get All data in Branch
        {

            List<Agent> CLAgntlist = new List<Agent>();
            Agent obj;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("[SP_GetQueues]", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                using (var da = new SqlDataAdapter(sql_cmnd))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                sqlCon.Close();
                foreach (DataRow item in dt.Rows)
                {
                    obj = new Agent();
                    obj.AgentName = (item["OpenChannel"]).ToString();
                    CLAgntlist.Add(obj);
                }

            }

            return CLAgntlist;
        }
        public List<Chat> GetChatDetailsReport(Chat Obj)
        {
            List<Chat> ChatList = new List<Chat>();
            Chat obj1;

            using (sqlCon = new SqlConnection(SqlconString))
            {
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("[SP_GetChatDetails]", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@FromDate", SqlDbType.NVarChar).Value = Obj.FromDate;
                sql_cmnd.Parameters.AddWithValue("@ToDate", SqlDbType.NVarChar).Value = Obj.ToDate;
                using (var da = new SqlDataAdapter(sql_cmnd))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                sqlCon.Close();
                foreach (DataRow item in dt.Rows)
                {
                    obj1 = new Chat();
                    obj1.AgentName = item["Employee"].ToString();
                    obj1.ChatType = item["Type"].ToString();
                    obj1.Messages = item["Messages"].ToString();
                    obj1.AvgRespTime = item["AvrgeReTime"].ToString();
                    TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(obj1.AvgRespTime));
                    obj1.AvgRespTime = t.Minutes.ToString();
                    obj1.InitRespTime = item["Initial_response_time"].ToString();
                    TimeSpan t1 = TimeSpan.FromSeconds(Convert.ToDouble(obj1.InitRespTime));
                    obj1.InitRespTime = t1.Minutes.ToString();
                    obj1.SuprRating = Convert.ToDouble(item["Supervisor_rating"]);
                    double rating = Math.Round(Convert.ToDouble(obj1.SuprRating), 1);
                    obj1.SuprRating = rating;
                    obj1.CreatedOn = item["Created_On"].ToString();
                    obj1.FromDate = Obj.FromDate;
                    obj1.ToDate = Obj.ToDate;
                    ChatList.Add(obj1);
                }

            }
            return ChatList;
        }
        public List<Chat> GetChatQueueDetailsReport(Chat Obj)
        {
            List<Chat> ChatList = new List<Chat>();
            Chat obj1;

            using (sqlCon = new SqlConnection(SqlconString))
            {
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("[SP_GetChatQueueDetails]", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@FromDate", SqlDbType.NVarChar).Value = Obj.FromDate;
                sql_cmnd.Parameters.AddWithValue("@ToDate", SqlDbType.NVarChar).Value = Obj.ToDate;
                using (var da = new SqlDataAdapter(sql_cmnd))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                sqlCon.Close();
                foreach (DataRow item in dt.Rows)
                {
                    obj1 = new Chat();
                    obj1.AgentName = item["Employee"].ToString();
                    obj1.ChatType = item["Type"].ToString();
                    obj1.Messages = item["Messages"].ToString();
                    obj1.AvgRespTime = item["AvrgeReTime"].ToString();
                    TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(obj1.AvgRespTime));
                    obj1.AvgRespTime = t.Minutes.ToString();
                    obj1.InitRespTime = item["Initial_response_time"].ToString();
                    TimeSpan t1 = TimeSpan.FromSeconds(Convert.ToDouble(obj1.InitRespTime));
                    obj1.InitRespTime = t1.Minutes.ToString();
                    //obj1.TotalAvgRespTime = t.Minutes.ToString();
                    obj1.SuprRating = Convert.ToDouble(item["Supervisor_rating"]);
                    double rating = Math.Round(Convert.ToDouble(obj1.SuprRating), 1);
                    obj1.SuprRating = rating;
                    obj1.CreatedOn = item["Created_On"].ToString();
                    obj1.Channel= item["OpenChannel"].ToString();
                    obj1.FromDate = Obj.FromDate;
                    obj1.ToDate = Obj.ToDate;
                    ChatList.Add(obj1);
                }

            }
            return ChatList;
        }
        public List<Chat> GetQueueSummary(Chat Obj) //Get All Roles
        {
            List<Chat> ChatList = new List<Chat>();
            Chat obj1;

            using (sqlCon = new SqlConnection(SqlconString))
            {
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("[SP_GetQueueSummary]", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@FromDate", SqlDbType.NVarChar).Value = Obj.FromDate;
                sql_cmnd.Parameters.AddWithValue("@ToDate", SqlDbType.NVarChar).Value = Obj.ToDate;
                using (var da = new SqlDataAdapter(sql_cmnd))
                {
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                sqlCon.Close();
                foreach (DataRow item in dt.Rows)
                {
                    obj1 = new Chat();
                    obj1.TotalConversation =item["TotalCon"].ToString();
                    obj1.QueueName = item["OpenChannel"].ToString();
                    obj1.InbConv = item["iCnonv"].ToString();
                    obj1.OutConv = item["OutConv"].ToString();
                    obj1.Messages = item["TMsg"].ToString();
                    obj1.AvgRespTime = item["AvgResTime"].ToString();
                    TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(obj1.AvgRespTime));
                    obj1.AvgRespTime = t.Minutes.ToString();
                    obj1.InitRespTime = item["IniResTime"].ToString();
                    TimeSpan t1 = TimeSpan.FromSeconds(Convert.ToDouble(obj1.InitRespTime));
                    obj1.InitRespTime = t1.Minutes.ToString();
                    obj1.SuprRating = Convert.ToDouble(item["SuperWRate"]);
                    //Obj.SuprRating = Math.Round(Convert.ToDecimal(obj1.SuprRating), 2).ToString();
                    double rating = Math.Round(Convert.ToDouble(obj1.SuprRating), 1);
                    obj1.SuprRating = rating;
                    obj1.TotalRating = item["TotalRating"].ToString();
                    obj1.FromDate = Obj.FromDate;
                    obj1.ToDate = Obj.ToDate;
                    ChatList.Add(obj1);
                }

            }
            return ChatList;
        }
    }
}