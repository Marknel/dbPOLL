using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBPOLLDemo.Controllers
{
    public class HelpController : Controller
    {
        
        
		// Detect the URL , then Detect the page and return the related help page
        public ActionResult Index() 
      
        {
            // Basic check to see if the user is Authenticated.
            string MyReferrer = "/";
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Return Absolute path of URLReffer in String
            //


            try
            {
               MyReferrer = Request.UrlReferrer.AbsolutePath.ToString();
            }
            catch(Exception e)
            {
                ViewData["error"] = e.Message;
                return View ("Error");
            }
            
            // string[] aurl = MyReferrer.Split('/');
            string url="index";
            
            // if (MyReferrer == "/") return new RedirectResult(Request.UrlReferrer.ToString());
            
            // string aurl = Request.UrlReferrer.ToString();
            // if (url.Length == 2) aurl = url[1] ;
            // if (url.Length > 2) aurl = url[1]+url[2] ;
            // if (MyReferrer.Contains("Main") & MyReferrer.Contains("viewPolls")) aurl = "detected";
            // if (MyReferrer == "/HOME") aurl = "Homeindex";


            //if (MyReferrer.Contains("/")) url = "HomeHome";
            if (MyReferrer == "/" | MyReferrer.Contains("Home/Index")) url = "HomeHome";
            if (MyReferrer.Contains("viewPolls")) url = "PollViewPolls";
            if (MyReferrer == "/Poll" | MyReferrer.Contains("/Poll/Index")) url = "PollIndex";
            if (MyReferrer.Contains("/Poll/TestDevices")) url = "PollTestDevices";
            if (MyReferrer == "/Answer" | MyReferrer.Contains("/Answer/Index")) url = "AnswerIndex";
            if (MyReferrer.Contains("Answer/Edit")) url = "AnswerEdit";
            if (MyReferrer == "/Object" | MyReferrer.Contains("Object/Index")) url = "ObjectIndex";
            if (MyReferrer == "/Question" | MyReferrer.Contains("/Question/Index")) url = "QuestionIndex";
            if (MyReferrer.Contains("/Question/Edit")) url = "QuestionEdit";
            if (MyReferrer.Contains("/Question/viewQuestions")) url = "QuestionviewQuestions";
            if (MyReferrer.Contains("/Question/Create")) url = "QuestionCreate";
            if (MyReferrer == "/Report" | MyReferrer.Contains("/Report/Index")) url = "ReportIndex";
            if (MyReferrer.Contains("/Report/SessionHistoryReport")) url = "SessionHistoryReport";
            if (MyReferrer.Contains("/Report/OneStatisticalReport")) url = "ReportOneStatisticalReport";
            if (MyReferrer.Contains("/Report/ViewAllPoll")) url = "ReportOneStatisticalReport";
            if (MyReferrer.Contains("/Report/DemographicComparison")) url = "ReportDemographicComparison";
            if (MyReferrer.Contains("/Report/SessionParticipation")) url = "ReportSessionParticipation";
            if (MyReferrer.Contains("/Poll/RunDevices")) url = "PollRunDevices";
            if (MyReferrer.Contains("/Poll/AssignPoll")) url = "PollAssignPoll";
            if (MyReferrer.Contains("/Session/Edit")) url = "SessionEdit";
            if (MyReferrer.Contains("/Session/Create")) url = "SessionCreate";
            if (MyReferrer.Contains("/User/ChangePassword")) url = "UserChangePassword";
            if (MyReferrer.Contains("/User/Edit")) url = "UserEdit";
            if (MyReferrer.Contains("/User/RegisterUser")) url = "UserRegisterUser";


            //if (MyReferrer.Contains("/User/ChangePassword ")) url = "UserChangePassword";
            //if (MyReferrer.Contains("")) url = "";

        
            
            return View(url);
        }

        //  
        // Link to different Help View

        public ActionResult Site(string idi) 
        {
			// Basic check to see if the user is Authenticated.
            
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
		            return View(idi);
        }
    }
}
