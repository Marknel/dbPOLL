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
            ViewData["name"] = name;
            ViewData["id"] = id;
            return View(new questionModel().displayQuestions(id));
        }

        //
        // GET: /Question/Details/5

        public ActionResult viewQuestions(int pollid)
        {
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
                ViewData["date1"] = "Please Enter a correct Date";
                valid = false;
            }

            if (!DateTime.TryParse(date2, out enddate))
            {
                ViewData["date2"] = "Please Enter a correct Date";
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
            return RedirectToAction("../Answer/Index/" + id.ToString() + "?name=" + name);
        }

        public ActionResult Delete(int questionid, int id, String name)
        {
            questionModel q = new questionModel(questionid);
            q.deleteQuestion();
            return RedirectToAction("Index", "Question", new { id = id, name = name });
        }

        //
        // GET: /Question/Create
        public ActionResult Create(int pollid, String name)
        {
            ViewData["name"] = name;
            ViewData["id"] = pollid;
            return View();
        } 

        public ActionResult CreateShortAnswer(int pollid)
        {
            ViewData["id"] = pollid;
            return View();
        } 

        //
        // POST: /Question/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateShortAnswer(int shortanswertype, String num, String question, int chartstyle, int pollid)
        {
            // Allows insertion of aussie dates
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            int numInt = 0;

            //returns the max question ID in the questions table
            int maxqid = new questionModel().getMaxID();

            try
            {
                numInt = int.Parse(num);
            }
            catch
            {
                //Not an int. do not insert and throw view error to user. 
            }
            
            try
            {
                //Build question  (Autoid, short answer type = 1, question text from form, date, pollid from poll it is created it
                questionModel q = new questionModel((maxqid + 1), shortanswertype, numInt, question,chartstyle, DateTime.Now, pollid);
                q.createQuestion();

                ViewData["error1"] = "! DONE! " + q.Question;
                ViewData["id"] = pollid;
                return View();
                //return RedirectToAction("Index/"+pollid);
            }
            catch (Exception e)
            {
                ViewData["error1"] = "!ERROR: "+e.Message;
                ViewData["id"] = pollid;
                return View();
            }
        }

        public ActionResult CreateMultipleChoice(int pollid)
        {
            ViewData["id"] = pollid;
            return View();
        }

        //
        // POST: /Question/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateMultipleChoice(String num,int questiontype, String question, int chartstyle, int pollid)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            int numInt = 0;

            //returns the max question ID in the questions table
            int maxqid = new questionModel().getMaxID();

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

                ViewData["error1"] = "! DONE! " + q.Question;
                ViewData["id"] = pollid;
                return View();
            }
            catch (Exception e)
            {
                ViewData["error1"] = "!ERROR: " + e.Message;
                return View();
            }
        }

        //
        // GET: /Question/Edit/5
 
        public ActionResult Edit(int questionid)
        {
            return View(new questionModel().getQuestion(questionid));
        }

        //
        // POST: /Question/Edit/5

        //qid x, questiontype, question, chart, short answer type (if short answer), qnum, created_at x, edited at x, pollid x, 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int questionid, int questiontype, String question, int chartstyle, int num, DateTime createdat, int pollid)
        {

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
