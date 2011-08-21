using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        // This function creates a new user - if the arguments pass validation
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult New(string username, string password, string user_type)
        {

            // TODO: Create the user with given arguments (if they validate)

            // TODO: Redirect back to Dashboard... but with a 'User Created Successfully' message appearing
            return RedirectToAction("Home", "Home");
        }

    }
}
