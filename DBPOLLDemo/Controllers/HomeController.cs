using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBPOLL.Models;
using DBPOLLContext;

namespace DBPOLLDemo.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private DBPOLLDataContext db = new DBPOLLDataContext();
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(String username, String password)
        {

            userModel user = new userModel(username, password);

            if (user.verify() == true)
            {
                return RedirectToAction("Index", "Main");
            }
            else
            {
                ViewData["Message"] = "Username / password is incorrect";
                return View();  
            }
           
            //return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

            
    }
}
