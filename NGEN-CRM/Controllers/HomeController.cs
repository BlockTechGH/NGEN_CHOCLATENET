using Newtonsoft.Json;
using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace NGEN_CRM.Controllers
{
  
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            Home obj = new Home();
            ViewBag.ShowDiv = false;
            ViewBag.Message = "Your contact page.";
            obj.ToDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj.FromDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj = HomeRepository.GetData(obj);
            obj.CallList = HomeRepository.GetAgentData(obj);
            //obj.QCallList = HomeRepository.GetAgentQReport(obj);
            obj.QCallList = HomeRepository.GetAgentQReportDashboard(obj);
            int TotalINBOUND =Convert.ToInt32( obj.Inbound);
            int TotalOUTBOUND = Convert.ToInt32(obj.Outbound);
            int TotalMissed = Convert.ToInt32(obj.Missed);
            int Total = TotalINBOUND + TotalOUTBOUND;
            ViewBag.Missed = TotalMissed;
            ViewBag.Inbound = TotalINBOUND;
            ViewBag.Outbound = TotalOUTBOUND;
            ViewBag.Total = Total;
            return View(obj);
        }
      
        public ActionResult Create(Home obj)
        {
            DataTable dt = new DataTable();
            if(obj.Duration=="1")
            {
                obj.ToDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
                obj.FromDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            }
            else if(obj.Duration == "2")
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
            ViewBag.Total = TotalINBOUND+TotalOUTBOUND;

            obj.CallList = HomeRepository.GetAgentData(obj);
            //obj.QCallList = HomeRepository.GetAgentQReport(obj);
            obj.QCallList = HomeRepository.GetAgentQReportDashboard(obj);
            dt = HomeRepository.GetAgentQReportTable(obj);

            List<object> iData = new List<object>();
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            obj.Data = iData;

            string strserialize = JsonConvert.SerializeObject(obj.Data);
            obj.PhoneNo = strserialize;
            return View("contact",obj);


        }
    }
}