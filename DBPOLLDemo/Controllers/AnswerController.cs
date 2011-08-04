using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DBPOLL.Models;
using DBPOLLDemo.Models;

using System.Threading;
using System.Globalization;

namespace DBPOLLDemo.Controllers
{
    public class AnswerController : Controller
    {


        //
        // GET: /Answer/

        public ActionResult Index(int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["name"] = name;
            ViewData["questionid"] = id;

            return View(new answerModel().displayAnswers(id));
        }

        //
        // GET: /Answer/Details/5

        public ActionResult Details(int id)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Delete(int answerid, int questionid, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            answerModel a = new answerModel(answerid);
            a.deleteAnswer();
            return RedirectToAction("Index", "Answer", new {id = questionid, name = name  });
        }
        

        //
        // GET: /Answer/Create

        public ActionResult Create(int questionid, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["name"] = name;
            ViewData["questionid"] = questionid;
            return View();
        } 

        //
        // POST: /Answer/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(String answer, int correct, String weight, int questionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            bool errorspresent = false;
            int weightInt = 0;


            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");

            int maxAns = new answerModel().getMaxID();

            if (!int.TryParse(weight, out weightInt) || weight == null)
            {
                ViewData["weighterror"] = "Above field must contain a number!";
                errorspresent = true;
            }

            if (answer == null)
            {
                ViewData["answererror"] = "Above field must contain an answer!";
                errorspresent = true;
            }

            if (errorspresent == false)
            {

                try
                {
                    // TODO: Add insert logic here
                    answerModel a = new answerModel((maxAns + 1), answer, correct, weightInt, DateTime.Now, questionid);
                    a.createAnswer();

                    ViewData["created"] = "Created Answer: " + a.answer;

                    ViewData["questionid"] = questionid;
                    return View();
                }
                catch (Exception e)
                {
                    ViewData["error1"] = "!ERROR: " + e.Message;
                    ViewData["questionid"] = questionid;
                    return View();
                }
            }
            else
            {
                // We have errors. sent to user posthaste!
                ViewData["mastererror"] = "There are errors marked in the form. Please correct these and resubmit";
                ViewData["questionid"] = questionid;
                return View();
            }
        }

        //
        // GET: /Answer/Edit/5

        public ActionResult Edit(int answerid, int questionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["questionid"] = questionid;
            return View(new answerModel().getAnswer(answerid));
        }

        //
        // POST: /Answer/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int answerid, String answer, int correct, String weight,DateTime createdat, int questionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int weightInt = 0;
            if (!int.TryParse(weight, out weightInt) || weight == null)
            {
                ViewData["weighterror"] = "Above field must contain a number!";
                //errorspresent = true;
            }

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");

            int maxAns = new answerModel().getMaxID();

            answerModel oldanswer = new answerModel(answerid);
            oldanswer.deleteAnswer();
            answerModel newanswer = new answerModel(answerid, answer, correct, weightInt, createdat, DateTime.Now, questionid);
            newanswer.createAnswer();



            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index", "Answer", new { id = questionid });
            }
            catch
            {
                return View();
            }
        }
    }
}
