using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DBPOLL.Models;
using DBPOLLContext;
using DBPOLLDemo.Models;
using System.Threading;
using System.Globalization;

namespace DBPOLLDemo.Controllers
{
    public class MainController : Controller
    {
        private DBPOLLDataContext db = new DBPOLLDataContext();
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
            if (startdate > DateTime.Now)
            {
                ViewData["date1"] = "Date incorrectly in the future.";
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
            if (enddate > DateTime.Now)
            {
                ViewData["date2"] = "Date incorrectly in the future.";
                valid = false;
            }

            if (enddate > startdate)
            {
                ViewData["date2"] = "Date needs to be after start date";
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
        /*public ActionResult viewPolls()
        {
            return View(new pollModel().displayPolls());
        }*/

        public ActionResult Delete(int pollid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            pollModel poll = new pollModel(pollid);
            poll.deletePoll();

            return RedirectToAction("Index", "Main");
        }

        //
        // GET: /Main/pollDetails/5
        public ActionResult Details(int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("../Question/Index/" + id.ToString() + "?name=" + name);
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

                //return View(new answerModel().displayAnswers(id));
                return RedirectToAction("../Answer/Index/" + id.ToString() + "?name=" + name);
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
        public ActionResult Create(FormCollection collection)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
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
        public ActionResult Edit(int pollid, String pollname, float longitude, float latitude, int createdby, DateTime createdat, DateTime expiresat, DateTime modifiedat, int test)
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
                //pollModel poll = new pollModel(pollid, pollname, longitude, latitude, createdby, expiresat, createdat, modifiedat);
                pollModel poll = new pollModel(pollid, pollname);
                poll.updatePoll();

                return RedirectToAction("Index");

                //return View();
            }
            catch (Exception e)
            {
                ViewData["error1"] = e.Message;
                return View();
            }
        }
    }
}
