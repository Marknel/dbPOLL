using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DBPOLL.Models;
using DBPOLLContext;
using DBPOLLDemo.Models;

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
