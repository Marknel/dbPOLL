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
    public class QuestionController : Controller
    {
        
        //
        // GET: /Question/

        public ActionResult Index(int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            ViewData["name"] = name;
            ViewData["id"] = id;
            return View(new questionModel().displayQuestions(id));
        }

        //
        // GET: /Question/Details/5

        public ActionResult viewQuestions(int pollid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

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
            bool valid = true;
            DateTime startdate;
            DateTime enddate;

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

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
                return View(new questionModel().displayQuestions(pollid, startdate, enddate));
            }
            else
            {
                return View(new questionModel().displayQuestions(pollid));
            }
        }

        public ActionResult Details(int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("../Answer/Index/" + id.ToString() + "?name=" + name);
        }

        public ActionResult Delete(int questionid, int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            questionModel q = new questionModel(questionid);
            q.deleteQuestion();
            return RedirectToAction("Index", "Question", new { id = id, name = name });
        }

        //
        // GET: /Question/Create
        public ActionResult Create(int pollid, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            ViewData["name"] = name;
            ViewData["id"] = pollid;
            return View();
        } 

        public ActionResult CreateShortAnswer(int pollid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            ViewData["id"] = pollid;
            return View();
        } 

        //
        // POST: /Question/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateShortAnswer(int shortanswertype, String num, String question, int chartstyle, int pollid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            // Allows insertion of aussie dates
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            int numInt = 0;
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

            if (question == null)
            {
                ViewData["questionerror"] = "Above field must contain a question!";
                errorspresent = true;
            }
            
            
            if(errorspresent == false)
            {
                try
                {
                    //Build question  (Autoid, short answer type = 1, question text from form, date, pollid from poll it is created it
                    questionModel q = new questionModel((maxqid + 1), shortanswertype, numInt, question,chartstyle, DateTime.Now, pollid);
                    q.createQuestion();

                    ViewData["created"] = "Created Question: " + q.Question;
                    
                    return View();
                    //return RedirectToAction("Index/"+pollid);
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
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["id"] = pollid;
            return View();
        }

        //
        // POST: /Question/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateMultipleChoice(String num,int questiontype, String question, int chartstyle, int pollid)
        {
            bool errorspresent = false;
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            int numInt = 0;

            //returns the max question ID in the questions table
            int maxqid = new questionModel().getMaxID();

            if (question == null)
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
                catch
                {
                    //Not an int. do not insert and throw view error to user. 
                }

                try
                {
                    questionModel q = new questionModel((maxqid + 1), questiontype, numInt, question, chartstyle, DateTime.Now, pollid);
                    q.createQuestion();

                    ViewData["id"] = pollid;
                    ViewData["created"] = "Created Question: " + q.Question;
                    return View();
                }
                catch (Exception e)
                {
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

        //
        // GET: /Question/Edit/5
 
        public ActionResult Edit(int questionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new questionModel().getQuestion(questionid));
        }

        //
        // POST: /Question/Edit/5

        //qid x, questiontype, question, chart, short answer type (if short answer), qnum, created_at x, edited at x, pollid x, 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int questionid, int questiontype, String question, int chartstyle, int num, DateTime createdat, int pollid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");

            try
            {
                questionModel oldquest = new questionModel(questionid);
                oldquest.deleteQuestion();

                questionModel newquest = new questionModel(questionid, questiontype, question, chartstyle, num, createdat, DateTime.Now, pollid);
                newquest.createQuestion();
                
                // TODO: Add update logic here
                ViewData["quest"] = question;
                
                return View(new questionModel().getQuestion(questionid));
            }
            catch
            {
                return View();
            }
        }
    }
}
