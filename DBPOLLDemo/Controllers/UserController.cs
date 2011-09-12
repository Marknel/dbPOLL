using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }


        // This function displays the user creation screen
        public ActionResult New()
        {
            // Basic check to see if the user is Authenticated.
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            userModel user = new userModel();
            var userDetails = user.get_details((int)Session["uid"]);
            ViewData["User"] = userDetails;
            return View();
        }

        // This function creates a new user - if the arguments pass validation
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult New(string email, string name, string user_type)
        {
            int user_power = 0;
            switch (user_type)
            {
                case "Poll User":
                    user_power = 1;
                    break;
                    
                case "Poll Master":
                    user_power = 2;
                    break;

                case "Poll Creator":
                    user_power = 3;
                    break;

                case "Poll Administrator":
                    user_power = 4;
                    break;

                default:
                    user_power = -1;
                    break;
            }
            
            // TODO: Validation of inputs

            // Get the ID for a new user
            int UserID = new userModel().getNewID();

            string password = name;
            DateTime expiry_Date = DateTime.Now.AddYears(10);

            // Create the user
            new userModel().createUser(UserID, user_power, password, name, email, (int)Session["uid"]);

            // Send Email to new user
            EmailController mail = new EmailController(email, password, email);

            string mailSuccess = mail.send();
            if (!mailSuccess.Equals("Email sent successfully"))
            {
                throw new Exception(mailSuccess);
            }

            // TODO: Redirect back to Dashboard... but with a 'User Created Successfully' message appearing
            ViewData["Message"] = "User Created Successfully";
            return RedirectToAction("Home", "Home");
        }
    }
}
