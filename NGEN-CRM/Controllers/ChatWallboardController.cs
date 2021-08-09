using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class ChatWallboardController : Controller
    {
        public ActionResult Index()
        {
            Chat obj = new Chat();
            ViewBag.ShowDiv = false;
            ViewBag.Message = "Your contact page.";
            obj.ToDate = Convert.ToDateTime("2021/07/25").ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            obj.FromDate = Convert.ToDateTime("2021/07/24").ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            //obj.ToDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            //obj.FromDate = DateTime.Now.ToString("yyyy/MM/dd", new System.Globalization.CultureInfo("nl-NL"));
            ChatDbLink ObjCh = new ChatDbLink();
            obj = ObjCh.GetWidget(obj);
            obj.ChatList = ObjCh.GetAgentData(obj);
            obj.ChannelList = ObjCh.GetChannelData(obj);
            string TotalConv = (obj.TotalConversation);
            string TotalMsg = (obj.TotalMsg);
            string TotalAvgT = (obj.TotalAvgRespTime);
          
            ViewBag.TotalConv = TotalConv;
            ViewBag.TotalMsg = TotalMsg;
            ViewBag.TotalAvgT = TotalAvgT;
            return View(obj);
        }
    }
}