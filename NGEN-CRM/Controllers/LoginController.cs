using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login obj)
        {
            Login obj1 = LoginRepository.isValidUser(obj);
            string msg = obj.StatusMesg;
            if (obj1.isValid)
            {
                Session["UsmID"] = obj1.UsmID;
                Session["UsmCode"] = obj1.UsmCode;
                Session["Usmname"] = obj1.Usmname;
                Session["RomID"] = obj1.RomID;
                Session["CpmIDPtr"] = obj.UnitId;
                return RedirectToAction("Contact", "Home");
            }
            return Content("<script language='javascript' type='text/javascript'>alert('" + msg + "');window.location='/Login/Login'</script>");
        }
        public ActionResult Logout()
        {
            Login log = new Login();
            Session["UsmID"] = null;
            Session["UsmCode"] = null;
            Session["Usmname"] = null;
            Session["RomID"] = null;
            Session["CpmIDPtr"] = null;
            Session["CustomerID"] = null;
            Session["BrmCode"] = null;
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            return RedirectToAction("Logout", "Logout");
        }
    }
}