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
                    obj1.FromDate = obj.FromDate;
                    obj1.ToDate = obj.ToDate;
                }
                return obj1;
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
                    obj1.InbConv = item["iCnonv"].ToString();
                    obj1.OutConv = item["OutConv"].ToString();
                    obj1.Messages = item["TMsg"].ToString();
                    obj1.AvgRespTime = item["AvgResTime"].ToString();
                    obj1.InitRespTime = item["IniResTime"].ToString();
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
    }
}