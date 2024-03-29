﻿using Newtonsoft.Json;
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
        CLSCommen cLSCommen = new CLSCommen();
        public ActionResult Widget()
        {
            Home obj = new Home();
            obj.ToDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj.FromDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            //obj.ToDate = DateTime.Now.AddDays(-11).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            //obj.FromDate = DateTime.Now.AddDays(-12).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj = HomeRepository.GetData(obj);
            obj.CallList = HomeRepository.GetAgentData(obj);
            obj.QCallList = HomeRepository.GetAgentQReportDashboard(obj);
            double Avg = 0.00;
            if (obj.QCallList.Count != 0)
            {
                Avg = obj.QCallList.Where(item => item.QSLA != "0").Average(item => Convert.ToDouble(item.QSLA.ToString().Replace(@"%", "")));
                Avg = Math.Round(Avg, 1);
            }
            
            int TotalMissed = Convert.ToInt32(obj.Missed);
            int TotalINBOUND = Convert.ToInt32(obj.Inbound);
            int TotalOUTBOUND = Convert.ToInt32(obj.Outbound);
            obj.InboundAns = (Convert.ToInt32(obj.Inbound) - Convert.ToInt32(obj.Missed)).ToString();
            int Total = TotalINBOUND + TotalOUTBOUND;
            ViewBag.Missed = TotalMissed;
            ViewBag.Inbound = obj.InboundAns;
            ViewBag.Outbound = TotalOUTBOUND;
            if(TotalINBOUND!=0)
            {
                //string SLA = (Convert.ToDecimal(obj.InboundAns) / (Convert.ToDecimal(TotalINBOUND))).ToString("0.0%");
                string SLA = Avg.ToString();
                ViewBag.SLA = SLA.Replace(@"%", "");
            }
            else { ViewBag.SLA = "0"; }
            ViewBag.Total = Total;

            //SideMenuName
            //using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            //{
            //    conn.Open();
            //    var menuIds = conn.Query<string>($"select MenuIDs from UserMenuAccessNgen where UserID={Session["UsmID"]}").FirstOrDefault();
            //    ViewBag.UserMenu = conn.Query<SideMenuNgen>($"select * from SideMenuNgen where MenuID in ({menuIds})").ToList();
            //    conn.Close();
            //}
            ViewBag.UserMenu = cLSCommen.GetUserMenu(Convert.ToInt16(Session["UsmID"]));

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
            //obj.ToDate = DateTime.Now.AddDays(-11).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            //obj.FromDate = DateTime.Now.AddDays(-12).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj = HomeRepository.GetData(obj);
            obj.CallList = HomeRepository.GetAgentData(obj);
            obj.QCallList = HomeRepository.GetAgentQReportDashboard(obj);
            double Avg = 0.00;
            if (obj.QCallList.Count != 0)
            {
                Avg = obj.QCallList.Where(item => item.QSLA != "0").Average(item => Convert.ToDouble(item.QSLA.ToString().Replace(@"%", "")));
                Avg = Math.Round(Avg, 1);
            }
            
            
            int TotalINBOUND = Convert.ToInt32(obj.Inbound);
            int TotalOUTBOUND = Convert.ToInt32(obj.Outbound);
            int TotalMissed = Convert.ToInt32(obj.Missed);
            obj.InboundAns = (Convert.ToInt32(obj.Inbound) - Convert.ToInt32(obj.Missed)).ToString();
            int Total = TotalINBOUND + TotalOUTBOUND;
            ViewBag.Missed = TotalMissed;
            ViewBag.Inbound = obj.InboundAns;
            ViewBag.Outbound = TotalOUTBOUND;
            string SLA = "0";
            if (TotalINBOUND !=0)
            {
                //SLA = (Convert.ToDecimal(obj.InboundAns) / (Convert.ToDecimal(TotalINBOUND))).ToString("0.0%");
                SLA = Avg.ToString();
                ViewBag.SLA = SLA.Replace(@"%", "");
            }
            else { ViewBag.SLA = "0"; }
            ViewBag.Total = Total;

            //SideMenuName
            //using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            //{
            //    conn.Open();
            //    var menuIds = conn.Query<string>($"select MenuIDs from UserMenuAccessNgen where UserID={Session["UsmID"]}").FirstOrDefault();
            //    ViewBag.UserMenu = conn.Query<SideMenuNgen>($"select * from SideMenuNgen where MenuID in ({menuIds})").ToList();
            //    conn.Close();
            //}
            ViewBag.UserMenu = cLSCommen.GetUserMenu(Convert.ToInt16(Session["UsmID"]));

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

            //SideMenuName
            //using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            //{
            //    conn.Open();
            //    var menuIds = conn.Query<string>($"select MenuIDs from UserMenuAccessNgen where UserID={Session["UsmID"]}").FirstOrDefault();
            //    ViewBag.UserMenu = conn.Query<SideMenuNgen>($"select * from SideMenuNgen where MenuID in ({menuIds})").ToList();
            //    conn.Close();
            //}
            ViewBag.UserMenu = cLSCommen.GetUserMenu(Convert.ToInt16(Session["UsmID"]));

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