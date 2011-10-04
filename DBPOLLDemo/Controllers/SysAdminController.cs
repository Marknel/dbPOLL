using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class SysAdminController : Controller
    {
        //
        // GET: /SysAdmin/

        public ActionResult Index()
        {
            // Basic check to see if the user is Authenticated.
            if (!Session["sysadmin"].ToString().Equals("true") || Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(new userModel().displayPollAdminUsers());
        }


        public ActionResult DeleteConfirm(int UserID)
        {
            if (!Session["sysadmin"].ToString().Equals("true") || Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["delID"] = UserID;
            return View();
        }

        public ActionResult DeleteSuccess(int UserID)
        {
            if (!Session["sysadmin"].ToString().Equals("true") || Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }

            userModel q = new userModel(UserID);
            q.deleteUser();

            return View(new userModel().displayPollAdminUsers());
        }

        public ActionResult SysAdmin()
        {
            if (!Session["sysadmin"].ToString().Equals("true") || Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new userModel().displayPollAdminUsers());
        }

        /// <summary>
        /// Redirects to Edit view to allow modification of User's details.
        /// </summary>
        /// <param name="UserID">User to be edited</param>
        /// <returns></returns>
        public ActionResult Edit(int UserID)
        {
            if (!Session["sysadmin"].ToString().Equals("true") || Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new userModel().getUser(UserID));
        }

        /// <summary>
        /// Where the actual edit takes place.
        /// Should turn params into a form collection object if we get the time.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int UserID, string expiry, string name, string email)
        {
            if (!Session["sysadmin"].ToString().Equals("true") || Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            bool errorspresent = false;
            int expInt = 0;

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
            if (expiry == null || expiry == "")
                expInt = 12;
            else if (!System.Text.RegularExpressions.Regex.IsMatch(expiry, @"^\d+$"))
            {
                ViewData["expiryError"] = "Expiry date must be a whole non-negative number";
                errorspresent = true;
            }
            else
            {
                try
                {
                    //converts user num into string
                    expInt = int.Parse(expiry);
                }
                catch (Exception e)
                {
                    //Not an int. do not insert and throw view error to user. 
                    ViewData["expiryError"] = "!ERROR: " + e.Message;
                    errorspresent = true;
                }
            }

            if (errorspresent)
            {
                return View(new userModel().getUser(UserID));
            }


            try
            {
                DateTime expiry_Date = DateTime.Now.AddMonths(expInt);
                userModel u = new userModel();
                u.updateUser(UserID, expiry_Date, name, email);

                ViewData["edited"] = "Details successfully changed";
                return View(new userModel().getUser(UserID));
            }
            catch(Exception e)
            {
                ViewData["edited"] = "!ERROR: " + e.Message;
                return View(new userModel().getUser(UserID));
            }
        }

        

        /// <summary>
        /// Returns the view with a choice between creating a short answer and multiple choice question.
        /// </summary>
        /// <param name="pollid">Poll to create question for</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult RegisterUser()
        {
            // Basic check to see if the user is Authenticated.
            if (!Session["sysadmin"].ToString().Equals("true") || Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        


        /// <summary>
        /// POST method that creates short answer questions from a set of given data.
        /// </summary>
        /// <param name="shortanswertype"></param>
        /// <param name="num"></param>
        /// <param name="question"></param>
        /// <param name="chartstyle"></param>
        /// <param name="pollid"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterUser(String name, String email, string expiry)
        {
            // Basic check to see if the user is Authenticated.
            if (!Session["sysadmin"].ToString().Equals("true") || Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            bool errorspresent = false;
            int SysAdmin_ID = (int)Session["uid"];


            // Allows insertion of Australian formatted dates
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            int expInt = 0;


            //returns the max question ID in the questions table
            int UserID = new userModel().getNewID();

            // VALIDATE FORM DATA!
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
            if (expiry == null || expiry == "")
                expInt = 12;
            else if (!System.Text.RegularExpressions.Regex.IsMatch(expiry, @"^\d+$"))
            {
                ViewData["expiryError"] = "Expiry date must be a whole non-negative number";
                errorspresent = true;
            }
            else
            {
                try
                {
                    //converts user num into string
                    expInt = int.Parse(expiry);
                }
                catch (Exception e)
                {
                    //Not an int. do not insert and throw view error to user. 
                    ViewData["expiryError"] = "!ERROR: " + e.Message;
                    errorspresent = true;
                }
            }

            if (errorspresent)
            {
                return View();
            }


            try
            {
                userModel user = new userModel();
                DateTime expiry_Date = DateTime.Now.AddMonths(expInt);
                string password = user.Password_Generator();
                //Build question  (Autoid, short answer type = 1, question text from form, date, pollid from poll it is created it
                user.createUser(UserID, 4, password, name, email, expiry_Date, SysAdmin_ID);

                EmailController mail = new EmailController(email, password, email);

                string mailSuccess = mail.send();
                if (!mailSuccess.Equals("Email sent successfully"))
                {
                    throw new Exception(mailSuccess);
                }

                return RedirectToAction("RegisterUserSuccess", "SysAdmin");
            }
            catch (Exception e)
            {
                ViewData["error1"] = "!ERROR: " + e.Message;
                return View();
            }
        }
        

        /// <summary>
        /// Returns the view with a choice between creating a short answer and multiple choice question.
        /// </summary>
        /// <param name="pollid">Poll to create question for</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult RegisterUserSuccess()
        {
            // Basic check to see if the user is Authenticated.
            if (!Session["sysadmin"].ToString().Equals("true") || Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        
        }
    }
}
