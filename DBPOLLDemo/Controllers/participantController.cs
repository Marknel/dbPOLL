using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBPOLLDemo.Controllers
{
    public class participantController : Controller
    {
        //
        // GET: /participant/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /participant/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /participant/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /participant/Create

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
        // GET: /participant/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /participant/Edit/5

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
        // GET: /participant/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /participant/Delete/5

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
