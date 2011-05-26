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
    public class PollController : Controller
    {
        //
        // GET: /Poll/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Delete(int pollid)
        {
            pollModel poll = new pollModel(pollid);
            poll.deletePoll();
            return View();
        }

        //
        // GET: /Poll/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Poll/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Poll/Create

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
        // GET: /Poll/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Poll/Edit/5

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
