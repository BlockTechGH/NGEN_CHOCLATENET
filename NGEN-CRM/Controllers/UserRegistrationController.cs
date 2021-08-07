using NGEN_CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGEN_CRM.Controllers
{
    public class UserRegistrationController : Controller
    {
        // GET: UserRegistration
        public ActionResult Create()
        {
            UserRegistration obj = new UserRegistration();
            ViewBag.Roles = new SelectList(UserRegistrationRepository.getAllRole(), "RoleId", "Rolename");
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(UserRegistration obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //obj.LoginUserID = (Session["UsmID"].ToString());
                   
                   int  t = UserRegistrationRepository.SaveUser(obj);
                    if (t > 0) //Save Success
                    {
                        string messege = "User Succesfully Registered";
                        if (messege != "")
                        {
                            return Content("<script language='javascript' type='text/javascript'>alert('" + messege + "');window.location='/UserRegistration/Create'</script>");
                        }
                        //else { return Content("<script language='javascript' type='text/javascript'>alert('Saved Successfully  Your Default Password is " + Password + " Please Login to Change the Password');window.location='/User/Create'</script>"); }
                    }
                    //return RedirectToAction("Error404", "Home");//Save Failed
                }
                catch(Exception ex)
                {
                    throw ex;
                    return RedirectToAction("Error404", "Home"); } //If Error
            }
            return RedirectToAction("Create");
        }
        //public bool isUserCodeExist(string UserCode, string InitialUserCode)
        //{
        //    UserCode = CLSCommon.getCompanyPrefix(2) + UserCode;
        //    if (UserCode == InitialUserCode)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        if (!CLSCommon.isCodeExist(UserCode, "UsmCode", "UsersMaster"))
        //        {
        //            return true; // Role Code is Available
        //        }
        //        return false;
        //    }
        //}
        public bool isUserNameExist(string Username, string InitialUsername)
        {
            if (Username == InitialUsername)
            {
                return true;
            }
            else
            {
                if (!CLSCommen.isCodeExist(Username))
                {
                    return true; // Role Code is Available
                }
                return false;
            }
        }
        //public ActionResult changePassword()
        //{
        //    User obj = new User();
        //    return View("ChangePassword", obj);
        //}
        //[HttpPost]
        //public ActionResult changePassword(User obj)
        //{
        //    if (obj.NewPswd != obj.ReEnterPswd)
        //        return Content("<script language='javascript' type='text/javascript'>alert('Re-entered Password Mismatch,Try Again');window.location='/User/changePassword'</script>");
        //    if (obj.NewPswd == obj.UsmPassword)
        //        return Content("<script language='javascript' type='text/javascript'>alert('New Password is same as Oldpassword,Try Again');window.location='/User/changePassword'</script>");

        //    if (Session["UsmID"] == null || Session["UsmID"].ToString() == "") return RedirectToAction("Logout", "Logout");
        //    //string UsmCode = Session["UsmID"].ToString();
        //    long UsmCode = UserRepository.GetUserID(obj.Usmname);
        //    //if (UserRepository.ChangePassword(obj.UsmPassword, obj.LoginUserID, UsmCode).)
        //    //    return Content("<script language='javascript' type='text/javascript'>alert('Your Password is Changed Successfully');window.location='/Home/Dashboard'</script>");
        //    //else
        //    //    return Content("<script language='javascript' type='text/javascript'>alert('Old Password is Wrong,Try Again');window.location='/User/changePassword'</script>");

        //    DataTable dt = UserRepository.ChangePassword(obj.NewPswd, obj.Usmname, UsmCode.ToString());
        //    if (dt.Rows[0]["StatusMessage"].ToString() == "Password Updated Sucessfully")
        //    {
        //        return Content("<script language='javascript' type='text/javascript'>alert('Your Password is Changed Successfully');window.location='/Home/index'</script>");

        //    }
        //    else
        //        return Content("<script language='javascript' type='text/javascript'>alert('Invalid loginid ,Try Again');window.location='/User/changePassword'</script>");



        //}
    }
}