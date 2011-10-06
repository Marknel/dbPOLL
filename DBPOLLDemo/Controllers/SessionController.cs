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

            if (Session["currentQuestionNumber"] == null || (int)Session["currentWebpollingSessionid"] != sessionid)
            {
                Session["currentQuestionNumber"] = 0;
                questnum = (int)Session["currentQuestionNumber"];
            }
            else 
            {
                questnum = (int)Session["currentQuestionNumber"];
            }

            if (TempData["webpollingError"] != null)
            {
                String[] error = TempData["webpollingError"].ToString().Split(',');
                ModelState.AddModelError(error[0], error[1]);
            }

            
            PollAndQuestions pollAndQuestionModel = new PollAndQuestions();
            pollAndQuestionModel.sessionData = new pollModel().displaySessionDetails(sessionid);
 
            List<questionModel> tempList = new questionModel().displayQuestionsFromAPoll(pollid);

            Session["currentWebpollingSessionid"] = sessionid;
            Session["currentWebpollingPollid"] = pollid;
            
            pollAndQuestionModel.questionData = new questionModel().getQuestion(tempList[questnum].questionid);
            Session["AllQuestion"] = tempList;
            

            //if its the last question, then let the view know so that the next button could be replaced with submit last answer
            Session["endOfQuestion"] = false;
            if (tempList.Count() == questnum+1)
            {
                Session["endOfQuestion"] = true;
            }
            Session["currentQuestionNumber"] = questnum;


            List<answerModel> unsorted = new answerModel().getPollAnswers(pollid);
            List<List<answerModel>> sorted = new List<List<answerModel>>();
            List<int> questionCheck = new List<int>();
            List<questionModel> answeredQuestions = new questionModel().GetAnsweredMCQQuestions(sessionid, (int)Session["uid"]);

            // Get a set of answer list for this question
            foreach (var answer in unsorted)
            {
                if (pollAndQuestionModel.questionData.questionid == answer.questionid && !questionCheck.Contains(pollAndQuestionModel.questionData.questionid))
                {
                    sorted.Add(new answerModel().displayAnswers(pollAndQuestionModel.questionData.questionid));
                    questionCheck.Add(pollAndQuestionModel.questionData.questionid);
                }
            }
            pollAndQuestionModel.answerData = sorted;


            // To set the first question's radio button to user's previous answer if he's answered it before
            foreach (var answeredquestion in answeredQuestions)
            {
                foreach (var answer in sorted)
                {
                    foreach (var a in answer) { 
                        if (answeredquestion.answer == a.answer)
                        {
                            Session["selectedAnswer"] = answeredquestion.answer;
                        }

                    }
                }
            }
            

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
            int questnum = (int)Session["currentQuestionNumber"];
            

            List<questionModel> allquestion = (List<questionModel>)Session["AllQuestion"];
            int currentquestion = allquestion[questnum].questionid;


            Session["selectedAnswer"] = "";


            // if the user is currently answering a MCQ type
            if (Request["UserAnswer"] != null )
            {
                int selectedAnswer = Convert.ToInt32(Request["UserAnswer"]);
                AnswerMultipleChoiceQuestion(selectedAnswer, sessionid, (int)Session["uid"], currentquestion);


                List<questionModel> tempList = new questionModel().displayQuestionsFromAPoll(pollid);
                if (button == "Previous Question")
                {
                    Session["currentQuestionNumber"] = questnum - 1;
                    int nextquestion = allquestion[(int)Session["currentQuestionNumber"]].questionid;
                    setNextAnswer(questnum + 1, nextquestion, sessionid, (int)Session["uid"]);
                }

                // if its the last question, then submit/ update answer but stay on the same question
                else if (button == "Submit Last Answer")
                {
                    Session["currentQuestionNumber"] = questnum;
                    Session["selectedAnswer"] = Request["UserAnswer"];
                }
                // its the next button
                else
                {
                    Session["currentQuestionNumber"] = questnum + 1;
                    int nextquestion = allquestion[(int)Session["currentQuestionNumber"]].questionid;
                    setNextAnswer(questnum - 1, nextquestion, sessionid, (int)Session["uid"]);
                }    

            }

            // if the user is currently answering a short answer question type
            else if (Request["ShortQuestionAnswer"] != "")
            {
                String selectedAnswer = Request["ShortQuestionAnswer"];
                AnswerShortAnswerQuestion(selectedAnswer, sessionid, (int)Session["uid"], currentquestion);

                List<questionModel> tempList = new questionModel().displayQuestionsFromAPoll(pollid);
                if (button == "Previous Question")
                {
                    Session["currentQuestionNumber"] = questnum - 1;
                    int nextquestion = allquestion[(int)Session["currentQuestionNumber"]].questionid;
                    setNextAnswer(questnum + 1, nextquestion, sessionid, (int)Session["uid"]);
                    
                }

                // if its the last question, then submit/ update answer but stay on the same question
                else if (button == "Submit Last Answer")
                {
                    Session["currentQuestionNumber"] = questnum;
                    Session["shortAnswer"] = Request["ShortQuestionAnswer"];
                }
                // its the next button
                else
                {
                    Session["currentQuestionNumber"] = questnum + 1;
                    int nextquestion = allquestion[(int)Session["currentQuestionNumber"]].questionid;
                    setNextAnswer(questnum - 1, nextquestion, sessionid, (int)Session["uid"]);
                }    
            }

            // if the user hasnt answered anything, then display error and ask em to answer it NAO
            else
            {
                String error = "webpollingError" + "," + "Please provide your answer";
                TempData["webpollingError"] = error;
                
            }

            return RedirectToAction("StartSession", new { sessionid = sessionid, pollid = pollid });
        }


        public void AnswerMultipleChoiceQuestion(int selectedAnswer, int sessionid, int userid, int currentquestionid)
        {

            List<questionModel> answeredQuestions = new questionModel().GetAnsweredMCQQuestions(sessionid, userid);

            questionModel answeredQuestion = new questionModel();

            foreach (var item in answeredQuestions)
            {
                if (item.questionid == currentquestionid)
                {
                    answeredQuestion = item;
                }
            }

            // If a question has been answered by this user before, then create a new response data
            if (answeredQuestion.question != null)
            {
                int responseId = new responseModel().getResponseId(sessionid, userid, answeredQuestion.answer);

                try
                {
                    new responseModel().updateMCQResponse(responseId, selectedAnswer);
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
            // else just update the response for this answer
            else
            {
                try
                {
                    new responseModel().createMCQResponse(userid, selectedAnswer, sessionid);
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }

        }

        public void AnswerShortAnswerQuestion(String answer, int sessionid, int userid, int currentquestionid)
        {

            List<questionModel> answeredQuestions = new questionModel().GetAnsweredShortAnswerQuestions(currentquestionid, userid);

            questionModel answeredQuestion = new questionModel();

            foreach (var item in answeredQuestions)
            {
                if (item.questionid == currentquestionid)
                {
                    answeredQuestion = item;
                }
            }

            // If a question has been answered by this user before, then create a new response data
            if (answeredQuestion.question != null)
            {
                int responseId = new responseModel().getResponseId(sessionid, userid, answeredQuestion.answer);

                try
                {
                    new responseModel().updateShortAnswerResponse(responseId, answer);
                    
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
            // else just update the response for this answer
            else
            {
                try
                {
                    new responseModel().createShortAnswerResponse(answer, userid, sessionid, currentquestionid);
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }

        }

        public void setNextAnswer(int currentQuestionId, int nextQuestionId, int sessionid, int userid)
        {

            List<questionModel> answeredQuestions = new questionModel().GetAnsweredMCQQuestions(sessionid, userid);
            List<questionModel> answeredQuestions2 = new questionModel().GetAnsweredShortAnswerQuestions(currentQuestionId, userid);

            foreach (var answeredquestion in answeredQuestions)
            {
                if (answeredquestion.questionid == nextQuestionId)
                {
                    Session["selectedAnswer"] = answeredquestion.answer;
                }
            }

            foreach (var answeredquestion in answeredQuestions2)
            {
                if (answeredquestion.questionid == nextQuestionId)
                {
                    Session["shortAnswer"] = answeredquestion.answer;
                }
            }

        }


    }
}
