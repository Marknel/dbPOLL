using System;
using System.Web.Mvc;
using System.Collections.Generic;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class AssignedAndUnassignedParticipants
    {
        public List<participantModel> participants;
        public List<userModel> unassigned;
    }


    public class ParticipantController : Controller
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

        public ActionResult Modify(int sessionid, String sessionname)
        {
            AssignedAndUnassignedParticipants comp = new AssignedAndUnassignedParticipants();
            comp.participants = new participantModel().displayParticipants(sessionid);
            comp.unassigned = new userModel().displayUnassignedParticipants(sessionid);

            ViewData["sessionid"] = sessionid;
            ViewData["sessionname"] = sessionname;

            return View(comp);
        } 

        //
        // POST: /participant/Create

        [HttpPost]
        public ActionResult Modify(FormCollection collection)
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
 
        public ActionResult Delete(int userid, int sessionid, String sessionname)
        {
            new participantModel().deleteParticipant(userid, sessionid);
            return RedirectToAction("Modify", new { sessionid = sessionid, sessionname = sessionname});
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
