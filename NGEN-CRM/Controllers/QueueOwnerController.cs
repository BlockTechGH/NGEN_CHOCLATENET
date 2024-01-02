using Dapper;
using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class QueueOwnerController : Controller
    {
        private Connection sqlConnectionString = new Connection();
        CLSCommen cLSCommen = new CLSCommen();

        // GET: QueueOwner
        public ActionResult Create()
        {
            QueueOwner model = new QueueOwner();
            model.queueownerList = new List<QueueOwnerList>();
            using (var conn=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                conn.Open();
                ViewBag.Agents = new SelectList(HomeRepository.getAllAgent(), "AgentName", "AgentName");
                ViewBag.Queue = new SelectList(HomeRepository.getAllQueueName(), "AgentName", "AgentName");

                //ViewBag.QueueNo = conn.Query<string>($"SELECT SUBSTRING(Code, LEN(Code) / 2 + 2, LEN(Code)) AS QueueExtension FROM agent_queue where Type='Q'").ToList();
                ViewBag.QueueNo = new SelectList(HomeRepository.getAllQueueNumber(), "QueueExtension", "QueueExtension");
                model.queueownerList = conn.Query<QueueOwnerList>($"select * from QueueTable").ToList();
                conn.Close();
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

            return View(model);
        }

        public string GetAgentExtension(string agent)
        {
            string extension = "";
            using (var conn=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                conn.Open();
                extension = conn.Query<string>($"SELECT SUBSTRING(Code, LEN(Code) / 2 + 2, LEN(Code)) AS Extension FROM agent_queue where Name like '%{agent}%'").FirstOrDefault();
                conn.Close();
            }
            return extension;
        }

        public string CreateUpdateQueueOwner(int id,string agent,string extension,string[] queue)
        {
            string message = "", que = "";
            using (var conn = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                conn.Open();
                foreach(var item in queue)
                {
                    if(string.IsNullOrEmpty(que))
                    {
                        que = item;
                    }
                    else
                    {
                        que = que + "," + item;
                    }
                }
                if (id != 0)
                {
                    var insert = conn.Execute($"update QueueTable set AgentName='{agent}',Extension='{extension}',QueueNumber='{que}' where QueueID={id}");
                    if (insert == 1)
                    {
                        message = "Successfully Updated";
                    }
                    else
                    {
                        message = "Failed";
                    }
                }
                else
                {
                    var insert = conn.Execute($"insert into QueueTable(AgentName,Extension,QueueNumber) values('{agent}','{extension}','{que}')");
                    if (insert == 1)
                    {
                        message = "Successfully Added";
                    }
                    else
                    {
                        message = "Failed";
                    }
                }
                
                conn.Close();
            }
            return message;
        }

        public ActionResult Edit(long queueid) //Get data for Edit
        {
            QueueOwner obj = new QueueOwner();
            using(var conn=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                conn.Open();
                ViewBag.Agents = new SelectList(HomeRepository.getAllAgent(), "AgentName", "AgentName");
                ViewBag.Queue = new SelectList(HomeRepository.getAllQueueName(), "AgentName", "AgentName");
                obj.Id = Convert.ToInt16(queueid);
                //ViewBag.QueueNo = conn.Query<string>($"SELECT SUBSTRING(Code, LEN(Code) / 2 + 2, LEN(Code)) AS QueueExtension FROM agent_queue where Type='Q'").ToList();
                ViewBag.QueueNo = new SelectList(HomeRepository.getAllQueueNumber(), "QueueExtension", "QueueExtension");
                obj.queueownerList = conn.Query<QueueOwnerList>($"select * from QueueTable").ToList();

                obj.AgentIds = conn.Query<string>($"select AgentName from QueueTable where QueueID={queueid}").FirstOrDefault();
                //obj.AgentIds = agentname;
                obj.ExtensionNo = conn.Query<string>($"select Extension from QueueTable where QueueID={queueid}").FirstOrDefault();
                var queueno = conn.Query<string>($"select QueueNumber from QueueTable where QueueID={queueid}").FirstOrDefault();
                if(queueno.Contains(','))
                {
                    obj.QueueNos = queueno.Split(',');
                }
                else
                {
                    //string inputString = "Hello";
                    string[] stringArray = new string[] { queueno };
                    obj.QueueNos = stringArray;
                }
                conn.Close();
            }
            //obj = Agent_QueueRepository.getByID(queueid);

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
    }
}