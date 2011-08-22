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

using System.Web.UI.DataVisualization.Charting;


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

       

        public ActionResult SessionHistoryReport()
        {
            return View(new pollModel().displayAllPolls());
        }

        public ActionResult SessionParticipation()
        {
            return View(new questionModel().displayAttendance());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void StatisticalReportExport()
        {
            Export(new questionModel().displayQuestionsAnswer());
        }

        public void Export(List<questionModel> list)
        //public void Export()
        {
            StringWriter sw = new StringWriter();

            sw.WriteLine("\"Question\",\"Answer\"");

            foreach (questionModel item in list)
            {
                String result;
                switch (item.answernum)
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

        public ActionResult Chart()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(4);
            list.Add(8);
            list.Add(10);
            list.Add(20);
            //list = (List<int>)Session["test"];


            //ViewData["Chart"] = list;

            Chart chart = new Chart();
            chart.BackColor = System.Drawing.Color.Transparent;
            //chart.Width = Unit.Pixel(250);
            //chart.Height = Unit.Pixel(100);

            Series series1 = new Series("Series1");
            series1.ChartArea = "ca1";
            series1.ChartType = SeriesChartType.Column;
            series1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular);
            int i = 1;
            foreach (int value in list)
            {
                series1.Points.Add(new DataPoint
                {
                    AxisLabel = "Session " + i.ToString(),
                    YValues = new double[] { value }
                });
                i++;
            }
            chart.Series.Add(series1);

            ChartArea ca1 = new ChartArea("ca1");
            ca1.BackColor = System.Drawing.Color.Transparent;
            chart.ChartAreas.Add(ca1);
            using (var ms = new System.IO.MemoryStream())
            {
                chart.SaveImage(ms, ChartImageFormat.Png);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                return File(ms.ToArray(), "image/png");
            }
        }
    }
}
