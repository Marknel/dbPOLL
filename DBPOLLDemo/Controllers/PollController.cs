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
    public class PollAndSessionData
    {
        public List<pollModel> pollData { get; set; }
        public List<pollModel> sessionData { get; set; }
    }

    public class Assign_PollMasters
    {
        public List<userModel> assigned { get; set; }
        public List<userModel> unassigned { get; set; }
    }

    public class PollSession
    {
        public pollModel pollData { get; set; }
        public questionModel questionData { get; set; }
        public answerModel answerData { get; set; }
    }


    public class PollController : Controller
    {
        private DBPOLLEntities db = new DBPOLLEntities(); // ADO.NET data Context.
        //
        // GET: /Main/

        public ActionResult Index()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PollAndSessionData pollSession = new PollAndSessionData();

            pollSession.pollData = new pollModel().displayPolls();
            pollSession.sessionData = new pollModel().displayPollSessions();
            

            return View(pollSession);
        }

        public ActionResult viewPolls()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            return View(new pollModel().displayPolls());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult viewPolls(String date1, String date2)
        {
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

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

            return RedirectToAction("Index", "Poll");
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

        // Our handlers for session oprations. Will redirect to session controller
        public ActionResult CreateSession(int pollID, String pollName)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Create", "Session", new { pollID, pollName });
        }

        public ActionResult EditSession(String sessionname, int sessionid, int pollid, decimal longitude, decimal latitude, DateTime time)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Edit", "Session", 
                new { sessionname, sessionid, pollid, longitude, latitude, time });
        }

        public ActionResult DeleteSession(int sessionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Delete", "Session", new {sessionid });
        }

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
        public ActionResult Create(String name, int createdby, Nullable<DateTime> expiresat)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                new pollModel().createPoll(name, createdby, expiresat);

                return RedirectToAction("Index", "Poll");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Main/Edit/5

        public ActionResult Edit(int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["name"] = name;
            ViewData["id"] = id;

            return View();
        }

        //
        // POST: /Main/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int pollid, String pollname, int changed)
        {
            char[] bad = new char[1];
            bad[0] = 'M';

            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            try
            {
                

                new pollModel().updatePoll(pollid, pollname);

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

        public ActionResult AssignPoll(int pollid, String pollname)
        {
            Assign_PollMasters pollMasters = new Assign_PollMasters();

            pollMasters.assigned = new userModel().displayAssignedPollMasterUsers(pollid);
            pollMasters.unassigned = new userModel().displayUnassignedPollMasterUsers(pollid);


            ViewData["pollid"] = pollid;
            ViewData["pollname"] = pollname;
            return View(pollMasters);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AssignPoll(int pollid, int[] selectedObjects, String pollname)
        {
            new pollModel().assignPoll(pollid, selectedObjects);


            Assign_PollMasters pollMasters = new Assign_PollMasters();

            pollMasters.assigned = new userModel().displayAssignedPollMasterUsers(pollid);
            pollMasters.unassigned = new userModel().displayUnassignedPollMasterUsers(pollid);


            ViewData["pollid"] = pollid;
            ViewData["pollname"] = pollname;
            return View(pollMasters);
        }


        public ActionResult UnassignPollUser(int pollid, String pollname, int userid)
        {
            new pollModel().unassignPollMaster(pollid, userid);

            return RedirectToAction("AssignPoll", "Poll", new {pollid = pollid, pollname = pollname });
        }
    }
}
