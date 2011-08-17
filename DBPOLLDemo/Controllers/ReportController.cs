using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
using DBPOLL.Models;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult SystemUtilisationReport()
        {
            return View(new userModel().displayAllUsers());
        }

        public ActionResult StatisticalReport()
        {
            return View(new questionModel().displayAllQuestions());
        }

        public ActionResult SessionHistoryReport()
        {
            return View(new pollModel().displayAllPolls());
        }

        public ActionResult testReport()
        {
            return View();
        }
    }
}
