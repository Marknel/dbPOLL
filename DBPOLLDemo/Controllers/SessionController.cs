using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DBPOLLDemo.Models;
using System.Text.RegularExpressions;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace DBPOLLDemo.Controllers
{
    public class PollAndQuestions
    {
        public List<pollModel> sessionData { get; set; }
        public questionModel questionData { get; set; }
        public List<answerModel> answerData { get; set; }
        public List<answerModel> responseData { get; set; }
    }

    public class PollAnswerAndSession
    {
        public List<pollModel> sessionData { get; set; }
        public List<questionModel> questionData { get; set; }
        public List<List<answerModel>> answerData { get; set; }
        public List<List<int>> responseData { get; set; }
    }

    //public class SyncAndAsyncSessions
    //{
    //    public List<pollModel> Sync { get; set; }
    //    public List<pollModel> Async { get; set; }
    //}

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
                    //ViewData["latBox"] = "field is not a valid latitude";
                    ModelState.AddModelError("latBox", "field is not a valid latitude");
                    valid = false;
                }
                else if (!latRange.Contains((int)parsedLatitude))
                {
                    //ViewData["latBox"] = "Latitude is not between -90" + (char)176 + " and 90" + (char)176;
                    ModelState.AddModelError("latBox", "Latitude is not between -90" + (char)176 + " and 90" + (char)176);
                    valid = false;
                }
                if (!Decimal.TryParse(longitudeBox, out parsedLongitude))
                {
                    //ViewData["longBox"] = "field is not a valid longitude";
                    ModelState.AddModelError("longBox", "field is not a valid longitude");
                    valid = false;
                }
                else if (!longRange.Contains((int)parsedLongitude))
                {
                    //ViewData["longBox"] = "Longitude is not between -180" + (char)176 + " and 180" + (char)176;
                    ModelState.AddModelError("longBox", "Longitude is not between -180" + (char)176 + " and 180" + (char)176);
                    valid = false;
                }
            }

            if (!DateTime.TryParse(time, out parsedDate))
            {

                if (time == "" || time == null)
                {
                    //ViewData["date1"] = "Above field must contain a date";
                    ModelState.AddModelError("date1", "Above field must contain a date");
                }
                else
                {
                    //ViewData["date1"] = "Please Enter a correct Date";
                    ModelState.AddModelError("date1", "Please Enter a correct Date");
                }
                valid = false;
            }

            if (parsedDate < DateTime.Now)
            {
                //ViewData["date1"] = "Date incorrectly in the past.";
                ModelState.AddModelError("date1", "Date incorrectly in the past.");
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

            //List<pollModel> allSessions = new pollModel().displayAssignedSessions(userid);
            //SyncAndAsyncSessions syncAndAsync = new SyncAndAsyncSessions();
            //syncAndAsync.Asy
            Session["showGraph"] = false;
            return View(new pollModel().displayAssignedSessions(userid));
            
        }

        public ActionResult StartAsyncSession(int sessionid, int pollid)
        {
            int questnum = 0;
            

            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (Session["currentQuestionNumber"] == null || (int)Session["currentWebpollingSessionid"] != sessionid)
            {
                Session["currentQuestionNumber"] = 0;
                Session["completed"] = "";
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
            List<answerModel> s = new List<answerModel>();
            List<int> questionCheck = new List<int>();
            List<questionModel> answeredQuestions = new questionModel().GetAnsweredMCQQuestions(sessionid, (int)Session["uid"]);

            // Get a set of answer list for this question
            foreach (var answer in unsorted)
            {
                if (pollAndQuestionModel.questionData.questionid == answer.questionid && !questionCheck.Contains(pollAndQuestionModel.questionData.questionid))
                {
                    //sorted.Add(new answerModel().displayAnswers(pollAndQuestionModel.questionData.questionid));
                    s = new answerModel().displayAnswers(pollAndQuestionModel.questionData.questionid);
                    questionCheck.Add(pollAndQuestionModel.questionData.questionid);
                }
            }
            pollAndQuestionModel.answerData = s;


            List<int?> selected = new List<int?>();
            // To set the first question's radio button to user's previous answer if he's answered it before
            foreach (var answeredquestion in answeredQuestions)
            {
                selected = new responseModel().getRankingAnswerIds(sessionid, (int)Session["uid"], pollAndQuestionModel.questionData.questionid);
                
                foreach (var a in s) { 
                    if (answeredquestion.answer == a.answer)
                    {
                        Session["selectedAnswer"] = answeredquestion.answer;
                    }

                }
            }

            if (pollAndQuestionModel.questionData.questiontype == 6)
            {
                ViewData["numOfPossibleAnswers"] = pollAndQuestionModel.questionData.numberofresponses;
                buildSelectList(s, selected);
            }
            

            return View(pollAndQuestionModel);
        }

        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StartAsyncSession(String button)
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


            //int uid = (int)Session["uid"];
            //string message = Request["msg"].ToString();
            //if (!message.Equals(""))
            //{
            //    messageModel msgModel = new messageModel();
            //    msgModel.sendFeedback(message, uid, pollid, currentquestion);
            //}

            Session["selectedAnswer"] = "";
            
            // if the user is currently answering an MCQ 
            if (allquestion[questnum].questiontype >= 3){
                // If this is a normal MCQ
                if (allquestion[questnum].questiontype < 6)
                {
                    int selectedAnswer = Convert.ToInt32(Request["MCQAnswer"]);
                    if (selectedAnswer != null)
                    {
                        
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
                            Session["selectedAnswer"] = Request["MCQAnswer"];
                            Session["completed"] = "Thank you, you have completed all of the questions in this session.";
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
                        String error = "webpollingError" + "," + "Please provide your answer(s)";
                        TempData["webpollingError"] = error;

                    }
                }
                
                // Else, it must be a ranking MCQ where a number of answers allowed is > 1
                else if (allquestion[questnum].questiontype == 6) 
                {
                    String selectedAnswer = Request["RankingAnswerList"];
                    Boolean redundant = isRedundant(selectedAnswer);
                    Boolean empty = isEmpty(selectedAnswer);

                    if (!redundant && !empty) 
                    {
                        AnswerRankingQuestion(selectedAnswer, sessionid, (int)Session["uid"], currentquestion);

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
                            Session["selectedAnswer"] = Request["MCQAnswer"];
                            Session["completed"] = "Thank you, you have completed all of the questions in this session.";
                        }
                        // its the next button
                        else
                        {
                            Session["currentQuestionNumber"] = questnum + 1;
                            int nextquestion = allquestion[(int)Session["currentQuestionNumber"]].questionid;
                            setNextAnswer(questnum - 1, nextquestion, sessionid, (int)Session["uid"]);
                        }
                    }
                    // If any of the inputs are the same value
                    else if (redundant && !empty)
                    {
                        String error = "webpollingError" + "," + "Each answer has to be unique";
                        TempData["webpollingError"] = error;

                    }
                    
                    else 
                    {
                        String error = "webpollingError" + "," + "Please provide your answer(s)";
                        TempData["webpollingError"] = error;
                    }

                }
            }
            // Then the the user is currently answering a short answer question type
            else if (allquestion[questnum].questiontype <=2 )
            {
                Boolean isValid = true;
                String selectedAnswer = Request["ShortQuestionAnswer"];
                // If its short answer numeric type only
                if (selectedAnswer == "")
                {
                    isValid = false;
                    String error = "webpollingError" + "," + "Please provide your answer(s)";
                    TempData["webpollingError"] = error;
                }
                else if (allquestion[questnum].questiontype == 1 && !Regex.IsMatch(selectedAnswer, @"^\d+$"))
                {
                    isValid = false;
                    String error = "webpollingError" + "," + "Only numeric answer is allowed";
                    Session["shortAnswer"] = "";
                    TempData["webpollingError"] = error;
                }
                else if (allquestion[questnum].questiontype == 2 && Regex.IsMatch(selectedAnswer, @"^\d+$"))
                {
                    isValid = false;
                    String error = "webpollingError" + "," + "Only alphanumeric answer is not allowed";
                    Session["shortAnswer"] = "";
                    TempData["webpollingError"] = error;
                }
                if (isValid)
                {
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
                        Session["completed"] = "Thank you, you have completed all of the questions in this session.";
                    }
                    // its the next button
                    else
                    {
                        Session["currentQuestionNumber"] = questnum + 1;
                        int nextquestion = allquestion[(int)Session["currentQuestionNumber"]].questionid;
                        setNextAnswer(questnum - 1, nextquestion, sessionid, (int)Session["uid"]);
                    }    
                }

            }
    

            return RedirectToAction("StartAsyncSession", new { sessionid = sessionid, pollid = pollid });
        }

        public ActionResult StartSyncSession(int sessionid, int pollid)
        {

            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (TempData["webpollingError"] != null)
            {
                String[] error = TempData["webpollingError"].ToString().Split(',');
                ModelState.AddModelError(error[0], error[1]);
            }

            
            Session["syncSessionId"] = sessionid;
            Session["syncPollId"] = pollid;

            int userid = (int)Session["uid"];
            
            pollModel currentQuestion = new pollModel().displaySessionDetails(sessionid)[0];
            int currentQuestionid = currentQuestion.currentquestion;

            Session["syncCurrentQuestionId"] = currentQuestionid;

            PollAndQuestions pollAndQuestionModel = new PollAndQuestions();
            pollAndQuestionModel.sessionData = new pollModel().displaySessionDetails(sessionid);

            List<questionModel> tempList = new questionModel().displayQuestionsFromAPoll(pollid);
            pollAndQuestionModel.questionData = new questionModel().getQuestion(currentQuestionid);
            pollAndQuestionModel.answerData = new answerModel().displayAnswers(currentQuestionid);

            List<answerModel> sorted = pollAndQuestionModel.answerData;

            if (pollAndQuestionModel.questionData.questiontype == 6)
            {
                Session["numOfPossibleAnswers"] = pollAndQuestionModel.questionData.numberofresponses;
                buildPlainSelectList(sorted);
            }

            return View(pollAndQuestionModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StartSyncSession(String load)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            int userid = (int)Session["uid"];
            int pollid = (int)Session["syncPollId"];

            int sessionid = (int)Session["syncSessionId"];
            Session["numOfPossibleAnswers"] = 2;


            int currentQuestionid = (int)Session["syncCurrentQuestionId"];
            Boolean isOpen = true;

            int uid = (int)Session["uid"];
            string message = Request["msg"].ToString();
            if (!message.Equals(""))
            {
                messageModel msgModel = new messageModel();
                msgModel.sendFeedback(message, uid, pollid, currentQuestionid);
            }

            PollAndQuestions pollAndQuestionModel = new PollAndQuestions();
            pollAndQuestionModel.sessionData = new pollModel().displaySessionDetails(sessionid);

            List<questionModel> tempList = new questionModel().displayQuestionsFromAPoll(pollid);
            pollAndQuestionModel.questionData = new questionModel().getQuestion(currentQuestionid);
            pollAndQuestionModel.answerData = new answerModel().displayAnswers(currentQuestionid);

            if (pollAndQuestionModel.questionData.questiontype >= 3)
            {
                // If this is a normal MCQ
                if (pollAndQuestionModel.questionData.questiontype < 6)
                {
                    int selectedAnswer = Convert.ToInt32(Request["MCQAnswer"]);
                    if (selectedAnswer != 0)
                    {
                        new responseModel().createMCQResponse(userid, selectedAnswer, sessionid);
                       
                    }
                    else
                    {
                        String error = "webpollingError" + "," + "Please provide your answer(s)";
                        TempData["webpollingError"] = error;
                        return RedirectToAction("StartSyncSession", new { sessionid = sessionid, pollid = pollid });
                    }
                }
                else if (pollAndQuestionModel.questionData.questiontype == 6) 
                {
                    String selectedAnswer = Request["RankingAnswerList"];
                    Boolean redundant = isRedundant(selectedAnswer);
                    Boolean empty = isEmpty(selectedAnswer);

                    if (!redundant && !empty) 
                    {
                        String[] answers = selectedAnswer.Split(',');
                        for (int i = 0; i < answers.Count(); i++)
                        {
                            int preferencenumber = i;
                            try
                            {
                                new responseModel().createRankingResponse(userid, Convert.ToInt32(answers[i]), sessionid, preferencenumber + 1);
                            }
                            catch (Exception e)
                            {
                                throw (e);
                            }
                        }
                    }
                    // If any of the inputs are the same value
                    else if (redundant && !empty)
                    {
                        String error = "webpollingError" + "," + "Each answer has to be unique";
                        TempData["webpollingError"] = error;
                        return RedirectToAction("StartSyncSession", new { sessionid = sessionid, pollid = pollid });
                    }
                    
                    else 
                    {
                        String error = "webpollingError" + "," + "Please provide your answer(s)";
                        TempData["webpollingError"] = error;
                        return RedirectToAction("StartSyncSession", new { sessionid = sessionid, pollid = pollid });
                    }

                }
            }
            else if (pollAndQuestionModel.questionData.questiontype <= 2)
            {
                Boolean isValid = true;
                String selectedAnswer = Request["ShortQuestionAnswer"];


                if (selectedAnswer == "" || selectedAnswer == null)
                {
                    isValid = false;
                    String error = "webpollingError" + "," + "Please provide your answer(s)";
                    TempData["webpollingError"] = error;
                    return RedirectToAction("StartSyncSession", new { sessionid = sessionid, pollid = pollid });
                }
                else if (pollAndQuestionModel.questionData.questiontype == 1 && !Regex.IsMatch(selectedAnswer, @"^\d+$"))
                {
                    isValid = false;
                    String error = "webpollingError" + "," + "Only numeric answer is allowed";
                    Session["shortAnswer"] = "";
                    TempData["webpollingError"] = error;
                    return RedirectToAction("StartSyncSession", new { sessionid = sessionid, pollid = pollid });
                }
                else if (pollAndQuestionModel.questionData.questiontype == 2 && Regex.IsMatch(selectedAnswer, @"^\d+$"))
                {
                    isValid = false;
                    String error = "webpollingError" + "," + "Only alphanumeric answer is not allowed";
                    Session["shortAnswer"] = "";
                    TempData["webpollingError"] = error;
                    return RedirectToAction("StartSyncSession", new { sessionid = sessionid, pollid = pollid });
                }
                if (isValid)
                {
                    AnswerShortAnswerQuestion(selectedAnswer, sessionid, (int)Session["uid"], currentQuestionid);
                }
                else 
                {
                    return RedirectToAction("StartSyncSession", new { sessionid = sessionid  ,pollid = pollid});
                }
            }
            

            while (isOpen)
            {
                int databaseCurrentQuestion = new pollModel().displaySessionDetails(sessionid)[0].currentquestion;
                // If the poll is closed by Poll Master, redirect them to a thank you page.
                if (currentQuestionid != databaseCurrentQuestion)
                {

                    if (databaseCurrentQuestion == 0)
                    {
                        isOpen = false;

                    }
                    //else if (databaseCurrentQuestion == -1)
                    //{
                    //    Session["showGraph"] = true;
                    //    //Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">\n");
                    //    //Response.Write("window.open( \"\/PopUp\/Popup.html\", \"\", \"width=300, height=100\")");
                    //    //Response.Write("<\/script>");
                    //}
                    else
                    {
                        Session["showGraph"] = false;
                        pollAndQuestionModel.sessionData = new pollModel().displaySessionDetails(sessionid);
                        pollAndQuestionModel.questionData = new questionModel().getQuestion(databaseCurrentQuestion);
                        List<answerModel> temp = new answerModel().displayAnswers(databaseCurrentQuestion);
                        pollAndQuestionModel.answerData = temp;
                        List<answerModel> sorted = pollAndQuestionModel.answerData;
                        Session["syncCurrentQuestionId"] = databaseCurrentQuestion;

                        if (pollAndQuestionModel.questionData.questiontype == 6)
                        {
                            Session["numOfPossibleAnswers"] = pollAndQuestionModel.questionData.numberofresponses;
                            buildPlainSelectList(sorted);
                        }

                        isOpen = new pollModel().isOpen(sessionid);
                        return View(pollAndQuestionModel);
                    }
                }
                else if (currentQuestionid == databaseCurrentQuestion)
                {
                    isOpen = new pollModel().isOpen(sessionid);
                    // This doesnt get displayed - 8 October
                }

                if (!isOpen)
                {
                    break;
                }
                else
                {

                    System.Threading.Thread.Sleep(200);
                }

                //return View(pollAndQuestionModel);
                
            }
            // When the session is closed, redirect the user somewhere *sighhhhhhh
            return RedirectToAction("ConfirmationPage", new { sessionid = sessionid, pollid = pollid });
        }

        // Function to either create or update a MCQ of type 3, 4 and 5
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

            // If a question has been answered by this user before, then update that data
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
            // else create a new row
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

        public ActionResult ConfirmationPage(int sessionid, int pollid)
        {
            PollAnswerAndSession pollAndQuestion = new PollAnswerAndSession();

            List<questionModel> tempList = new questionModel().displayQuestionsFromAPoll(pollid);

            pollAndQuestion.sessionData = new pollModel().displaySessionDetails(sessionid);
            pollAndQuestion.questionData = tempList;
            List<List<answerModel>> sorted = new List<List<answerModel>>();
            List<int> questionCheck = new List<int>();


            List<List<int>> orderedResponses = new List<List<int>>();

            foreach (var item in pollAndQuestion.questionData)
            {
                List<answerModel> test = new answerModel().displayAnswers(item.questionid);
                sorted.Add(test);
                List<int> unorderedResponses = new List<int>();
                foreach (var answer in test)
                {
                    unorderedResponses.Add(new responseModel().countResponse(answer.answerid));
                }
                orderedResponses.Add(unorderedResponses);
            }

            pollAndQuestion.answerData = sorted;
            pollAndQuestion.responseData = orderedResponses;
            

            return View(pollAndQuestion);
        }

        // Function to either create or update a MCQ of type 6, Ranking Question
        public void AnswerRankingQuestion(String selectedAnswer, int sessionid, int userid, int currentquestionid)
        {

            List<questionModel> answeredQuestions = new questionModel().GetAnsweredMCQQuestions(sessionid, userid);

            List<String> answeredQuestion = new List<String>();

            foreach (var item in answeredQuestions)
            {
                if (item.questionid == currentquestionid)
                {
                    answeredQuestion.Add(item.answer);
                }
            }

             String[] answers = selectedAnswer.Split(',');


            // If a question has been answered by this user before, then update that data
            if (answeredQuestion.Count() != 0)
            {

               
                for (int i = 0; i < answers.Count(); i++) 
                {
                    int responseId = new responseModel().getResponseId(sessionid, userid, answeredQuestion[i]);
                    int preferencenumber = i;
                    try
                    {
                        new responseModel().updateRankingResponse(responseId, Convert.ToInt32(answers[i]), preferencenumber+1);
                    }
                    catch (Exception e)
                    {
                        throw (e);
                    }
                }
                

            }
            // else create a new row
            else
            {
                for (int i = 0; i < answers.Count(); i++) 
                {
                    int preferencenumber = i;
                    try
                    {
                        new responseModel().createRankingResponse(userid, Convert.ToInt32(answers[i]), sessionid, preferencenumber+1);
                    }
                    catch (Exception e)
                    {
                        throw (e);
                    }
                }
            }

        }


        // Function for creating or updating all type of short answer question
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

            // If a question has been answered by this user before, then update that data
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
            // else create a new row
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

        // Function to generate a dropdown list dynamically, and automatically select a value if a user has answered 
        // this question and the answer is stored in the database
        private void buildSelectList(List<answerModel> answerList, List<int?> selectedAnswer)
        {
           
            List<SelectListItem> ListItems = new List<SelectListItem>();


            ListItems.Add(new SelectListItem
            {
                Text = "",
                Value = null,
                Selected = true,
            });

            foreach (var answer in answerList)
            {
                ListItems.Add(new SelectListItem
                {
                    Text = answer.answer,
                    Value = answer.answerid.ToString(),
                });

            }

            int i = 1;
            ViewData["RankingAnswerHistory"] += "Your previous answers: ";
            ViewData["RankingAnswerHistory"] += "<br />";
            foreach (int a in selectedAnswer) 
            {
                ViewData["RankingAnswerHistory"] += "Preference " + i + ": ";
                ViewData["RankingAnswerHistory"] += new answerModel().getFeedback(a);
                ViewData["RankingAnswerHistory"] += "<br />";
                i++;
            }

            ViewData["RankingAnswerHistory"] += "<br />";
            ViewData["RankingAnswerHistory"] += "Please select your answers again";
            ViewData["RankingAnswerHistory"] += "<br />";

            ViewData["RankingAnswerList"] = ListItems;
        }

        private void buildPlainSelectList(List<answerModel> answerList)
        {

            List<SelectListItem> ListItems = new List<SelectListItem>();


            ListItems.Add(new SelectListItem
            {
                Text = "",
                Value = null,
                Selected = true,
            });

            foreach (var answer in answerList)
            {
                ListItems.Add(new SelectListItem
                {
                    Text = answer.answer,
                    Value = answer.answerid.ToString(),
                });

            }


            ViewData["RankingAnswerList"] = ListItems;
        }

        public Boolean isRedundant(String answer)
        {
            String[] check = answer.Split(',');
            List<String> used = new List<String>();
            Boolean result = false;
            foreach (String item in check)
            {
                if (used.Contains(item))
                {
                    result = true;
                }
                used.Add(item);
            }
            return result;
        }

        public Boolean isEmpty(String answer)
        {
            String[] check = answer.Split(',');
            Boolean result = false;
            foreach (String item in check)
            {
                if (item == "")
                {
                    result = true;
                }
            }
            return result;
        }


        public Boolean caniseethegraph()
        {
            if (Session["showGraph"] == null) 
            {
                Session["showGraph"] = false;
            }
            return (Boolean)Session["showGraph"];
        }

        public ActionResult TwoDimensionalChart(String chartParameter)
        {
            // Grace: you need two parameters of: x axis = answer choices eg: brisbane, perth, sydney
            // y axis: those answers values

            String[] xValues = chartParameter.Split(',')[0].Split('/');
            String[] y = chartParameter.Split(',')[1].Split('/');

            int[] yValues = new int[chartParameter.Split(',')[1].Count() / 2 + 1];

            int newCounter = 0;

            foreach (String item in y)
            {
                yValues[newCounter] = Convert.ToInt32(item);
                newCounter++;
            }

            Chart chart = new Chart();
            chart.BackColor = System.Drawing.Color.Transparent;
            chart.Width = Unit.Pixel(500);
            chart.BackColor = System.Drawing.Color.LightGoldenrodYellow;

            Series series1 = new Series("Series1");
            series1.ChartArea = "ca1";
            series1.ChartType = SeriesChartType.Column;
            series1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular);
            int i = 0;
            foreach (int value in yValues)
            {
                series1.Points.Add(new DataPoint
                {
                    AxisLabel = xValues[i],
                    YValues = new double[] { value }
                });
                i++;
            }
            chart.Series.Add(series1);

            ChartArea ca1 = new ChartArea("ca1");
            ca1.BackColor = System.Drawing.Color.Transparent;
            chart.ChartAreas.Add(ca1);
            using (var ms = new System.IO.MemoryStream())
            {
                chart.SaveImage(ms, ChartImageFormat.Png);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                return File(ms.ToArray(), "image/png");
            }
        }

    }
}
