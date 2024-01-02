using Microsoft.Ajax.Utilities;
using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class Agent_QueueCreationController : Controller
    {
        CLSCommen cLSCommen = new CLSCommen();

        // GET: Agent_QueueCreation
        public ActionResult Create()
        {
            Agent_Queue obj = new Agent_Queue();
            obj.Agentlist = Agent_QueueRepository.getAllAgentQueue();
            ViewBag.Types = new List<SelectListItem>{ new SelectListItem{
                Text="Agent",
                Value = "A"
            },
            new SelectListItem{
                Text="Queue",
                Value = "Q"
            }};

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
        public ActionResult Create(Agent_Queue obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Agent_QueueRepository.SaveAgentQueue(obj) > 0) //Save Success
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Saved Successfully');window.location='/Agent_QueueCreation/Create'</script>");
                    }
                    return RedirectToAction("Error404", "Home");//Save Failed
                }
                catch { return RedirectToAction("Error404", "Home"); } //If Error
            }

            //SideMenuName
            //using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            //{
            //    conn.Open();
            //    var menuIds = conn.Query<string>($"select MenuIDs from UserMenuAccessNgen where UserID={Session["UsmID"]}").FirstOrDefault();
            //    ViewBag.UserMenu = conn.Query<SideMenuNgen>($"select * from SideMenuNgen where MenuID in ({menuIds})").ToList();
            //    conn.Close();
            //}
            ViewBag.UserMenu = cLSCommen.GetUserMenu(Convert.ToInt16(Session["UsmID"]));

            return RedirectToAction("Create");
        }
        public ActionResult Edit(long AgentID) //Get data for Edit
        {
            Agent_Queue obj = new Agent_Queue();
            obj = Agent_QueueRepository.getByID(AgentID);
            ViewBag.Types = new List<SelectListItem>{ new SelectListItem{
                Text="Agent",
                Value = "A"
            },
            new SelectListItem{
                Text="Queue",
                Value = "Q"
            }};

            //SideMenuName
            //using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            //{
            //    conn.Open();
            //    var menuIds = conn.Query<string>($"select MenuIDs from UserMenuAccessNgen where UserID={Session["UsmID"]}").FirstOrDefault();
            //    ViewBag.UserMenu = conn.Query<SideMenuNgen>($"select * from SideMenuNgen where MenuID in ({menuIds})").ToList();
            //    conn.Close();
            //}
            ViewBag.UserMenu = cLSCommen.GetUserMenu(Convert.ToInt16(Session["UsmID"]));

            return View("Create", obj);
        }
        [HttpPost]
        public ActionResult Edit(Agent_Queue obj) //Update
        {
            //obj.RomStatusUser = Convert.ToInt64(Session["UsmID"].ToString());
            if (Agent_QueueRepository.UpdateAgentQueue(obj) > 0) //Update success
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Updated Successfully');window.location='/Agent_QueueCreation/Create'</script>");
            }

            //SideMenuName
            //using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            //{
            //    conn.Open();
            //    var menuIds = conn.Query<string>($"select MenuIDs from UserMenuAccessNgen where UserID={Session["UsmID"]}").FirstOrDefault();
            //    ViewBag.UserMenu = conn.Query<SideMenuNgen>($"select * from SideMenuNgen where MenuID in ({menuIds})").ToList();
            //    conn.Close();
            //}
            ViewBag.UserMenu = cLSCommen.GetUserMenu(Convert.ToInt16(Session["UsmID"]));

            return RedirectToAction("Create");
        }
        public ActionResult Delete(long AgentID) //Delete
        {
            long Success = Agent_QueueRepository.DeleteAgentQueue(AgentID);
            if (Success == 0)
            {//Delete not success
                return Content("<script language='javascript' type='text/javascript'>alert('Reference Exist, Unable to delete');window.location='/Agent_QueueCreation/Create'</script>");
                return RedirectToAction("Create");
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Deleteted Succesfully');window.location='/Agent_QueueCreation/Create'</script>");
                return RedirectToAction("Create");
            }

            //SideMenuName
            //using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            //{
            //    conn.Open();
            //    var menuIds = conn.Query<string>($"select MenuIDs from UserMenuAccessNgen where UserID={Session["UsmID"]}").FirstOrDefault();
            //    ViewBag.UserMenu = conn.Query<SideMenuNgen>($"select * from SideMenuNgen where MenuID in ({menuIds})").ToList();
            //    conn.Close();
            //}
            ViewBag.UserMenu = cLSCommen.GetUserMenu(Convert.ToInt16(Session["UsmID"]));

            return RedirectToAction("Create");
        }
       
        public bool isCodeExist(string Chkcode, string InitialCode)
        {
           bool ChkcodeE = Agent_QueueRepository.isCodeExist(Chkcode);
            if (Chkcode == InitialCode)
            {
                return true;
            }
            else
            {
                if (!Agent_QueueRepository.isCodeExist(Chkcode))
                {
                    return true; // Code is Available
                }
                return false;
            }
        }
    }
}