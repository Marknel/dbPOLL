using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class QuestionController : Controller
    {
        
        //
        // GET: /Question/
        int pollid;
        /// <summary>
        /// Returns a view listing all the questions associated with a poll.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Index(int id, String name)
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
            ViewData["id"] = id;
            pollid = id;

            return View(new questionModel().displayQuestions(id));
        }

        //
        // GET: /Question/Details/5

        public ActionResult viewQuestions(int pollid)
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

            if (pollid == 0)
            {
                return View(new questionModel().displayAllQuestions());
            }
            else
            {
                return View(new questionModel().displayQuestions(pollid));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult viewQuestions(int pollid, String date1, String date2)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }

            bool valid = true;
            DateTime startdate;
            DateTime enddate;

            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

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
                return View(new questionModel().displayQuestions(pollid));
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
                return View(new questionModel().displayQuestions(pollid, startdate, enddate));
            }
            else
            {
                return View(new questionModel().displayQuestions(pollid));
            }
        }

        /// <summary>
        /// Returns the answers associated with the chosen question.
        /// </summary>
        /// <param name="id">Selected Question ID</param>
        /// <param name="name"></param>
        /// <returns></returns>
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

            ViewData["name"] = name;
            return RedirectToAction("Index", "Answer", new {id, name});
        }


        /// <summary>
        /// Calls the model method to delete Questions based on the Question ID and refreshes the view to display the updated list of questions.
        /// Currently all logged in user can delete question associated with their account. Need to enforce some type of
        /// Authorization based on user type.
        /// </summary>
        /// <param name="questionid"></param>
        /// <param name="id">Question ID to be deleted</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Delete(int questionid, int id, String name)
        {

            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }


            questionModel q = new questionModel(questionid);
            q.deleteQuestion();
            return RedirectToAction("Index", "Question", new { id = id, name = name });
        }

        /// <summary>
        /// Returns the view with a choice between creating a short answer and multiple choice question.
        /// </summary>
        /// <param name="pollid">Poll to create question for</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Create(int pollid, String name)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }


            ViewData["name"] = name;
            ViewData["id"] = pollid;
            return View();
        } 

        /// <summary>
        /// Returns view to create short answer questions. NOTE: Creation is not done here. Creation is performed in the 
        /// [AcceptVerbs(HttpVerbs.Post)] which is the POST version of this method.
        /// </summary>
        /// <param name="pollid"></param>
        /// <returns></returns>
        public ActionResult CreateShortAnswer(int pollid)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            ViewData["id"] = pollid;
            return View();
        } 

        /// <summary>
        /// POST method that creates short answer questions from a set of given data.
        /// </summary>
        /// <param name="shortanswertype"></param>
        /// <param name="num"></param>
        /// <param name="question"></param>
        /// <param name="chartstyle"></param>
        /// <param name="pollid"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateShortAnswer(int shortanswertype, String num, String question, int chartstyle, int pollid)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            // Allows insertion of Australian formatted dates
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            int numInt = 0;

            // Contains pollid number for display. i.e "Creating question for poll 1"
            ViewData["id"] = pollid;
            bool errorspresent = false;

            //returns the max question ID in the questions table
            int maxqid = new questionModel().getMaxID();
            

            // VALIDATE FORM DATA!
            if (!int.TryParse(num,out numInt) || num == null)
            {
                ViewData["numerror"] = "Above field must contain a number!";
                errorspresent = true;
            }

            if (question == "")
            {
                ViewData["questionerror"] = "Above field must contain a question!";
                errorspresent = true;
            }
            if(errorspresent == false)
            {
                try
                {
                    //Build question  (Autoid, short answer type = 1, question text from form, date, pollid from poll it is created it
                    questionModel q = new questionModel();
                    q.createQuestion(shortanswertype, question, chartstyle, numInt, pollid);
                    q.createDefaultObjects(pollid, q.getMaxID());

                    ViewData["created"] = "Created Question: " + question;
                    return View();
                }
                catch (Exception e)
                {
                    ViewData["error1"] = "!ERROR: "+e.Message;
                    ViewData["id"] = pollid;
                    return View();
                }
            }else{
                // We have errors. sent to user posthaste!
                ViewData["mastererror"] = "There are errors marked in the form. Please correct these and resubmit";
                return View();
            }
        }

        public ActionResult CreateMultipleChoice(int pollid)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            ViewData["id"] = pollid;
            return View();
        }

        //
        // POST: /Question/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateMultipleChoice(String num, int questiontype, String question, int chartstyle, int pollid)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            int numInt = 0;
            bool errorspresent = false;


            if (question == "")
            {
                ViewData["questionerror"] = "Above field must contain a question!";
                errorspresent = true;
            }

            if(errorspresent == false)
                {
                try
                {
                    //converts user num into string
                    numInt = int.Parse(num);
                }
                catch (Exception e)
                {
                    //Not an int. do not insert and throw view error to user. 
                    ViewData["error1"] = "!ERROR: " + e.Message;
                    return View();
                }

                try
                {

                    questionModel q = new questionModel();
                    q.createQuestion(questiontype, question, chartstyle, numInt, pollid);
                    q.createDefaultObjects(pollid, q.getMaxID());

                    ViewData["id"] = pollid;
                    ViewData["created"] = "Created Question: " + question;
                    return View();
                }
                catch (Exception e)
                {
                    // Something went bad and we couldn't edit.
                    ViewData["error1"] = "!ERROR: " + e.Message;
                    return View();
                }
            }
            else
            {
                // We have errors. sent to user posthaste!
                ViewData["mastererror"] = "There are errors marked in the form. Please correct these and resubmit";
                return View();
            }
        }

        /// <summary>
        /// Redirects to Edit view to allow modification of Question details.
        /// </summary>
        /// <param name="questionid">Question designated to be edited</param>
        /// <returns></returns>
        public ActionResult Edit(int questionid)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            return View(new questionModel().getQuestion(questionid));
        }

        /// <summary>
        /// Where the actual edit takes place.
        /// Should turn params into a form collection object if we get the time.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int questionid, int questiontype, String question, int chartstyle, int num, DateTime createdat, int pollid)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_CREATOR)
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
                questionModel q = new questionModel();
                q.updateQuestion(questionid, questiontype, question, chartstyle, num, pollid);
                ViewData["edited"] = "Updated Quesiton: " + question;
                return View(new questionModel().getQuestion(questionid));
            }
            catch(Exception e)
            {
                ViewData["quest"] = "ERROR: " + e.Message;
                
                return View();
            }
        }

        public ActionResult Test(int questionid, String name, int num_response)
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

            answerModel a = new answerModel();

            List<answerModel> list = a.displayAnswers(questionid);

            if (list.Count < 1) {
                   ViewData["message"] = "This question does not have any answers, create a question and try again.";
                return View(list);
            }

            ViewData["name"] = name;
            ViewData["qid"] = questionid;
            ViewData["num"] = num_response;
            return View(list);
        }

        public ActionResult Keypad(int qid)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }
            ViewData["qid"] = qid;
            return View();
        }

        public ActionResult Result(int aid)
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
    }
}
