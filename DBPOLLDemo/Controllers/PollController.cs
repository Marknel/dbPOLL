using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using DBPOLLDemo.Models;


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


        public ActionResult Index()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }

            PollAndSessionData pollSession = new PollAndSessionData();

            pollSession.pollData = new pollModel().displayPolls();
            pollSession.sessionData = new pollModel().displayPollSessions();
            
            return View(pollSession);
        }

        public ActionResult viewPolls()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
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
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }

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
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            pollModel poll = new pollModel(pollid);
            poll.deletePoll();

            return RedirectToAction("Index", "Poll");
        }

        //
        // GET: /Main/pollDetails/5
        public ActionResult Details(int id, String name)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }
            return RedirectToAction("Index", "Question", new { id, name });
        }

        // Our handlers for session oprations. Will redirect to session controller
        public ActionResult CreateSession(int pollID, String pollName)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }

            return RedirectToAction("Create", "Session", new { pollID, pollName });
        }

        public ActionResult EditSession(String sessionname, int sessionid, int pollid, decimal longitude, decimal latitude, DateTime time)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }

            return RedirectToAction("Edit", "Session", 
                new { sessionname, sessionid, pollid, longitude, latitude, time });
        }

        public ActionResult DeleteSession(int sessionid)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }

            return RedirectToAction("Delete", "Session", new {sessionid });
        }

        public ActionResult answerDetails(int id, String name)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }


            ViewData["name"] = name;

                return RedirectToAction("Index", "Answer", new {id, name});
        }

        //
        // GET: /Main/Create

        public ActionResult Create()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            return View();
        } 


        //
        // POST: /Main/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(String name, int createdby, Nullable<DateTime> expiresat)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
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
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
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

            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
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
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }


            return View();
        }

        public ActionResult RunDevices()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }

            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }

            return View();
        }

        public ActionResult AssignPollCreator(int pollid, String pollname)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }

            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            Assign_PollMasters pollMasters = new Assign_PollMasters();

            pollMasters.assigned = new userModel().displayAssignedUsers(pollid, User_Type.POLL_CREATOR);
            pollMasters.unassigned = new userModel().displayUnassignedUsers(pollid, User_Type.POLL_CREATOR);

            ViewData["pollid"] = pollid;
            ViewData["pollname"] = pollname;

            return View(pollMasters);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AssignPollCreator(int pollid, int[] selectedObjects, String pollname)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            String errorString = "";

            new pollModel().assignPoll(pollid, selectedObjects);

            Assign_PollMasters pollMasters = new Assign_PollMasters();

            pollMasters.assigned = new userModel().displayAssignedUsers(pollid, User_Type.POLL_CREATOR);
            pollMasters.unassigned = new userModel().displayUnassignedUsers(pollid, User_Type.POLL_CREATOR);

                foreach (int id in selectedObjects)
                {
                    userModel u = new userModel();
                    u = u.getUser(id);
                    EmailController mail = new EmailController(pollname, u.username);

                    string mailSuccess = mail.send1();
                    if (!mailSuccess.Equals("Email sent successfully"))
                    {
                        errorString += u.username + "\n";
                        //throw new Exception(mailSuccess);
                    }
                }

            if(errorString.Length != 0)
                ViewData["emailError"] = "Could not send email to following Users: \n" + errorString;


            ViewData["pollid"] = pollid;
            ViewData["pollname"] = pollname;
            return View(pollMasters);
        }

        public ActionResult AssignPollMaster(int pollid, String pollname)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }

            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            Assign_PollMasters pollMasters = new Assign_PollMasters();

            pollMasters.assigned = new userModel().displayAssignedUsers(pollid, User_Type.POLL_MASTER);
            pollMasters.unassigned = new userModel().displayUnassignedUsers(pollid, User_Type.POLL_MASTER);

            ViewData["pollid"] = pollid;
            ViewData["pollname"] = pollname;


            return View(pollMasters);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AssignPollMaster(int pollid, int[] selectedObjects, String pollname)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }



            new pollModel().assignPoll(pollid, selectedObjects);

            String errorString = "";
            Assign_PollMasters pollMasters = new Assign_PollMasters();

            pollMasters.assigned = new userModel().displayAssignedUsers(pollid, User_Type.POLL_MASTER);
            pollMasters.unassigned = new userModel().displayUnassignedUsers(pollid, User_Type.POLL_MASTER);

            foreach (int id in selectedObjects)
            {
                userModel u = new userModel();
                u = u.getUser(id);
                EmailController mail = new EmailController(pollname, u.username);
                
                string mailSuccess = mail.send1();
                if (!mailSuccess.Equals("Email sent successfully"))
                {
                    errorString += u.username + "\n";
                    //throw new Exception(mailSuccess);
                }
            }

            if (errorString.Length != 0)
                ViewData["emailError"] = "Could not send email to following Users: \n" + errorString;

            ViewData["pollid"] = pollid;
            ViewData["pollname"] = pollname;
            return View(pollMasters);
        }

        public ActionResult ViewObjects(int pollid, String pollname)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }
            return RedirectToAction("Index", "PollObject", new { pollid, pollname });
        }


        public ActionResult UnassignPollUser(int pollid, String pollname, int userid)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }
            new pollModel().unassignPoll(pollid, userid);

            return RedirectToAction("AssignPoll", "Poll", new {pollid = pollid, pollname = pollname });
        }
    }
}
