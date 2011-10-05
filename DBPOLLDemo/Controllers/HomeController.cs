using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBPOLLDemo.Models;
using System.Security.Cryptography;
using System.Web.Security;

namespace DBPOLLDemo.Controllers
{
    public class HomeController : Controller
    {
        
        private DBPOLLEntities db = new DBPOLLEntities(); // ADO.NET data Context.

        public ActionResult Index()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Logon", "Home");
            }
            userModel user = new userModel();
            if (Session["sysadmin"].Equals("false"))
            {
                var userDetails = user.get_details((int)Session["uid"]);
                ViewData["Message"] = "Welcome " + userDetails.NAME;
                ViewData["User"] = userDetails;
            }
            else
            {
                var userDetails = user.get_sys_admin_details((int)Session["uid"]);
                ViewData["Message"] = "Welcome " + userDetails.NAME;
                ViewData["User"] = userDetails;
            }
            //ViewData["sysadmin"] = Session["sysadmin"];
            return View();
        }

        public ActionResult Logon()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Logon(String username, String password)
        {
            userModel user = new userModel();
            var authenticated = user.verify(username, password);
            var type = user.getUserType(authenticated);
            if (authenticated != 0)
            {
                user = user.getUser(authenticated);
                Session["user_type"] = type;
                Session["sysadmin"] = "false";
                if (user.Reset_Password_Key != null && user.Reset_Password_Key.Equals("Created"))
                {
                    Session["Created"] = authenticated;
                    return RedirectToAction("ChangePassword", "User");
                }
                Session["uid"] = authenticated;



                return RedirectToAction("Index", "Home");
            }
            else
            {
                authenticated = user.verify_as_sys_admin(username, password);
                if (authenticated != 0)
                {
                    Session["uid"] = authenticated;
                    Session["user_type"] = type;
                    Session["sysadmin"] = "true";
                    return RedirectToAction("Index", "SysAdmin");
                }
                else
                {
                    ViewData["Message"] = "Username or password was incorrect";
                    return View();
                }
            }
        }

        public void LogOff()
        {
            Session["uid"] = null;
            Session["user_type"] = null;
            Session["sysadmin"] = null;
            Response.Redirect("Index");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResetPassword(string email)
        {
            int uid;
            userModel user = new userModel();
            //            if (email == null || System.Text.RegularExpressions.Regex.IsMatch(email, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=
            //                [0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
            //            {
            //                ViewData["emailError"] = "Above field must contain a valid email address!";
            //                error = true;
            //            }

            uid = user.verify(email);
            if (uid == 0)
            {
                ViewData["outcome"] = "No account with this email address was found";
                return View();
            }

            //generate new password
            string newPassword = user.Password_Generator();
            //store new password in db
            user.changePassword(uid, newPassword);

            //send new password in email
            EmailController mail = new EmailController(email, newPassword, email);

            string mailSuccess = mail.send();
            if (!mailSuccess.Equals("Email sent successfully"))
            {
                ViewData["outcome"] = "An error occurred whilst trying to reset your password, please try again in a few moments or contact your System Administrator.";
            }
            else
                ViewData["outcome"] = "Password successfully reset! Please check your email for your new password";
            ViewData["emailError"] = mailSuccess;

            return View();
        }
    }
}