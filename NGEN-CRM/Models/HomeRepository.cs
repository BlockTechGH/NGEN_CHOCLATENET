using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QvertzDBLink;
namespace NGEN_CRM.Models
{
    public class HomeRepository
    {
        public static Home GetData (Home Obj) //Get All Roles
        {

            Home obj1 = new Home();
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataSet tblItems = DbController.ExecuteDataSet("SP_GetCallSummary", ParamList);
            //Int32 total = 0;
            foreach (DataRow item in tblItems.Tables[0].Rows)
            {
                obj1.Inbound = item["INBOUND"].ToString();
                obj1.Outbound = item["OUTBOUND"].ToString();
                obj1.Missed = Convert.ToInt32(item["AgentMissed"]).ToString();
                obj1.FromDate = Obj.FromDate;
                obj1.ToDate = Obj.ToDate;
            }
            //foreach (DataRow item1 in tblItems.Tables[1].Rows)
            //{
              
            //    obj1.QMissed = item1["Qmissed"].ToString();
            //    //total =total + Convert.ToInt32(item1["Qmissed"]);
            //    obj1.Missed = obj1.QMissed;
            //}
            return obj1;
        }
        public static  DataTable GetAgentTable(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("SP_GetAgentCallSummary", ParamList);
            tblItems.Columns.Add("SLA", typeof(string));
            foreach (DataRow dr in tblItems.Rows)
            {
                if (dr["INBOUND"].ToString() != "0")
                {
                    dr["SLA"] = ((Convert.ToDecimal(dr["Answered"])*100 / (Convert.ToDecimal(dr["INBOUND"]))).ToString());
                }
                else
                {
                    dr["SLA"] = "0.00";
                }
                if (dr["QMissed"].ToString() != "0" || dr["IMissed"].ToString() != "0")
                { dr.Delete(); }
              
            }
            tblItems.AcceptChanges();
            return tblItems;
        }
        public static  List<Home> GetAgentData(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("SP_GetAgentCallSummary", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Home();
                obj.Agent=(item["Agent"]).ToString();
                if (obj.Agent.EndsWith(" ")) { obj.Agent = obj.Agent.Substring(0, obj.Agent.Length - 1); }
                else { obj.Agent = obj.Agent; }
                obj.Inbound = item["INBOUND"].ToString();
                obj.Outbound = item["OUTBOUND"].ToString();
                obj.Missed = item["MISSED"].ToString();
                obj.InboundAns = item["Answered"].ToString();
                //obj.QMissed= item["QMissed"].ToString();
                //obj.IMissed = item["IMissed"].ToString();
                obj.Total = ((Convert.ToDecimal(obj.Inbound)) + (Convert.ToDecimal(obj.Outbound))).ToString();
                if (obj.Inbound != "0")
                {
                    obj.SLA = ((Convert.ToDecimal(obj.InboundAns)) / (Convert.ToDecimal(obj.Inbound))).ToString("0.00%");
                }
                if (obj.Total != "0")
                {
                    HomeList.Add(obj);

                }
            }
            return HomeList;
        }
        public static List<Home> GetAgentqueueReport(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("SP_GetAgentQCallSummary", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Home();
                obj.Inbound = item["INBOUND"].ToString();
                obj.Agent = (item["Agent"]).ToString();
                if (obj.Agent.EndsWith(" ")) { obj.Agent = obj.Agent.Substring(0, obj.Agent.Length - 1); }
                else { obj.Agent = obj.Agent; }
                obj.Missed = item["QMissed"].ToString(); ;
                obj.InboundAns = ((Convert.ToDecimal(obj.Inbound))- (Convert.ToDecimal(obj.Missed))).ToString();
              
            
                obj.Outbound = item["OUTBOUND"].ToString();
                if (obj.Inbound != "0")
                {
                    obj.SLA = ((Convert.ToDecimal(obj.InboundAns)) / (Convert.ToDecimal(obj.Inbound))).ToString("0.00%");
                }
                //obj.SLA = ((Convert.ToDecimal(obj.InboundAns)) / (Convert.ToDecimal(obj.Inbound))).ToString("0.00%");
                int Total = Convert.ToInt32(obj.Inbound) + Convert.ToInt32(obj.Outbound);
                obj.Total = Total.ToString();
                if (obj.Agent != "")
                {
                    HomeList.Add(obj);

                }
            }
            return HomeList;
        }
        public static List<Home> GetAgentReport(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("SP_GetAgentCallSummary", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Home();
                obj.Agent = (item["Agent"]).ToString();
                if (obj.Agent.EndsWith(" ")) { obj.Agent = obj.Agent.Substring(0, obj.Agent.Length - 1); }
                else { obj.Agent = obj.Agent; }
                obj.InboundAns = item["Answered"].ToString();
                obj.Missed = item["MISSED"].ToString(); ;
                obj.Inbound = item["INBOUND"].ToString();
                obj.Outbound = item["OUTBOUND"].ToString();
                if (obj.Inbound != "0")
                {
                    obj.SLA = ((Convert.ToDecimal(obj.InboundAns)) / (Convert.ToDecimal(obj.Inbound))).ToString("0.00%");
                }
                //obj.SLA = ((Convert.ToDecimal(obj.InboundAns)) / (Convert.ToDecimal(obj.Inbound))).ToString("0.00%");
               int Total = Convert.ToInt32(obj.Inbound) + Convert.ToInt32(obj.Outbound);
                obj.Total = Total.ToString();
                if (obj.Agent != "")
                {
                    HomeList.Add(obj);

                }
            }
            return HomeList;
        }
        public static List<QCall> GetAgentQReportDashboard(Home Obj) //Get All Roles
        {
            List<QCall> HomeList = new List<QCall>();
            QCall obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("[SP_GetAgentQCallSummaryDashboard]", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new QCall();

                obj.Agent = (item["QueName"]).ToString();
                obj.QMissed = item["Qmi"].ToString();
                obj.QAns = item["QAnsw"].ToString();
                Int32 QTotal = Convert.ToInt32(item["Qmi"]) + Convert.ToInt32(item["QAnsw"]);
                obj.QTotal = QTotal.ToString();
                if (obj.QTotal != "0")
                {
                    obj.QSLA = ((Convert.ToDecimal(obj.QAns)) / (Convert.ToDecimal(obj.QTotal))).ToString("0.00%");
                }

                HomeList.Add(obj);

            }
            return HomeList;
        }
            public static List<QCall> GetAgentQReport(Home Obj) //Get All Roles
        {
            List<QCall> HomeList = new List<QCall>();
            QCall obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("[SP_GetAgentQCallSummary]", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new QCall();
                obj.QTotal = (item["TOTAL"]).ToString();
                obj.Agent = (item["Agent"]).ToString();
                if (obj.Agent.EndsWith(" ")) { obj.Agent = obj.Agent.Substring(0, obj.Agent.Length - 1); }
                else { obj.Agent = obj.Agent; }
                obj.QMissed = item["Qmi"].ToString();
                obj.QAns = item["QAnsw"].ToString(); 
                if (obj.QTotal != "0")
                {
                    obj.QSLA = ((Convert.ToDecimal(obj.QAns)) / (Convert.ToDecimal(obj.QTotal))).ToString("0.00%");
                }
                else
                {
                    obj.QSLA = "0";
                }
                HomeList.Add(obj);

            }
            return HomeList;
        }

