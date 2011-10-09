using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class UserController : Controller
    {
        //needed?
        public ActionResult Index()
        {
            return View();
        }


        // This function displays the user creation screen
        public ActionResult RegisterUser()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }

            buildSelectList();

            return View();
        }

        private void buildSelectList()
        {
            int userType = (int)Session["user_type"];

            userModel user = new userModel();
            var userDetails = user.get_details((int)Session["uid"]);
            ViewData["User"] = userDetails;

            List<SelectListItem> ListItems = new List<SelectListItem>();
            ListItems.Add(new SelectListItem
            {
                Text = "Poll User",
                Value = "1"
            });
            if (userType > 2)
            {
                ListItems.Add(new SelectListItem
                {
                    Text = "Poll Master",
                    Value = "2",
                    Selected = true
                });
            }
            if (userType > 3)
            {
                ListItems.Add(new SelectListItem
                {
                    Text = "Poll Creator",
                    Value = "3"
                });
            }
            ViewData["USER_TYPE"] = ListItems;
        }

        // This function creates a new user - if the arguments pass validation
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterUser(string email, string name, int user_type)
        {
            // Basic check to see if the user is Authenticated.
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_MASTER)
            {
                return RedirectToAction("Invalid", "Home");
            }
            bool errorspresent = false;
            // VALIDATE FORM DATA!
            if (name == null || name == "")
            {
                ViewData["nameError"] = "Above field must contain a name!";
                errorspresent = true;
            }
            else if (name.Length > 64)
            {
                ViewData["nameError"] = "Name is too long, maximum length allowed is 64 characters";
                errorspresent = true;
            }
            //if (email == null || System.Text.RegularExpressions.Regex.IsMatch(email, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=
            //  [0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
            if (email == null || !Regex.IsMatch(email, @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase))
            {
                ViewData["emailError"] = "Above field must contain a valid email address!";
                errorspresent = true;
            }
            else if (email.Length > 64)
            {
                ViewData["emailError"] = "Email address is too long, maximum length allowed is 64 characters";
                errorspresent = true;
            }
            if (errorspresent)
            {
                buildSelectList();
                return View();
            }



            userModel user = new userModel();

            // Get the ID for a new user
            int UserID = user.getNewID();

            string password = user.Password_Generator();
            DateTime expiry_Date = DateTime.Now.AddYears(10);

            // Create the user
            if (!user.createUser(UserID, user_type, password, name, email, (int)Session["uid"]))
            {
                ViewData["Message"] = "A user account with this email address already exists";
                buildSelectList();
                return View();
            }

            // Send Email to new user
            EmailController mail = new EmailController(email, password, email);

            string mailSuccess = mail.send();
            if (!mailSuccess.Equals("Email sent successfully"))
            {
                throw new Exception(mailSuccess);
            }


            return RedirectToAction("RegisterUserSuccess", "User");
        }

        public ActionResult RegisterUserSuccess()
        {
            // Basic check to see if the user is Authenticated.
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

        public ActionResult ChangePassword()
        {
            // Basic check to see if the user is Authenticated.
            if (Session["Created"] == null && (Session["uid"] == null || Session["uid"].ToString().Equals("")))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            // Basic check to see if the user is Authenticated.
            if (Session["Created"] == null && (Session["uid"] == null || Session["uid"].ToString().Equals("")))
            {
                return RedirectToAction("Index", "Home");
            }
            int uid;
            if (Session["uid"] == null)
                uid = (int)Session["Created"];
            else
                uid = (int)Session["uid"];

            //confirm passwords match
            if (!newPassword.Equals(confirmPassword))
            {
                ViewData["confirmPassword"] = "Passwords must match!";
                return View();
            }

            if (newPassword == null || newPassword.Equals(""))
            {
                ViewData["confirmPassword"] = "Please enter a new Password!";
                return View();
            }
            else if (newPassword.Length > 64)
            {
                ViewData["confirmPassword"] = "New password is too long";
                return View();
            }

            //confirm current password is correct
            userModel user = new userModel();
            var userDetails = user.get_details(uid);
            string username = userDetails.USERNAME;

            if (user.verify(username, currentPassword) == 0)
            {
                ViewData["currentPassword"] = "Password incorrect";
                return View();
            }

            //write new password to db
            user.changePassword(uid, newPassword);

            //let them see all the links now that they've changed their password
            if (Session["Created"] != null)
            {
                Session["uid"] = Session["Created"];
                Session["Created"] = null;
            }
            return View("ChangepasswordSuccess");
        }


        /// <summary>
        /// Redirects to Edit view to allow modification of User's details.
        /// </summary>
        /// <param name="UserID">User to be edited</param>
        /// <returns></returns>
        public ActionResult Edit()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_USER)
            {
                return RedirectToAction("Invalid", "Home");
            }
            int UserID = (int)Session["uid"];
            return View(new userModel().getUser(UserID));
        }

        /// <summary>
        /// Where the actual edit takes place.
        /// Should turn params into a form collection object if we get the time.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int UserID, string name, string email)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_USER)
            {
                return RedirectToAction("Invalid", "Home");
            }

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            bool errorspresent = false;

            if (name == null || name == "")
            {
                ViewData["nameError"] = "Above field must contain a name!";
                errorspresent = true;
            }
            if (email == null || !Regex.IsMatch(email, @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase))
            {
                ViewData["emailError"] = "Above field must contain a valid email address!";
                errorspresent = true;
            }


            if (errorspresent)
            {
                return View(new userModel().getUser(UserID));
            }


            try
            {
                userModel u = new userModel();
                u.updateUser(UserID, name, email);

                ViewData["edited"] = "Details successfully changed";
                return View(new userModel().getUser(UserID));
            }
            catch (Exception e)
            {
                ViewData["edited"] = "!ERROR: " + e.Message;
                return View(new userModel().getUser(UserID));
            }
        }
    }
}
