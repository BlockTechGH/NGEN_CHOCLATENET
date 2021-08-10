using Newtonsoft.Json;
using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            Home obj = new Home();
            ViewBag.ShowDiv = false;
            ViewBag.Message = "Your contact page.";
            obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

            
            obj = HomeRepository.GetData(obj);
            obj.CallList = HomeRepository.GetAgentData(obj);
            DataTable dtQSummary = HomeRepository.GetAgentQReportTable(obj);
            DataTable dtAgent = HomeRepository.GetAgentTable(obj);

            obj.Qstring = GetChartString(dtQSummary);
            obj.Agentstring = GetChartString(dtAgent);          
            ViewBag.Agentstring = obj.Agentstring;
            int TotalINBOUND = Convert.ToInt32(obj.Inbound);
            int TotalOUTBOUND = Convert.ToInt32(obj.Outbound);
            int TotalMissed = Convert.ToInt32(obj.Missed);
            int Total = TotalINBOUND + TotalOUTBOUND;
            ViewBag.Missed = TotalMissed;
            ViewBag.Inbound = TotalINBOUND;
            ViewBag.Outbound = TotalOUTBOUND;
            ViewBag.Total = Total;
            return View(obj);
        }
        [HttpPost]
        public ActionResult Dashboard(Home obj)
        {
            DataTable dtQSummary = new DataTable();
            DataTable dtAgent = new DataTable();
            if (obj.Duration == "1")
            {
                obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            }
            else if (obj.Duration == "2")
            {
                obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

            }
            else if (obj.Duration == "3")
            {
                obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = DateTime.Now.AddDays(-15).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

            }
            else if (obj.Duration == "4")
            {
                obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = DateTime.Now.AddDays(-30).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

            }
            else
            {
                obj.ToDate = Convert.ToDateTime(obj.ToDate).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = Convert.ToDateTime(obj.FromDate).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));

            }
            obj = HomeRepository.GetData(obj);
            int TotalINBOUND = Convert.ToInt32(obj.Inbound);
            int TotalOUTBOUND = Convert.ToInt32(obj.Outbound);
            int Missed = Convert.ToInt32(obj.Missed);
            ViewBag.Inbound = TotalINBOUND;
            ViewBag.Outbound = TotalOUTBOUND;
            ViewBag.Missed = Missed;
            ViewBag.Total = TotalINBOUND + TotalOUTBOUND;
            dtQSummary = HomeRepository.GetAgentQReportTable(obj);
            dtAgent= HomeRepository.GetAgentTable(obj);
            obj.Qstring = GetChartString(dtQSummary);
            obj.Agentstring = GetChartString(dtAgent);
           
            //foreach (DataRow item in tblItems.Rows)
            //{
            //    obj = new Home();
            //    obj.Agent = (item["Agent"]).ToString();
            //    obj.Inbound = item["INBOUND"].ToString();
            //    obj.Outbound = item["OUTBOUND"].ToString();
            //    obj.Missed = item["MISSED"].ToString();
            //    obj.InboundAns = item["Answered"].ToString();
            //    obj.QMissed = item["QMissed"].ToString();
            //    obj.IMissed = item["IMissed"].ToString();
            //    if (obj.Inbound != "0")
            //    {
            //        obj.SLA = ((Convert.ToDecimal(obj.InboundAns)) / (Convert.ToDecimal(obj.Inbound))).ToString("0.00%");
            //    }
            //    if (obj.QMissed == "0" && obj.IMissed == "0")
            //    {
            //        HomeList.Add(obj);

            //    }
            //}
            //return HomeList;



            return View("Dashboard", obj);


        }
        public string GetChartString(DataTable dt)
        {
            List<object> iData = new List<object>();
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            string data = JsonConvert.SerializeObject(iData);
            string result = string.Concat(data.Where(c => !char.IsWhiteSpace(c)));
            return result;
        }
        public string populateXAxis(Home obj)
        {
            DataTable dt = HomeRepository.GetAgentTable(obj);
            string statemnt = "";
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
               
                var value = row["Agent"].ToString();
                sb.Append("" +value + ',' + "");
            }
            statemnt = sb.ToString();
            if (statemnt != null && statemnt.Length > 0)
            {
                statemnt = statemnt.Substring(0, statemnt.Length - 1);
            }
            string result = string.Concat(statemnt.Where(c => !char.IsWhiteSpace(c)));
            string data = JsonConvert.SerializeObject(result);
            //var text = statemnt.Replace("/ / g", "&nbsp;");
            return data;
        }
        public string populateYAxis(Home obj)
        {
            DataTable dt = HomeRepository.GetAgentTable(obj);
            string statemnt = "";
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
              
                var value = row["INBOUND"].ToString();
                var va1 = row["MISSED"].ToString();
                //sb.Append("" + value + ',' + "");
                sb.Append("" + value + ',' + ""+""+va1+','+"");
            }
            statemnt = sb.ToString();
            if (statemnt != null && statemnt.Length > 0)
            {
                statemnt = statemnt.Substring(0, statemnt.Length - 1);
            }
            return statemnt;
        }
    }
}