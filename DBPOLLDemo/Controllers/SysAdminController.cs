using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DBPOLLDemo.Models;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web.UI;

namespace DBPOLLDemo.Controllers
{
    public class SysAdminController : Controller
    {
        //
        // GET: /SysAdmin/

        public ActionResult Index()
        {
            // Basic check to see if the user is Authenticated.
            if (Session["sysadmin"] != "true")
            {
                return RedirectToAction("Index", "Home");
            }
            //Export(new userModel().displayPollAdminUsers());

            return View(new userModel().displayPollAdminUsers());
        }


        public ActionResult DeleteConfirm(int UserID)
        {
            if (Session["sysadmin"] != "true")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["delID"] = UserID;
            return View("DeleteConfirm");
        }

        public ActionResult Delete(int UserID)
        {
            if (Session["sysadmin"] != "true")
            {
                return RedirectToAction("Index", "Home");
            }


            userModel q = new userModel(UserID);
            q.deleteUser();
            return View(new userModel().displayPollAdminUsers());
        }


        /// <summary>
        /// Redirects to Edit view to allow modification of User's details.
        /// </summary>
        /// <param name="UserID">User to be edited</param>
        /// <returns></returns>
        public ActionResult Edit(int UserID)
        {
            // Basic check to see if the user is Authenticated.
            if (Session["sysadmin"] != "true")
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
        public ActionResult Edit(int UserID, DateTime Expires_At, string Name, string username)
        {
            //if (Session["uid"] == null) { return RedirectToAction("Index", "Home"); }

            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            try
            {
                userModel u = new userModel();
                u.updateUser(UserID, Expires_At, Name, username);

                ViewData["edited"] = "Details successfully changed";
                return View(new userModel().getUser(UserID));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Returns the view with a choice between creating a short answer and multiple choice question.
        /// </summary>
        /// <param name="pollid">Poll to create question for</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Create()
        {
            // Basic check to see if the user is Authenticated.
            if (Session["sysadmin"] != "true")
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
        public ActionResult Create(String name, String email, string expiry)
        {
            // Basic check to see if the user is Authenticated.
            if (Session["sysadmin"] != "true")
            {
                return RedirectToAction("Index", "Home");
            }
            string password = name;
            bool errorspresent = false;
            //if (Session["uid"] == null) { return RedirectToAction("Index", "Home"); }
            int SysAdmin_ID = 1000; //Int32.Parse(Session["uid"].ToString());

            // Allows insertion of Australian formatted dates
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            int expInt = 0;


            //returns the max question ID in the questions table
            int UserID = new userModel().getNewID();

            // VALIDATE FORM DATA!
            if (name == null || name == "")
            {
                ViewData["nameError"] = "Above field must contain a name!";
                errorspresent = true;
            }
            if (email == null || System.Text.RegularExpressions.Regex.IsMatch(email, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=
                [0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
            {
                ViewData["emailError"] = "Above field must contain a valid email address!";
                errorspresent = true;
            }
            if (expiry == null)
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


            if (errorspresent == false)
            {
                try
                {
                    DateTime expiry_Date = DateTime.Now.AddMonths(expInt);
                    //Build question  (Autoid, short answer type = 1, question text from form, date, pollid from poll it is created it
                    new userModel().createUser(UserID, 4, password, name, email, expiry_Date, SysAdmin_ID);
                    ViewData["created"] = "Created User: " + name;

                    EmailController mail = new EmailController(email, password, email);
                    
                    string mailSuccess = mail.send();
                    if (!mailSuccess.Equals("Email sent successfully"))
                    {
                        throw new Exception(mailSuccess);
                    }


                    return View();
                }
                catch (Exception e)
                {
                    ViewData["error1"] = "!ERROR: " + e.Message;
                    return View();
                }
            }
            else
            {
                // We have errors, send to user posthaste!
                ViewData["mastererror"] = "There are errors marked in the form. Please correct these and resubmit";
                return View();
            }
        }
    }
}
