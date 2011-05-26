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
        public ActionResult Index()
        {
            userModel barry = new userModel("John", "John");
            bool result = barry.verify();


            ViewData["Message"] = "Welcome to AS2P.NET MVC! " + result.ToString() + Session["uid"] + " BLAH";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

            
    }
}
