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
        CLSCommen cLSCommen = new CLSCommen();
        public ActionResult Widget()
        {
            Chat obj = new Chat();
            ViewBag.ShowDiv = false;
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

        public ActionResult Index()
        {
            Chat obj = new Chat();
            ViewBag.ShowDiv = false;
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
    }
}