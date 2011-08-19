using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBPOLLDemo.Controllers
{
    public class HelpController : Controller
    {
        //  
        // GET: /HelloWorld/  

        

        public ActionResult Index() 
      
        {
            // Basic check to see if the user is Authenticated.
            string MyReferrer = "/";
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Return Absolute path of URLReffer in String
            //if (Request.UrlReferrer.AbsolutePath.ToString() == null)
            //{
            //     RedirectToAction("Home", "Home");
            //} 

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
            
            if (MyReferrer == "/") return new RedirectResult(Request.UrlReferrer.ToString());
            
            // string aurl = Request.UrlReferrer.ToString();
            // if (url.Length == 2) aurl = url[1] ;
            // if (url.Length > 2) aurl = url[1]+url[2] ;
            // if (MyReferrer.Contains("Main") & MyReferrer.Contains("viewPolls")) aurl = "detected";
            // if (MyReferrer == "/HOME") aurl = "Homeindex";


            if (MyReferrer.Contains("/Home/Home")) url = "HomeHome";
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
            if (MyReferrer.Contains("Report")) url = "ReportIndex";          
            
            return View(url);
            
                        
                       
 
        }



        //  
        // GET: /HelloWorld/Welcome/  

        public string help2()
        {
            return "welcome to my worlds";
        }
    }
}
