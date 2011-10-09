using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class ObjectController : Controller
    {
        //
        // GET: /Object/

        public ActionResult Index(int questionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            questionObjectModel qo = new questionObjectModel();
            List<questionObjectModel> list = qo.indexObjects(questionid);


            ViewData["message"] = "This question does not have any objects.";
            ViewData["questionid"] = questionid;
            return View(list);
        }

        //
        // GET: /Object/Details/5

        public ActionResult Details(int id)
        {

           
            return View();
        }

        public ActionResult Delete(int objectid, int questionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            questionObjectModel ob = new questionObjectModel(objectid);
            ob.deleteObject();


            return RedirectToAction("Index", "Object", new { questionid = questionid});
        }

        //
        // GET: /Object/Create

        public ActionResult Create(int questionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["questionid"] = questionid;
            return View();
        } 

        //
        // POST: /Object/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(int obtype, String attribute, int questionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["questionid"] = questionid;

            questionObjectModel ob = new questionObjectModel();
            int a = ob.getObject(obtype, questionid).obid;
            if (ob.getObject(obtype, questionid).obid != -1)
            {
                ViewData["created"] = "This object already exists.";
                return View();
            }

            try
            {
                switch (obtype)
                {
                    case 1:
                        ViewData["created"] = "Added a Countdown Timer";
                        break;
                    case 2:
                        ViewData["created"] = "Added a Response Counter";
                        break;
                    case 3:
                        ViewData["created"] = "Added a Correct Answer Indicator";
                        break;
                    default:
                        break;
                }
                new questionObjectModel().createObject(obtype, attribute, questionid);
                return View();
            }
            catch(Exception e)
            {
                ViewData["error1"] = "!ERROR: " + e.Message;
                return View();
            }
        }

        //
        // GET: /Object/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Object/Edit/5

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
