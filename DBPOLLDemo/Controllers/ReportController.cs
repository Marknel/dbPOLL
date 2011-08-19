using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
using DBPOLLDemo.Models;
using System.Web.UI;
using System.IO;

namespace DBPOLLDemo.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult SystemUtilisationReport()
        {
            return View(new userModel().displayAllUsers());
        }

        public ActionResult StatisticalReport()
        {
            
            return View(new questionModel().displayQuestionsAnswer());
        }

        public void Export(List<questionModel> list)
        {
            StringWriter sw = new StringWriter();

            //First line for column names
            //sw.WriteLine("\"ID\",\"Date\",\"Description\"");
            sw.WriteLine("\"Question\",\"Answer\"");

            foreach (questionModel item in list)
            {
                String result;
                switch (item.answer)
                {
                    case 1:
                        result = "a";
                        break;
                    case 2:
                        result = "b";
                        break;
                    case 3:
                        result = "c";
                        break;
                    case 4:
                        result = "d";
                        break;
                    case 5:
                        result = "e";
                        break;
                    case 6:
                        result = "f";
                        break;
                    case 7:
                        result = "g";
                        break;
                    case 8:
                        result = "h";
                        break;
                    case 9:
                        result = "i";
                        break;
                    case 10:
                        result = "j";
                        break;
                    default:
                        result = "Answered";
                        break;
                }
                sw.WriteLine(string.Format("\"{0}\",\"{1}\"",
                item.questnum,
                result));
            }

            Response.AddHeader("Content-Disposition", "attachment; filename=test.csv");
            Response.ContentType = "text/csv";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.Write(sw);
            Response.End();
        }

        public ActionResult SessionHistoryReport()
        {
            return View(new pollModel().displayAllPolls());
        }

        public ActionResult SessionParticipation()
        {
            return View(new questionModel().displayAttendance());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void StatisticalReport(String clickme)
        {
            Export(new questionModel().displayQuestionsAnswer());
            //ViewData["test"] = "helo";

            //string attachment = "haha.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.ContentType = "application/ms-excel";
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //TextWriter tw = null;
            //Type tpe = null;
            //HtmlTextWriter htw = Page.CreateHtmlTextWriterFromType(tw, tpe);

            //Response.Write(sw.ToString());
            //Response.End();

            //return View(new questionModel().displayQuestionsAnswer());
        }

      
    }
}
