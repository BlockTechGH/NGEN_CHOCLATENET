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
            obj.UserList = UserRegistrationRepository.getAllUser();
            ViewBag.Roles = new SelectList(UserRegistrationRepository.getAllRole(), "RoleId", "Rolename");
            return View(obj);
        }
        public ActionResult Edit(long UserId) //Get data for Edit
        {
            UserRegistration obj = new UserRegistration();
            obj = UserRegistrationRepository.getByID(UserId);
            obj.UserList = UserRegistrationRepository.getAllUser();
            ViewBag.Roles = new SelectList(UserRegistrationRepository.getAllRole(), "RoleId", "Rolename");
            return View("Create", obj);
        }
        [HttpPost]
        public ActionResult Edit(UserRegistration obj) //Update
        {
            //obj.RomStatusUser = Convert.ToInt64(Session["UsmID"].ToString());
            if (UserRegistrationRepository.UpdateUser(obj) > 0) //Update success
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Updated Successfully');window.location='/UserRegistration/Create'</script>");
            }
            return RedirectToAction("Create");
        }
        public ActionResult Delete(long UserID) //Delete
        {
            long Success = UserRegistrationRepository.DeleteUser(UserID);
            if (Success == 0)
            {//Delete not success
                return Content("<script language='javascript' type='text/javascript'>alert('Reference Exist, Unable to delete');window.location='/UserRegistration/Create'</script>");
                return RedirectToAction("Create");
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Deleteted Succesfully');window.location='/UserRegistration/Create'</script>");
                return RedirectToAction("Create");
            }
            return RedirectToAction("Create");
        }
        [HttpPost]
        public ActionResult Create(UserRegistration obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //obj.LoginUserID = (Session["UsmID"].ToString());
                   
                   long  t = UserRegistrationRepository.SaveUser(obj);
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
      
        public bool isUserNameExist(string Username, string InitialLoginId)
        {
            if (Username == InitialLoginId)
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