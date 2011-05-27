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
    public class AnswerController : Controller
    {
        //
        // GET: /Answer/

        public ActionResult Index(int id, String name)
        {
            ViewData["name"] = name;
            ViewData["id"] = id;

            return View(new answerModel().displayAnswers(id));
        }

        //
        // GET: /Answer/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Delete(int answerid, int id, String name)
        {
            answerModel a = new answerModel(answerid);
            a.deleteAnswer();
            return RedirectToAction("Index", "Answer", new {id = id, name = name  });
        }
        

        //
        // GET: /Answer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Answer/Create

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
        // GET: /Answer/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Answer/Edit/5

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
