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
            comp.unassigned = new participantModel().displayUnassignedParticipants(sessionid);

            ViewData["sessionid"] = sessionid;
            ViewData["sessionname"] = sessionname;
            return View(comp);
        } 

        //
        // POST: /participant/Create
        [HttpPost]
        public ActionResult Modify(FormCollection collection)
        {
            // Check if we are adding participants or updating existing data
            if (collection["submit"].ToString().Equals("Add Participants"))
            {
                // Adding new participant(s)
                new participantModel().createParticipantsFromCollection(collection);

                return RedirectToAction("Modify", new { sessionid = int.Parse(collection["sessionid"]), sessionname = collection["sessionname"] });
            }
            else
            {
                // Updating existing data

                new participantModel().editParticipantDataFromCollection(collection);

                return RedirectToAction("Modify", new { sessionid = int.Parse(collection["sessionid"]), sessionname = collection["sessionname"] });
            }
        }

        //
        // GET: /participant/Delete/5
 
        public ActionResult Delete(int userid, int sessionid, String sessionname)
        {
            new participantModel().deleteParticipant(userid, sessionid);
            return RedirectToAction("Modify", new { sessionid = sessionid, sessionname = sessionname});
        }

    }
}
