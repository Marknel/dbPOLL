using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class MessageController : Controller
    {
        //
        // GET: /Message/

        public ActionResult Index()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult sendFeedback()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sendFeedback(string message)
        {
            // Basic check to see if the user is Authenticated.
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            int uid = (int)Session["uid"];
            int pollID = (int)Session["pollID"];
            messageModel msg = new messageModel();
            //msg.sendFeedback(message, uid, pollID);





            return RedirectToAction("RegisterUserSuccess", "User");
        }


        public ActionResult replyConfirm()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
