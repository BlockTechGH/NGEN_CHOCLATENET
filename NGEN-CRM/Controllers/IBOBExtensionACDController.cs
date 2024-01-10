﻿using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Microsoft.Ajax.Utilities;

namespace NGEN_CRM.Controllers
{
    public class IBOBExtensionACDController : Controller
    {
        private Connection sqlConnectionString = new Connection();
        CLSCommen cLSCommen = new CLSCommen();

        // GET: IBOBExtensionACD
        public ActionResult Index()
        {
            ExtensionACD_IBOB_Log model = new ExtensionACD_IBOB_Log();
            model.modelList = new List<ExtensionACD_IBOB_LogList>();
            try
            {
                using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    //var data = conn.Query<ExtensionACD_IBOB_LogList>($"select * from ExtensionACD_IBOB_Log where Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}'").ToList();// 
                    //var data = conn.Query<ExtensionACD_IBOB_LogList>(@$"select Ext ExtensionNumber,
                    //                    (select SUM(Cast(InboundACD_Calls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) InboundACD_Calls,
                    //                    (select SUM(Cast(InboundExtension_Calls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) InboundExtension_Calls,
                    //                    (select SUM(Cast(TotalInboundCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) TotalInboundCalls,
                    //                    (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, ACDAvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) ACDAvgTalktime,
                    //                    (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, ExtensionAvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) ExtensionAvgTalktime,
                    //                    (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, AvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) AvgTalktime,
                    //                    (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, TotalTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) TotalTalktime,
                    //                    (select SUM(Cast(OutboundExternalCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) OutboundExternalCalls,
                    //                    (select SUM(Cast(OutboundExtensionCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) OutboundExtensionCalls,
                    //                    (select SUM(Cast(TotalOutboundCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) TotalOutboundCalls,
                    //                    (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, OutboundAvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) OutboundAvgTalktime,
                    //                    (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, TotalOutboundTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) TotalOutboundTalktime,
                    //                    (select SUM(Cast(TotalCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) TotalCalls
                    //                    from (select Distinct ExtensionNumber Ext from ExtensionACD_IBOB_Log where Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}') E").ToList();// 

                    var data = conn.Query<ExtensionACD_IBOB_LogList>($"select Ext ExtensionNumber,\r\n                                        (select SUM(Cast(InboundACD_Calls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) InboundACD_Calls,\r\n                                        (select SUM(Cast(InboundExtension_Calls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) InboundExtension_Calls,\r\n                                        (select SUM(Cast(TotalInboundCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) TotalInboundCalls,\r\n                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, ACDAvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) ACDAvgTalktime,\r\n                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, ExtensionAvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) ExtensionAvgTalktime,\r\n                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, AvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) AvgTalktime,\r\n                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, TotalTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) TotalTalktime,\r\n                                        (select SUM(Cast(OutboundExternalCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) OutboundExternalCalls,\r\n                                        (select SUM(Cast(OutboundExtensionCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) OutboundExtensionCalls,\r\n                                        (select SUM(Cast(TotalOutboundCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) TotalOutboundCalls,\r\n                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, OutboundAvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) OutboundAvgTalktime,\r\n                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, TotalOutboundTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) TotalOutboundTalktime,\r\n                                        (select SUM(Cast(TotalCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}' ) TotalCalls\r\n                                        from (select Distinct ExtensionNumber Ext from ExtensionACD_IBOB_Log where Date='{Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd")}') E").ToList();
                    
                    foreach (var log in data)
                    {
                        ExtensionACD_IBOB_LogList obj = new ExtensionACD_IBOB_LogList();
                        obj = log;
                        if (log.ACDAvgTalktime == null)
                        {
                            obj.ACDAvgTalktime = "0";
                        }
                        if (log.ExtensionAvgTalktime == null)
                        {
                            obj.ExtensionAvgTalktime = "0";
                        }
                        if (log.AvgTalktime == null)
                        {
                            obj.AvgTalktime = "0";
                        }
                        if (log.TotalTalktime == null)
                        {
                            obj.TotalTalktime = "0";
                        }
                        if (log.OutboundAvgTalktime == null)
                        {
                            obj.OutboundAvgTalktime = "0";
                        }
                        if (log.TotalOutboundTalktime == null)
                        {
                            obj.TotalOutboundTalktime = "0";
                        }
                        model.modelList.Add(obj);
                    }
                }

