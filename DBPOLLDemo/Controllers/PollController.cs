using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DBPOLLDemo.Models;
using System.Threading;
using System.Globalization;

namespace DBPOLLDemo.Controllers
{
    public class PollController : Controller
    {
        private DBPOLLEntities db = new DBPOLLEntities(); // ADO.NET data Context.
        //
        // GET: /Main/

        public ActionResult Index()
        {
            //return View(pollModel.displayPolls(user));
            //return View(db.POLLs.ToList());
            //return View(new pollModel().displayPolls(user));

            //pollModel p = new pollModel(356672, "advdav", (decimal)76.54, (decimal)2.54, 1, DateTime.Now);
            //p.createPoll();

            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

                return View(new pollModel().displayPolls());
        }
        
        public ActionResult viewPolls()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new pollModel().displayPolls());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult viewPolls(String date1, String date2)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            bool valid = true;
            DateTime startdate;
            DateTime enddate; 

            if (!DateTime.TryParse(date1, out startdate))
            {

                if (date1 == "" || date1 == null)
                {
                    ViewData["date1"] = "Above field must contain a date";
                }
                else
                {
                    ViewData["date1"] = "Please Enter a correct Date";
                }
                valid = false;

            }

            if (!DateTime.TryParse(date2, out enddate))
            {
                
                if (date1 == "" || date1 == null)
                {
                    ViewData["date2"] = "Above field must contain a date";
                }
                else
                {
                    ViewData["date2"] = "Please Enter a correct Date";
                }
                valid = false;
                
            }

            if (valid == false)
            {
                return View(new pollModel().displayPolls());
            }

            if (startdate > DateTime.Now)
            {
                ViewData["date1"] = "Date incorrectly in the future.";
                valid = false;
            }

            if (enddate > DateTime.Now)
            {
                ViewData["date2"] = "Date incorrectly in the future.";
                valid = false;
            }

            if (enddate < startdate)
            {
                ViewData["date2"] = "End date needs to be after start date";
                valid = false;
            }

            if (valid == true)
            {
                return View(new pollModel().displayPolls(startdate, enddate));
            }
            else
            {
                return View(new pollModel().displayPolls());
            }
        }

        public ActionResult Delete(int pollid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            pollModel poll = new pollModel(pollid);
            poll.deletePoll();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Main/pollDetails/5
        public ActionResult Details(int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Question", new { id, name });
        }


        //
        // GET: /Main/answerDetails/5

        public ActionResult answerDetails(int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            ViewData["name"] = name;

                return RedirectToAction("Index", "Answer", new {id, name});
        }

        //
        // GET: /Main/Create

        public ActionResult Create()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        } 

        //
        // POST: /Main/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(String name, int latitude, int longitude, int createdby, Nullable<DateTime> expiresat)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                new pollModel().createPoll(name, longitude, latitude, createdby, expiresat);

                return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Main/Edit/5

        public ActionResult Edit(int id, String name, float longitude, float latitude, int createdby, DateTime createdat)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            //ViewData["name"] = name;
            ViewData["id"] = id;
            ViewData["longitude"] = longitude;
            ViewData["latitude"] = latitude;
            ViewData["createdby"] = createdby;
            ViewData["createdat"] = createdat;

            return View();
        }

        //
        // POST: /Main/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int pollid, String pollname, decimal longitude, decimal latitude, DateTime expiresat, int test)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            try
            {
                new pollModel().updatePoll(pollid, pollname, longitude, latitude, expiresat);

                return RedirectToAction("Index");

                //return View();
            }
            catch (Exception e)
            {
                ViewData["error1"] = e.Message;
                return View();
            }
        }

        public ActionResult TestDevices()
        {
            return View();
        }
    }
}
