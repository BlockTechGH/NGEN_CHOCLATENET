using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class ChatDashboardController : Controller
    {
        // GET: ChatDashboard
       
        public ActionResult Dashboard()
        {
            Chat obj = new Chat();
            ViewBag.ShowDiv = false;
            ViewBag.Message = "Your contact page.";
            obj.ToDate =Convert.ToDateTime ("2021/07/30").ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj.FromDate = Convert.ToDateTime("2021/07/29").ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            //obj.ToDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            //obj.FromDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            ChatDbLink ObjCh = new ChatDbLink();
            obj = ObjCh.GetWidget(obj);
            obj.ChatList = ObjCh.GetAgentData(obj);
            ////obj.QCallList = HomeRepository.GetAgentQReport(obj);
            string TotalConv =(obj.TotalConversation);
            string TotalMsg = (obj.TotalMsg);
            string TotalAvgT = (obj.TotalAvgRespTime);
            ViewBag.TotalConv = TotalConv;
            ViewBag.TotalMsg = TotalMsg;
            ViewBag.TotalAvgT = TotalAvgT;
            return View(obj);
        }
        [HttpPost]
        public ActionResult Dashboard(Chat obj)
        {
            DataTable dt = new DataTable();
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
            
            ChatDbLink ObjCh = new ChatDbLink();
            obj = ObjCh.GetWidget(obj);
            obj.ChatList = ObjCh.GetAgentData(obj);
            string TotalConv = (obj.TotalConversation);
            string TotalMsg = (obj.TotalMsg);
            string TotalAvgT = (obj.TotalAvgRespTime);
            ViewBag.TotalConv = TotalConv;
            ViewBag.TotalMsg = TotalMsg;
            ViewBag.TotalAvgT = TotalAvgT;
            return View("Dashboard", obj);


        }
    }
}