                ViewBag.UserMenu = cLSCommen.GetUserMenu(Convert.ToInt16(Session["UsmID"]));
            }
            catch(Exception e)
            {
                var msg = e.Message;
            }
            

            return View(model);
        }

        public JsonResult GetCallDetailByFromDateToDate(DateTime fromdate, DateTime todate)
        {
            List<ExtensionACD_IBOB_LogList> model = new List<ExtensionACD_IBOB_LogList>();
            using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                conn.Open();
                if (Session["Usmname"].ToString() == "Admin")
                {
                    //model = conn.Query<ExtensionACD_IBOB_LogList>($"select * from ExtensionACD_IBOB_Log where Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' order by Date desc").ToList();
                    model = conn.Query<ExtensionACD_IBOB_LogList>($@"select Ext ExtensionNumber,
                                        (select SUM(Cast(InboundACD_Calls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) InboundACD_Calls,
                                        (select SUM(Cast(InboundExtension_Calls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) InboundExtension_Calls,
                                        (select SUM(Cast(TotalInboundCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) TotalInboundCalls,
                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, ACDAvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) ACDAvgTalktime,
                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, ExtensionAvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) ExtensionAvgTalktime,
                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, AvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) AvgTalktime,
                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, TotalTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) TotalTalktime,
                                        (select SUM(Cast(OutboundExternalCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) OutboundExternalCalls,
                                        (select SUM(Cast(OutboundExtensionCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) OutboundExtensionCalls,
                                        (select SUM(Cast(TotalOutboundCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) TotalOutboundCalls,
                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, OutboundAvgTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) OutboundAvgTalktime,
                                        (select CONVERT(VARCHAR, DATEADD(SECOND, SUM(DATEDIFF(SECOND, '00:00:00', CONVERT(TIME, TotalOutboundTalktime))), '19000101'), 108) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) TotalOutboundTalktime,
                                        (select SUM(Cast(TotalCalls as int)) from ExtensionACD_IBOB_Log where ExtensionNumber=Ext and Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) TotalCalls
                                        from (select Distinct ExtensionNumber Ext from ExtensionACD_IBOB_Log where Date>='{Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd")}' and Date<='{Convert.ToDateTime(todate).ToString("yyyy-MM-dd")}' ) E").ToList();
                }
                //else
                //{
                //    string agentextension = conn.Query<string>($"select Code from agent_queue where Name='{Session["Usmname"]}'").FirstOrDefault();
                //    var queueNos = conn.Query<string>($"select QueueNumber from QueueTable where Extension='{agentextension.Replace("Ext.", "")}'").FirstOrDefault();
                //    model = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension where cr.AgentExtension= '{agentextension.Replace("Ext.", "")}' and RecordingDate>='{Convert.ToDateTime(fromdate).ToString("dd-MM-yyyy")}' and RecordingDate<='{Convert.ToDateTime(todate).ToString("dd-MM-yyyy")}' order by cr.RecordingDate desc").ToList();
                //    if (queueNos.Contains(','))
                //    {
                //        var qNo = queueNos.Split(',').ToList();
                //        foreach (var q in qNo)
                //        {
                //            var qName = conn.Query<string>($"select Name from agent_queue where Code like '%{q}%'").FirstOrDefault();
                //            List<CallRecordingsList> obj = new List<CallRecordingsList>();
                //            obj = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension where cr.QueueName='{qName}' and RecordingDate>='{Convert.ToDateTime(fromdate).ToString("dd-MM-yyyy")}' and RecordingDate<='{Convert.ToDateTime(todate).ToString("dd-MM-yyyy")}' order by cr.RecordingDate desc").ToList();
                //            model.AddRange(obj);
                //        }
                //    }
                //    else
                //    {
                //        var qName = conn.Query<string>($"select Name from agent_queue where Code like '%{queueNos}%'").FirstOrDefault();
                //        List<CallRecordingsList> obj = new List<CallRecordingsList>();
                //        obj = conn.Query<CallRecordingsList>($"select cr.* from CallRecording cr inner join QueueTable qt on cr.AgentExtension=qt.Extension where cr.QueueName='{qName}' and RecordingDate>='{Convert.ToDateTime(fromdate).ToString("dd-MM-yyyy")}' and RecordingDate<='{Convert.ToDateTime(todate).ToString("dd-MM-yyyy")}' order by cr.RecordingDate desc").ToList();
                //        model.AddRange(obj);

                //    }
                //}

                conn.Close();
            }
            return Json(model);
        }
    }
}