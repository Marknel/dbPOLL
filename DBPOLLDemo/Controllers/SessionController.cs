using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class PollAndQuestions
    {
        public List<pollModel> sessionData { get; set; }
        public questionModel questionData { get; set; }
        public List<List<answerModel>> answerData { get; set; }
     
    }

    public class SessionController : Controller
    {
        //
        // GET: /Session/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Session/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Session/Create

        public ActionResult Create(int pollID, String pollName)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["pollID"] = pollID;
            ViewData["pollName"] = pollName;

            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(int pollID, String pollName, String name, decimal latitude, decimal longitude, String time, String longitudeBox, String latitudeBox)
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

            bool valid = true;
            DateTime parsedDate;
            Decimal parsedLongitude = longitude;
            Decimal parsedLatitude = latitude;

            IEnumerable<int> longRange = Enumerable.Range(-180, 360);
            IEnumerable<int> latRange = Enumerable.Range(-90, 180);


            if (!(longitudeBox.Equals("") && latitudeBox.Equals("")))
            {
                if (!Decimal.TryParse(latitudeBox, out parsedLatitude))
                {
                    ViewData["latBox"] = "field is not a valid latitude";
                    valid = false;
                }
                else if (!latRange.Contains((int)parsedLatitude))
                {
                    ViewData["latBox"] = "Latitude is not between -90" + (char)176 + " and 90" + (char)176;
                    valid = false;
                }
                if (!Decimal.TryParse(longitudeBox, out parsedLongitude))
                {
                    ViewData["longBox"] = "field is not a valid longitude";
                    valid = false;
                }
                else if (!longRange.Contains((int)parsedLongitude))
                {
                    ViewData["longBox"] = "Longitude is not between -180" + (char)176 + " and 180" + (char)176;
                    valid = false;
                }
            }

            if (!DateTime.TryParse(time, out parsedDate))
            {

                if (time == "" || time == null)
                {
                    ViewData["date1"] = "Above field must contain a date";
                }
                else
                {
                    ViewData["date1"] = "Please Enter a correct Date";
                }
                valid = false;
            }

            if (parsedDate < DateTime.Now)
            {
                ViewData["date1"] = "Date incorrectly in the past.";
                valid = false;
            }

            if (valid == false)
            {
                ViewData["pollID"] = pollID;
                ViewData["pollName"] = pollName;
                return View();
            }
            

            if (valid == true)
            {
                try
                {
                    new pollModel().createSession(pollID, name, parsedLatitude, parsedLongitude, parsedDate);
                    return RedirectToAction("Index", "Poll", new { pollID = pollID, pollName = pollName });
                }
                catch(Exception e)
                {
                    ViewData["pollID"] = pollID;
                    ViewData["pollName"] = pollName +"ERROR: "+e.Message;
                    return View();
                }
            }
            ViewData["pollID"] = pollID;
            ViewData["pollName"] = pollName;
            return View();

        }
        
        //
        // GET: /Session/Edit/5


        public ActionResult Edit(String sessionname, int sessionid, int pollid, decimal longitude, decimal latitude, DateTime time)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["name"] = sessionname;
            ViewData["sessionid"] = sessionid;
            ViewData["pollid"] = pollid;
            ViewData["longitude"] = longitude;
            ViewData["latitude"] = latitude;
            ViewData["time"] = time;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(String sessionname, int sessionid, int pollid, decimal latitude, decimal longitude, String time, String longitudeBox, String latitudeBox)
        {

            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            bool valid = true;
            DateTime parsedDate;

            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            Decimal parsedLongitude = longitude;
            Decimal parsedLatitude = latitude;

            IEnumerable<int> longRange = Enumerable.Range(-180, 360);
            IEnumerable<int> latRange = Enumerable.Range(-90, 180);


            if (!(longitudeBox.Equals("") && latitudeBox.Equals("")))
            {
                if (!Decimal.TryParse(latitudeBox, out parsedLatitude))
                {
                    ViewData["latBox"] = "field is not a valid latitude";
                    valid = false;
                }
                else if (!latRange.Contains((int)parsedLatitude))
                {
                    ViewData["latBox"] = "Latitude is not between -90" + (char)176 + " and 90" + (char)176;
                    valid = false;
                }
                if (!Decimal.TryParse(longitudeBox, out parsedLongitude))
                {
                    ViewData["longBox"] = "field is not a valid longitude";
                    valid = false;
                }
                else if (!longRange.Contains((int)parsedLongitude))
                {
                    ViewData["longBox"] = "Longitude is not between -180" + (char)176 + " and 180" + (char)176;
                    valid = false;
                }
            }

            if (!DateTime.TryParse(time, out parsedDate))
            {

                if (time == "" || time == null)
                {
                    ViewData["date1"] = "Above field must contain a date";
                }
                else
                {
                    ViewData["date1"] = "Please Enter a correct Date";
                }
                valid = false;
            }

            if (parsedDate < DateTime.Now)
            {
                ViewData["date1"] = "Date incorrectly in the past.";
                valid = false;
            }

            if (valid == false)
            {

                ViewData["name"] = sessionname;
                ViewData["sessionid"] = sessionid;
                ViewData["pollid"] = pollid;
                ViewData["longitude"] = longitude;
                ViewData["latitude"] = latitude;
                ViewData["time"] = time;

                return View();
            }

            

            if (valid == true)
            {

                try
                {
                    new pollModel().editSession(sessionname, sessionid, parsedLatitude, parsedLongitude, parsedDate);
                    return RedirectToAction("Index", "Poll");
                }
                catch
                {
                    ViewData["name"] = "Could not Edit Session. Please try again";
                    ViewData["sessionid"] = sessionid;
                    ViewData["pollid"] = pollid;
                    ViewData["longitude"] = longitude;
                    ViewData["latitude"] = latitude;
                    ViewData["time"] = time;

                    return View();
                }


            }

            ViewData["name"] = sessionname;
            ViewData["sessionid"] = sessionid;
            ViewData["pollid"] = pollid;
            ViewData["longitude"] = longitude;
            ViewData["latitude"] = latitude;
            ViewData["time"] = time;

            return View();
        }

        
        //
        // GET: /Session/Delete/5

        public ActionResult Delete(int sessionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            pollModel poll = new pollModel(sessionid, 1);
            poll.deleteSession();

            return RedirectToAction("Index", "Poll");
        }

        public ActionResult ViewAvailableSession(int userid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //pollModel poll = new pollModel(sessionid, 1);
            //poll.deleteSession();

            return View(new pollModel().displayAssignedSessions(userid));
            
        }

        public ActionResult StartSession(int sessionid, int pollid)
        {
            int questnum = 0;

            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (Session["currentwebpollingQuestion"] == null)
            {
                Session["currentwebpollingQuestion"] = 0;
                questnum = (int)Session["currentwebpollingQuestion"];
            }
            else {
                questnum = (int)Session["currentwebpollingQuestion"];
            }

            Session["endOfQuestion"] = false;

            PollAndQuestions pollAndQuestionModel = new PollAndQuestions();
            pollAndQuestionModel.sessionData = new pollModel().displaySessionDetails(sessionid);
 
            List<questionModel> tempList = new questionModel().displayQuestionsFromAPoll(pollid);

            Session["currentWebpollingSessionid"] = sessionid;
            Session["currentWebpollingPollid"] = pollid;


            
            pollAndQuestionModel.questionData = new questionModel().getQuestion(tempList[questnum].questionid);
            Session["currentQuestionId"] = pollAndQuestionModel.questionData.questionid;
            //questionModel tempQuestionModel = tempList[questnum+1];
            if (tempList.Count() == questnum+1)
            {
                Session["endOfQuestion"] = true;
            }
            Session["currentwebpollingQuestion"] = questnum;

            List<answerModel> unsorted = new answerModel().getPollAnswers(pollid);
            List<List<answerModel>> sorted = new List<List<answerModel>>();
            
            List<int> questionCheck = new List<int>();

                foreach (var answer in unsorted)
                {
                    if (pollAndQuestionModel.questionData.questionid == answer.questionid && !questionCheck.Contains(pollAndQuestionModel.questionData.questionid))
                    {
                        sorted.Add(new answerModel().displayAnswers(pollAndQuestionModel.questionData.questionid));
                        questionCheck.Add(pollAndQuestionModel.questionData.questionid);
                    }
                }
            pollAndQuestionModel.answerData = sorted;
            
            
            return View(pollAndQuestionModel);
        }

        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StartSession(String button)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PollAndQuestions pollAndQuestionModel = new PollAndQuestions();
            int sessionid = (int)Session["currentWebpollingSessionid"];
            int pollid = (int)Session["currentWebpollingPollid"];
            int questnum = (int)Session["currentwebpollingQuestion"];
            Session["selectedAnswer"] = 0;

            // Get user answer from radio button

            if (Request["UserAnswer"] !=  null)
            {
                int selectedAnswer = Convert.ToInt32(Request["UserAnswer"]);
            
                List<questionModel> answeredQuestions = new questionModel().AnsweredQuestions(sessionid, (int)Session["uid"]);

                questionModel answeredQuestion = new questionModel();

                foreach (var item in answeredQuestions)
                {
                    if (item.questionid == (int)Session["currentQuestionId"])
                    {
                        answeredQuestion = item;
                    }
                }

                // If a question has been answered by this user before, then create a new response data
                if (answeredQuestion.question != null )
                {
                    int responseId = new responseModel().getResponseId(sessionid, (int)Session["uid"], answeredQuestion.answer);
                    try
                    {
                        new responseModel().updateResponse(responseId, selectedAnswer);
                    }
                    catch (Exception e)
                    {
                        throw (e);
                    }
                }
                // else just update the response for this answer
                else 
                {
                    Session["selectedAnswer"] = answeredQuestion.answernum;
                    try
                    {
                        new responseModel().createResponse((int)Session["uid"], selectedAnswer, sessionid);
                    }
                    catch (Exception e)
                    {
                        throw (e);
                    }
                }

                List<questionModel> tempList = new questionModel().displayQuestionsFromAPoll(pollid);
                if (button == "Previous Question")
                {
                
                    Session["currentwebpollingQuestion"] = questnum-1;
                }

                else
                {
                    // TODO check if its end of the question, else line below will throw an error when index outta bound
                    Session["currentwebpollingQuestion"] = questnum+1;
                }
            }
             
            return RedirectToAction("StartSession", new { sessionid = sessionid, pollid = pollid});
        }

        //public Boolean isAnswered(int questionid, int sessionid, int userid, int pollid)
        //{
        //    List<questionModel> questionList = new questionModel().displayQuestionsFromAPoll(pollid);
        //    return false;
        //}

        private void buildAnswerRadioButton(String[] answers)
        {

            userModel user = new userModel();
            var userDetails = user.get_details((int)Session["uid"]);
            ViewData["User"] = userDetails;

            List<SelectListItem> ListItems = new List<SelectListItem>();
            ListItems.Add(new SelectListItem
            {
                Text = "Poll User",
                Value = "1"
            });
            ListItems.Add(new SelectListItem
            {
                Text = "Poll Master",
                Value = "2",
                Selected = true
            });
            ListItems.Add(new SelectListItem
            {
                Text = "Poll Creator",
                Value = "3"
            });
            ListItems.Add(new SelectListItem
            {
                Text = "Poll Administrator",
                Value = "4"
            });
            ViewData["USER_TYPE"] = ListItems;
        }
    }
}
