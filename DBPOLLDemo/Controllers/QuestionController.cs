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
            return View(new questionModel().displayQuestions(pollid));
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

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Question/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
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
