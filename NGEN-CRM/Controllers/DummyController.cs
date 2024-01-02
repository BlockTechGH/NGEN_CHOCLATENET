using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class DummyController : Controller
    {
        // GET: Dummy
        public ActionResult Dummy1()
        {
            QueueOwner model = new QueueOwner();
            ViewBag.Queue = new SelectList(HomeRepository.getAllQueueName(), "AgentName", "AgentName");
            ViewBag.Agents = new SelectList(HomeRepository.getAllAgent(), "AgentName", "AgentName");
            return View(model);
        }
    }
}