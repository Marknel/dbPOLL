using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Controllers
{
    public class AnswerController : Controller
    {


        //
        // GET: /Answer/

        public ActionResult Index(int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["name"] = name;
            ViewData["questionid"] = id;
            

            return View(new answerModel().displayAnswers(id));
        }

        //
        // GET: /Answer/Details/5

        public ActionResult Details(int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "answerHistory", new { id, name });
        }

        public ActionResult Delete(int answerid, int questionid, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            answerModel a = new answerModel(answerid);
            a.deleteAnswer();
            return RedirectToAction("Index", "Answer", new {id = questionid, name = name  });
        }

        public ActionResult CreateMethod(int questionid, String name)
        {
            if (Session["uid"] == null) { return RedirectToAction("Index", "Home"); }

            ViewData["name"] = name;
            ViewData["questionid"] = questionid;
            return View();
        } 

        //
        // GET: /Answer/Create

        public ActionResult Create(int questionid, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["name"] = name;
            ViewData["questionid"] = questionid;
            return View();
        } 

        //
        // POST: /Answer/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(String answer, int correct, String weight, int questionid, string ansnum)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            bool errorspresent = false;
            int weightInt = 0;
            int ansnumInt = 0;


            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            
            answerModel a = new answerModel();
            questionModel q = new questionModel();
            q = q.getQuestion(questionid);

            int type = q.questiontype;

            List<answerModel> list = a.displayAnswers(questionid);

            if (list.Count >= 10 && (q.questiontype == 3 || q.questiontype == 4 || q.questiontype == 5 || q.questiontype == 6))
            {
                ViewData["mastererror"] = "This Multiple Choice Question is at the limit of 10 answers. Please remove a previous answer before creating another.";
                ViewData["questionid"] = questionid;
                return View();
            } 
            

            if (!int.TryParse(weight, out weightInt) || weight == null)
            {
                ViewData["weighterror"] = "Above field must contain a number!";
                errorspresent = true;
            }

            if (answer == "")
            {
                ViewData["answererror"] = "Above field must contain an answer!";
                errorspresent = true;
            }

            if (!int.TryParse(ansnum, out ansnumInt))
            {
                ViewData["ansnumerror"] = "Answer Number must contain a number!";
                errorspresent = true;
            }

            if (errorspresent == false)
            {

                try
                {
                    new answerModel().createAnswer(answer, correct, int.Parse(weight), int.Parse(ansnum), questionid);

                    ViewData["created"] = "Created Answer: " + answer;

                    ViewData["questionid"] = questionid;
                    return View();
                }
                catch (Exception e)
                {
                    ViewData["error1"] = "!ERROR: " + e.Message +" inner: "+ e.InnerException;
                    ViewData["questionid"] = questionid;
                    return View();
                }
            }
            else
            {
                // We have errors. sent to user posthaste!
                ViewData["mastererror"] = "There are errors marked in the form. Please correct these and resubmit";
                ViewData["questionid"] = questionid;
                return View();
            }
        }

        public ActionResult CreateTrueFalse(int questionid)
        {
            if (Session["uid"] == null) { return RedirectToAction("Index", "Home"); }

            ViewData["questionid"] = questionid;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTrueFalse(String answer, int correct, String weight, int questionid, string ansnum, String answer1, int correct1, String weight1, string ansnum1)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            bool errorspresent = false;
            int weightInt = 0;
            int ansnumInt = 0;


            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (!int.TryParse(weight, out weightInt) || !int.TryParse(weight1, out weightInt) || weight == null || weight1 == null)
            {
                ViewData["weighterror"] = "Above field must contain a number!";
                errorspresent = true;
            }

            if (answer == "" || answer1 == "")
            {
                ViewData["answererror"] = "Above field must contain an answer!";
                errorspresent = true;
            }

            if (!int.TryParse(ansnum, out ansnumInt) || !int.TryParse(ansnum1, out ansnumInt) || ansnum == null || ansnum1 == null)
            {
                ViewData["ansnumerror"] = "Answer Number must contain a number!";
                errorspresent = true;
            }

            if (errorspresent == false)
            {

                try
                {
                    new answerModel().createAnswer(answer, correct, int.Parse(weight), int.Parse(ansnum), questionid);
                    new answerModel().createAnswer(answer1, correct1, int.Parse(weight1), int.Parse(ansnum1), questionid);

                    ViewData["created"] = "Created Answer: " + answer;

                    ViewData["questionid"] = questionid;
                    return View();
                }
                catch (Exception e)
                {
                    ViewData["error1"] = "!ERROR: " + e.Message + " inner: " + e.InnerException;
                    ViewData["questionid"] = questionid;
                    return View();
                }
            }
            else
            {
                // We have errors. sent to user posthaste!
                ViewData["mastererror"] = "There are errors marked in the form. Please correct these and resubmit";
                ViewData["questionid"] = questionid;
                return View();
            }
        }

        public ActionResult CreateYesNoAbstain(int questionid)
        {
            if (Session["uid"] == null) { return RedirectToAction("Index", "Home"); }

            ViewData["questionid"] = questionid;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateYesNoAbstain(String answer, int correct, String weight, int questionid, String ansnum, String answer1, int correct1, String weight1, String ansnum1, 
            String answer2, int correct2, String weight2, String ansnum2)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            bool errorspresent = false;
            int weightInt = 0;
            int ansnumInt = 0;


            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (!int.TryParse(weight, out weightInt) || !int.TryParse(weight1, out weightInt) || 
                !int.TryParse(weight2, out weightInt) || weight == null || weight1 == null || weight2 == null)
            {
                ViewData["weighterror"] = "Above field must contain a number!";
                errorspresent = true;
            }

            if (answer == "" || answer1 == "" || answer2 == "")
            {
                ViewData["answererror"] = "Above field must contain an answer!";
                errorspresent = true;
            }

            if (!int.TryParse(ansnum, out ansnumInt) || !int.TryParse(ansnum1, out ansnumInt) ||
                !int.TryParse(ansnum2, out ansnumInt) || ansnum == null || ansnum1 == null || ansnum2 == null)
            {
                ViewData["ansnumerror"] = "Answer Number must contain a number!";
                errorspresent = true;
            }

            if (errorspresent == false)
            {

                try
                {
                    new answerModel().createAnswer(answer, correct, int.Parse(weight), int.Parse(ansnum), questionid);
                    new answerModel().createAnswer(answer1, correct1, int.Parse(weight1), int.Parse(ansnum1), questionid);
                    new answerModel().createAnswer(answer2, correct2, int.Parse(weight2), int.Parse(ansnum2), questionid);

                    ViewData["created"] = "Created Answer: " + answer;

                    ViewData["questionid"] = questionid;
                    return View();
                }
                catch (Exception e)
                {
                    ViewData["error1"] = "!ERROR: " + e.Message + " inner: " + e.InnerException;
                    ViewData["questionid"] = questionid;
                    return View();
                }
            }
            else
            {
                // We have errors. sent to user posthaste!
                ViewData["mastererror"] = "There are errors marked in the form. Please correct these and resubmit";
                ViewData["questionid"] = questionid;
                return View();
            }
        }

        public ActionResult OpinionScale(int questionid)
        {
            if (Session["uid"] == null) { return RedirectToAction("Index", "Home"); }

            ViewData["questionid"] = questionid;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult OpinionScale(String answer, int correct, String weight, int questionid, String ansnum, String answer1, int correct1, String weight1, String ansnum1, 
            String answer2, int correct2, String weight2, String ansnum2, String answer3, int correct3, String weight3, String ansnum3,
            String answer4, int correct4, String weight4, String ansnum4)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            bool errorspresent = false;
            int weightInt = 0;
            int ansnumInt = 0;


            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (!int.TryParse(weight, out weightInt) || !int.TryParse(weight1, out weightInt) ||
                !int.TryParse(weight2, out weightInt) || !int.TryParse(weight3, out weightInt) ||
                !int.TryParse(weight4, out weightInt) || weight == null || weight1 == null || 
                weight2 == null || weight3 == null || weight4 == null)
            {
                ViewData["weighterror"] = "Above field must contain a number!";
                errorspresent = true;
            }

            if (answer == "" || answer1 == "" || answer2 == "" || answer3 == "" || answer4 == "")
            {
                ViewData["answererror"] = "Above field must contain an answer!";
                errorspresent = true;
            }

            if (!int.TryParse(ansnum, out ansnumInt) || !int.TryParse(ansnum1, out ansnumInt) ||
                !int.TryParse(ansnum2, out ansnumInt) || !int.TryParse(ansnum3, out ansnumInt) || !int.TryParse(ansnum4, out ansnumInt) ||
                ansnum == null || ansnum1 == null || ansnum2 == null || 
                ansnum3 == null || ansnum4 == null)
            {
                ViewData["ansnumerror"] = "Answer Number must contain a number!";
                errorspresent = true;
            }

            if (errorspresent == false)
            {

                try
                {
                    new answerModel().createAnswer(answer, correct, int.Parse(weight), int.Parse(ansnum), questionid);
                    new answerModel().createAnswer(answer1, correct1, int.Parse(weight1), int.Parse(ansnum1), questionid);
                    new answerModel().createAnswer(answer2, correct2, int.Parse(weight2), int.Parse(ansnum2), questionid);
                    new answerModel().createAnswer(answer3, correct3, int.Parse(weight3), int.Parse(ansnum3), questionid);
                    new answerModel().createAnswer(answer4, correct4, int.Parse(weight4), int.Parse(ansnum4), questionid);

                    ViewData["created"] = "Created Answer: " + answer;

                    ViewData["questionid"] = questionid;
                    return View();
                }
                catch (Exception e)
                {
                    ViewData["error1"] = "!ERROR: " + e.Message + " inner: " + e.InnerException;
                    ViewData["questionid"] = questionid;
                    return View();
                }
            }
            else
            {
                // We have errors. sent to user posthaste!
                ViewData["mastererror"] = "There are errors marked in the form. Please correct these and resubmit";
                ViewData["questionid"] = questionid;
                return View();
            }
        }
        //
        // GET: /Answer/Edit/5

        public ActionResult Edit(int answerid, int questionid)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["questionid"] = questionid;
            return View(new answerModel().getAnswer(answerid));
        }

        //
        // POST: /Answer/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int answerid, String answer, int correct, String weight, int questionid, String ansnum)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int weightInt = 0;
            int ansnumInt = 0;
            if (!int.TryParse(weight, out weightInt) || weight == null)
            {
                ViewData["weighterror"] = "Above field must contain a number!";
                //errorspresent = true;
            }

            if (answer == "")
            {
                ViewData["answererror"] = "Above field must contain an answer!";
            }

            if (!int.TryParse(ansnum, out ansnumInt))
            {
                ViewData["ansnumerror"] = "Answer Number must contain a number!";
            }

            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            
            try
            {
               answerModel a = new answerModel();
               a = a.getAnswer(answerid);

               /* Create a record of the old answer in the Answer History table */
               new answerHistoryModel(answerid).createAnswerHistory(a.answerid, a.answer, a.correct, a.weight, a.ansnum);

               /* Update the answer*/
               a.updateAnswer(answerid, answer, correct, int.Parse(weight), int.Parse(ansnum));

               ViewData["questionid"] = questionid;
               return View(a);
            }
            catch(Exception e)
            {
                ViewData["weighterror"] = "OMG THERE IS AN ERROR "+ e.Message;
                answerModel a = new answerModel().getAnswer(answerid);
                ViewData["questionid"] = questionid;
                return View(a);
            }
        }
    }
}
