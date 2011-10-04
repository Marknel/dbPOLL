using System;
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

            ViewData["questionid"] = questionid;
            return View(new objectModel().indexObjects(questionid));
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
            objectModel ob = new objectModel(objectid);
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

            try
            {

                new objectModel().createObject(obtype, attribute, questionid);

                return RedirectToAction("Index", new { questionid = questionid });
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
