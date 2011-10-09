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
        public ActionResult sendFeedback(string message, int pollID, int questID)
        {
            // Basic check to see if the user is Authenticated.
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            int uid = (int)Session["uid"];

            messageModel msg = new messageModel();
            msg.sendFeedback(message, uid, pollID, questID);

            return RedirectToAction("RegisterUserSuccess", "User");
        }


        public ActionResult sendPrivateMessage()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            buildSelectList();
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sendPrivateMessage(string msg, int USER_LIST)
        {
            // Basic check to see if the user is Authenticated.
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                buildSelectList();
                return RedirectToAction("Index", "Home");
            }
            int uid = (int)Session["uid"];

            if (msg.Equals(""))
            {
                ViewData["edited"] = "Please do not send blank messages";
                buildSelectList();
                return View();
            }

            messageModel messageModel = new messageModel();
            messageModel.sendMessage(msg, uid, USER_LIST);

            return RedirectToAction("sendMessageSuccess");
        }

        private void buildSelectList()
        {

            userModel userModel = new userModel();
            List<userModel> userList = userModel.getUserList();

            List<SelectListItem> ListItems = new List<SelectListItem>();

            foreach (userModel user in userList)
            {
                ListItems.Add(new SelectListItem
                {
                    Text = user.name,
                    Value = user.UserID.ToString(),
                });
            }
            ViewData["USER_LIST"] = ListItems;
        }

        public ActionResult sendMessageSuccess()
        {
            // Basic check to see if the user is Authenticated.
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
