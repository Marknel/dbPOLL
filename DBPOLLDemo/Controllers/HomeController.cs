﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private DBPOLLEntities db = new DBPOLLEntities(); // ADO.NET data Context.
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(String username, String password)
        {
            userModel user = new userModel();
            var authenticated = user.verify(username, password);
            if ( authenticated != 0 ) {
                Session["uid"] = authenticated;
                Session["sysadmin"] = "false";
                return RedirectToAction("Home", "Home");
            } else {
                authenticated = user.verify_as_sys_admin(username, password);
                if (authenticated != 0)
                {
                    Session["uid"] = authenticated;
                    Session["sysadmin"] = "true";
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    ViewData["Message"] = "Username or password was incorrect";
                    return View();
                }
            }
        }

        public ActionResult About()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            if (Session["uid"] == null) {
                return RedirectToAction("Index", "Home");
            }
            userModel user = new userModel();
            if (Session["sysadmin"] == "false")
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
            ViewData["sysadmin"] = Session["sysadmin"];

            return View();
        }

        public ActionResult LogOff()
        {
            Session["uid"] = null;
            return RedirectToAction("Index", "Home");
        }

            
    }
}
