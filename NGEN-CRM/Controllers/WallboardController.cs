using Newtonsoft.Json;
using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class WallboardController : Controller
    {

        public ActionResult Widget()
        {
            Home obj = new Home();
            obj.ToDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj.FromDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj = HomeRepository.GetData(obj);
            obj.CallList = HomeRepository.GetAgentData(obj);
            obj.QCallList = HomeRepository.GetAgentQReportDashboard(obj);
            DataTable dtQSummary = HomeRepository.GetAgentQReportTable(obj);
            DataTable dtAgent = HomeRepository.GetAgentTable(obj);
            int TotalINBOUND = Convert.ToInt32(obj.Inbound);
            int TotalOUTBOUND = Convert.ToInt32(obj.Outbound);
            int TotalMissed = Convert.ToInt32(obj.Missed);
            int Total = TotalINBOUND + TotalOUTBOUND;
            ViewBag.Missed = TotalMissed;
            ViewBag.Inbound = TotalINBOUND;
            ViewBag.Outbound = TotalOUTBOUND;
            ViewBag.Total = Total;
            return PartialView("IndexPa", obj);
        }
        // GET: Wallboard
        public ActionResult Index()
        {
            Home obj = new Home();
            ViewBag.ShowDiv = false;
            ViewBag.Message = "Your contact page.";
            obj.ToDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj.FromDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj = HomeRepository.GetData(obj);
            obj.CallList = HomeRepository.GetAgentData(obj);
            obj.QCallList = HomeRepository.GetAgentQReportDashboard(obj);
            DataTable dtQSummary = HomeRepository.GetAgentQReportTable(obj);
            DataTable dtAgent = HomeRepository.GetAgentTable(obj);

            //obj.Qstring = GetChartString(dtQSummary);
            //obj.Agentstring = GetChartString(dtAgent);
            //ViewBag.Agentstring = obj.Agentstring;
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
            obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj = HomeRepository.GetData(obj);
            int TotalINBOUND = Convert.ToInt32(obj.Inbound);
            int TotalOUTBOUND = Convert.ToInt32(obj.Outbound);
            int Missed = Convert.ToInt32(obj.Missed);
            ViewBag.Inbound = TotalINBOUND;
            ViewBag.Outbound = TotalOUTBOUND;
            ViewBag.Missed = Missed;
            ViewBag.Total = TotalINBOUND + TotalOUTBOUND;
            dtQSummary = HomeRepository.GetAgentQReportTable(obj);
            dtAgent = HomeRepository.GetAgentTable(obj);
            obj.Qstring = GetChartString(dtQSummary);
            obj.Agentstring = GetChartString(dtAgent);
            
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
       
       
    }
}