        public static DataTable GetAgentQReportTable(Home Obj) //Get All Roles
        {
            List<QCall> HomeList = new List<QCall>();
            QCall obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("[SP_GetAgentQCallSummary]", ParamList);
            tblItems.Columns.Add("SLA", typeof(string));
            foreach (DataRow dr in tblItems.Rows)
            {
               
                if (dr["TOTAL"].ToString() != "0")
                {
                    dr["SLA"] = ((Convert.ToDecimal(dr["QAnsw"])*100 / (Convert.ToDecimal(dr["TOTAL"]))).ToString());
                }
                else
                {
                    dr["SLA"] = "0.00";
                }
            }
            return tblItems;
        }
        public static List<Agent> getAllAgent() //Get All data in Branch
        {
            List<DbParameterList> ParamList = new List<DbParameterList>();
            List<Agent> CLAgntlist = new List<Agent>();
            Agent obj;
            DataTable tblItems = DbController.ExecuteDataTable("SP_GetAgents", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Agent();
                obj.AgentName =(item["Name"]).ToString();
                CLAgntlist.Add(obj);
            }
            return CLAgntlist;
        }
        public static List<Agent> getAllQueueName() //Get All data in Branch
        {
            List<DbParameterList> ParamList = new List<DbParameterList>();
            List<Agent> CLAgntlist = new List<Agent>();
            Agent obj=new Agent();
            DataTable tblItems = DbController.ExecuteDataTable("[SP_GetQueueNames]", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Agent();
                obj.AgentName = (item["Name"]).ToString();
                CLAgntlist.Add(obj);
            }
            Agent obj1 = new Agent();
            //obj1.AgentName = "IVR MissedCall";
            //CLAgntlist.Add(obj1);
            return CLAgntlist;
        }
        public static List<Home> GetCallSummaryReport(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj1;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataSet tblItems = DbController.ExecuteDataSet("SP_GetCallSummary", ParamList);
            obj1 = new Home();
            foreach (DataRow item in tblItems.Tables[0].Rows)
            {
                obj1.InboundAns = item["Answered"].ToString();
                obj1.Outbound = item["OUTBOUND"].ToString();
                //total = Convert.ToInt32(item["AgentMissed"]);
                obj1.FromDate = Obj.FromDate;
                obj1.ToDate = Obj.ToDate;
            }
            foreach (DataRow item1 in tblItems.Tables[1].Rows)
            {
                obj1.Inbound = Convert.ToInt32(item1["QTotal"]).ToString();
                obj1.QMissed = item1["Qmissed"].ToString();
                //total =total + Convert.ToInt32(item1["Qmissed"]);
                obj1.Missed = obj1.QMissed;
            }
            //string SLA = (Convert.ToDecimal(TotalINBOUND - TotalMissed) / (Convert.ToDecimal(TotalINBOUND))).ToString("0.0%");
            if (obj1.Inbound!="0")
            {
                    obj1.SLA = (((Convert.ToDecimal(obj1.Inbound))-(Convert.ToDecimal(obj1.Missed))) / (Convert.ToDecimal(obj1.Inbound))).ToString("0.00%");
            }

            obj1.Total = (Convert.ToInt32(obj1.Inbound) + Convert.ToInt32(obj1.Outbound)).ToString();
            HomeList.Add(obj1);
           
            return HomeList;
        }
        public static List<Home> GetQueueCallDetailsReport(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("[SP_GetCallDetailReport]", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Home();
                obj.PhoneNo = item["Phone"].ToString();
                obj.CallDate = item["Call_Date"].ToString();
                obj.CallTime = item["Call_Time"].ToString();
                obj.Duration = item["duration"].ToString();
                obj.CallType = item["Call_Type"].ToString();
                if (obj.CallType == "INBOUND")
                {
                    obj.QueueName = item["QueName"].ToString();
                    //if(item["final_Type"].ToString()!="Queue")
                    //{
                        obj.Agent = item["AgentName"].ToString();
                    if (obj.Agent.EndsWith(" ")) { obj.Agent = obj.Agent.Substring(0, obj.Agent.Length - 1); }
                    else { obj.Agent = obj.Agent; }
                    //}
                    //else { obj.Agent = ""; }
                    //obj.Agent = ""; 
                }
               
                if (obj.CallType == "OUTBOUND")
                {
                    obj.Agent = item["AgentName"].ToString();

                }
                HomeList.Add(obj);
            }
            return HomeList;
        }
        public static List<Home> GetCallDetailsReport(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("SP_GetCallDetailReport", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Home();
                obj.PhoneNo = item["Phone"].ToString();
                obj.CallDate = item["Call_Date"].ToString();
                obj.CallTime = item["Call_Time"].ToString();
                obj.Duration = item["duration"].ToString();
                obj.CallType = item["Call_Type"].ToString();
                if(obj.CallType=="INBOUND")
                {
                    obj.QueueName = item["QueName"].ToString();
                    obj.Agent = item["AgentName"].ToString();
                    if (obj.Agent.EndsWith(" ")) { obj.Agent = obj.Agent.Substring(0, obj.Agent.Length - 1); }
                    else { obj.Agent = obj.Agent; }

                }
                if (obj.CallType == "OUTBOUND")
                {
                    //obj.QueueName = "";
                    obj.Agent = item["AgentName"].ToString();
                    if (obj.Agent.EndsWith(" ")) { obj.Agent = obj.Agent.Substring(0, obj.Agent.Length - 1); }
                    else { obj.Agent = obj.Agent; }
                }
                HomeList.Add(obj);
            }
            return HomeList;
        }
        public static List<Home> GetQMissedCallReport(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("SP_GetMissedCallReport", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Home();
                obj.FinalType = item["final_Type"].ToString();
                if (obj.FinalType == "")
                {
                    obj.QueueName = item["QueName"].ToString();
                    //obj.Agent = item["final_Dispname"].ToString();
                }
              
                else if (obj.FinalType == "Extension")
                {
                    obj.QueueName = item["QueName"].ToString();
                    obj.Agent = item["final_Dispname"].ToString();

                }          
                obj.PhoneNo = item["from_No"].ToString();
                obj.CallDate = item["Call_Date"].ToString();
                obj.CallTime = item["Call_Time"].ToString();
                
               
               
                HomeList.Add(obj);
            }
            return HomeList;
        }
        public static List<Home> GetMissedCallReport(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("SP_GetMissedCallReport", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Home();
                obj.PhoneNo = item["CallFrom"].ToString();
                obj.CallDate = item["Call_Date"].ToString();
                obj.CallTime = item["Call_Time"].ToString();
                obj.Agent = item["AgentName"].ToString();
                if (obj.Agent =="")
                {
                    obj.QueueName = item["QueName"].ToString();
                    obj.Agent = "";
                }
                else
                {
                    obj.QueueName = item["QueName"].ToString(); ;
                    obj.Agent = item["AgentName"].ToString();
                    if (obj.Agent.EndsWith(" ")) { obj.Agent = obj.Agent.Substring(0, obj.Agent.Length - 1); }
                    else { obj.Agent = obj.Agent; }
                }
                HomeList.Add(obj);
            }
            return HomeList;
        }
        public static List<Home> GetIVRMissedCallReport(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("[SP_GetIVRMissedCallReport]", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Home();
                obj.PhoneNo = item["from_No"].ToString();
                obj.CallDate = item["Call_Date"].ToString();
                obj.CallTime = item["Call_Time"].ToString();
                HomeList.Add(obj);
            }
            return HomeList;
        }
        public static List<Home> GetMissedQueueCallReport(Home Obj) //Get All Roles
        {
            List<Home> HomeList = new List<Home>();
            Home obj;
            List<DbParameterList> ParamList = new List<DbParameterList>();
            ParamList.Add(new DbParameterList("FromDate", Obj.FromDate, DbType.Date));
            ParamList.Add(new DbParameterList("ToDate", Obj.ToDate, DbType.Date));
            DataTable tblItems = DbController.ExecuteDataTable("SP_GetMissedCallReport", ParamList);
            foreach (DataRow item in tblItems.Rows)
            {
                obj = new Home();
                obj.FinalType = item["final_Type"].ToString();
                obj.PhoneNo = item["from_No"].ToString();
                obj.CallDate = item["Call_Date"].ToString();
                obj.CallTime = item["Call_Time"].ToString();
                obj.Agent = item["final_Dispname"].ToString();
                if(obj.FinalType=="QUEUE")
                {
                    HomeList.Add(obj);

                }
            }
            return HomeList;
        }
    }
}