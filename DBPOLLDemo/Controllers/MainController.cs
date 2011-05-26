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
    public class MainController : Controller
    {
        private DBPOLLDataContext db = new DBPOLLDataContext();
        //
        // GET: /Main/

        public ActionResult Index()
        {
            userModel user = new userModel("Terrance", "Terrance");
            //return View(pollModel.displayPolls(user));
            //return View(db.POLLs.ToList());
            //return View(new pollModel().displayPolls(user));
           

            //pollModel p = new pollModel(356672, "advdav", (decimal)76.54, (decimal)2.54, 1, DateTime.Now);
            //p.createPoll();

            if (user.verify() == true)
            {
                return View(new pollModel().displayPolls(user));
            }
            else {
                return View();
            }
        }

        //
        // GET: /Main/pollDetails/5
        public ActionResult questionDetails(int id, String name)
        {
            ViewData["name"] = name;
            return View(new questionModel().displayAnswers(id));
        }


        //
        // GET: /Main/answerDetails/5

        public ActionResult answerDetails(int id, String name)
        {
            ViewData["name"] = name;

                return View(new answerModel().displayAnswers(id));

        }

        //
        // GET: /Main/Create

        public ActionResult Create()
        {


            return View();
        } 

        //
        // POST: /Main/Create

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
        // GET: /Main/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Main/Edit/5

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
