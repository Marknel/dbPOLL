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
        public ActionResult Create(int pollid)
        {
            ViewData["id"] = pollid;
            return View();
        } 

        public ActionResult CreateShortAnswer()
        {
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

            //returns the max question ID in the questions table
            int maxqid = new questionModel().getMaxID();
            
            try
            {
                //Build question  (Autoid, short answer type = 1, question text from form, date, pollid from poll it is created it
                questionModel q = new questionModel((maxqid + 1), 1, question, DateTime.Now, pollid);
                q.createQuestion();

                ViewData["error1"] = "! DONE! " + q.Question;
                return View();
                //return RedirectToAction("Index/"+pollid);
            }
            catch (Exception e)
            {
                ViewData["error1"] = "!ERROR: "+e.Message;
                return View();
            }
        }

        public ActionResult CreateMultipleChoice()
        {
            return View();
        }

        //
        // POST: /Question/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateMultipleChoice(FormCollection collection)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");


            try
            {
                // TODO: Add insert logic here

                questionModel q = new questionModel(213, 2, "How many are in the room2?", DateTime.Now, 2);
                q.createQuestion();

                ViewData["error1"] = "! DONE! " + q.Question;
                return View();

                //return RedirectToAction("Index/"+2);
            }
            catch (Exception e)
            {
                ViewData["error1"] = "!ERROR: " + e.Message;
                return View();
            }
        }

        //
        // GET: /Question/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Question/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {

            try
            {
                
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
