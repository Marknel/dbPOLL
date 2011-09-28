using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class answerHistoryController : Controller
    {
        //
        // GET: /answerHistory/

        public ActionResult Index(int id)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new answerHistoryModel().displayAnswerHistory(id));
        }

        //
        // GET: /answerHistory/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /answerHistory/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /answerHistory/Create

        [HttpPost]
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
        // GET: /answerHistory/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /answerHistory/Edit/5

        [HttpPost]
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

        //
        // GET: /answerHistory/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /answerHistory/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
