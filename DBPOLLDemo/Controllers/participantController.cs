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
        public ActionResult Index()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }
            return View();
        }

        public ActionResult Modify(int sessionid, String sessionname)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }
            AssignedAndUnassignedParticipants comp = new AssignedAndUnassignedParticipants();
            comp.participants = new participantModel().displayParticipants(sessionid);
            comp.unassigned = new participantModel().displayUnassignedParticipants(sessionid);

            ViewData["sessionid"] = sessionid;
            ViewData["sessionname"] = sessionname;
            return View(comp);
        } 

        [HttpPost]
        public ActionResult Modify(FormCollection collection)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }
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

 
        public ActionResult Delete(int userid, int sessionid, String sessionname)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }
            new participantModel().deleteParticipant(userid, sessionid);
            return RedirectToAction("Modify", new { sessionid = sessionid, sessionname = sessionname});
        }

    }
}
