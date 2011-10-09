using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using DBPOLLDemo.Models;
using System.Web.UI.WebControls;


namespace DBPOLLDemo.Controllers
{
    public class TwoQuestionModels
    {
        public List<questionModel> data1 { get; set; }
        public List<questionModel> data2 { get; set; }
    }

    public class PollQuestionAnswer
    {
        public List<questionModel> qModelList { get; set; }
        public List<pollModel> pModelList { get; set; }
        public List<answerModel> aModelList { get; set; }
    }

    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            return View();
        }

        public ActionResult SystemUtilisationReport()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if (!Session["sysadmin"].ToString().Equals("true"))
            {
                return RedirectToAction("Invalid", "Home");
            }
            return View(new userModel().displayAllUsers());
        }

        public ActionResult StatisticalReport()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }
            TwoQuestionModels twoModels = new TwoQuestionModels();

            twoModels.data1 = new questionModel().displayQuestionsAnswer();
            twoModels.data2 = new questionModel().displayQuestionsAnswer();

            return View(twoModels);
        }

        public ActionResult OneStatisticalReport(int pollID)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }
            //return View(new questionModel().displayOneQuestionAnswer(pollID));

            TwoQuestionModels twoModels = new TwoQuestionModels();

            twoModels.data1 = new questionModel().displayOneQuestionAnswer(pollID);
            twoModels.data2 = new questionModel().displayOneQuestionAnswer(pollID);

            return View(twoModels);
        }

        public ActionResult SessionHistoryReport()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }
            return View(new pollModel().displayAllPolls());
        }

        public ActionResult SessionParticipation()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }
            return View(new questionModel().displayAttendance());
        }

        //[AcceptVerbs(HttpVerbs.Post)]
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

        public ActionResult Chart(String chartParameter)
        {

            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }

            int newCounter = 0;
            int[] sessionValues = new int[chartParameter.Split(',')[0].Count() / 2 + 1];
            String[] temp = chartParameter.Split(',')[0].Split('/');
            foreach (String item in temp)
            {
                sessionValues[newCounter] = Convert.ToInt32(item);
                newCounter++;
            }

            String[] sessionLists = chartParameter.Split(',')[1].Split('/');
            String[] answerLists = chartParameter.Split(',')[2].Split('/');
            String graphType = (String)Session["graphType"];

            Chart chart = new Chart();
            chart.Width = Unit.Pixel(500);
            //chart.Height = Unit.Pixel(500);
            
            chart.BackColor = System.Drawing.Color.LightGoldenrodYellow;

            ChartArea ca1 = new ChartArea("ca1");
            ca1.BackColor = System.Drawing.Color.Transparent;
            chart.ChartAreas.Add(ca1);

            int j = 0;
            foreach (String value in answerLists)
            {
                if (value != null)
                {
                    Legend newLegend = new Legend(value);
                    newLegend.IsTextAutoFit = true;
                    chart.Legends.Add(newLegend);
                    //chart.Legends.Add(new Legend(value));
                    chart.Legends[value].DockedToChartArea = "ca1";
                    //chart.Legends[value].IsTextAutoFit = true;

                    Series series1 = new Series(value);
                    series1.ChartArea = "ca1";
                    if (graphType == "Bar")
                    {
                        series1.ChartType = SeriesChartType.Bar;
                    }
                    else
                    {
                        series1.ChartType = SeriesChartType.Column;
                    }

                    series1.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular);

                    for (int l = 0; l < sessionLists.Count(); l++)
                    {
                        //check if it isnt out of bounds
                        if (sessionLists[l] != null && sessionValues[j] != null)
                        {
                            series1.Points.Add(new DataPoint
                            {
                                AxisLabel = sessionLists[l],
                                YValues = new double[] { sessionValues[j] }
                            });
                            j++;
                        }
                    }

                    chart.Series.Add(series1);
                    chart.Series[value].Legend = value;
                }
            }

            using (var ms = new System.IO.MemoryStream())
            {
                chart.SaveImage(ms, ChartImageFormat.Png);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                return File(ms.ToArray(), "image/png");
            }
        }

        public Boolean contains(List<Series> series, Series s)
        {
            foreach (Series se in series)
            {
                if (se.Name == s.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public ActionResult ViewAllPoll()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }
            // need an if statement, check if the current user is only poll master, then he could
            // only see a list of polls he manages. Else if user == higher level eg poll admin, he
            // is able to see ALL POLL

            // if poll master:
            //return View(new pollModel().displayPolls());

            // else
            //String graphType = Request["graphType"];
            //Session["graphType"] = graphType;
            return View(new pollModel().displayPollsThatContainSessions());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ViewAllPoll(String graphType)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }
            // need an if statement, check if the current user is only poll master, then he could
            // only see a list of polls he manages. Else if user == higher level eg poll admin, he
            // is able to see ALL POLL

            // if poll master:
            //return View(new pollModel().displayPolls());

            // else
            Session["graphType"] = graphType;
            return View(new pollModel().displayPollsThatContainSessions());
        }

        public ActionResult DemographicComparison()
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DemographicComparison(String demographic, String graphType, String includeOrExclude)
        {
            if (Session["uid"] == null || Session["uid"].ToString().Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            if ((int)Session["user_type"] < User_Type.POLL_ADMINISTRATOR)
            {
                return RedirectToAction("Invalid", "Home");
            }
            TwoQuestionModels twoModels = new TwoQuestionModels();

            Session["graphType"] = graphType;

            if (includeOrExclude == "Include")
            {
                List<questionModel> result = new questionModel().includeDemographicQuestions(demographic);
                if (!result.Equals(null))
                {

                    twoModels.data1 = new questionModel().includeDemographicQuestions(demographic);
                    twoModels.data2 = new questionModel().includeDemographicQuestions(demographic);

                    if (twoModels.data1.Count() == 0)
                    {
                        ViewData["error"] = "Your search returns no result. Please try again.";
                    }
                    else
                    {
                        ViewData["error"] = "";
                    }

                    return View(twoModels);
                }

                else
                {
                    return View();
                }
            }

            //then it's exclude
            else
            {
                List<questionModel> result = new questionModel().excludeDemographicQuestions(demographic);
                if (!result.Equals(null))
                {
                    twoModels.data1 = new questionModel().excludeDemographicQuestions(demographic);
                    twoModels.data2 = new questionModel().excludeDemographicQuestions(demographic);

                    if (twoModels.data1.Count() == 0)
                    {
                        ViewData["error"] = "Your search returns no result. Please try again.";
                    }

                    else
                    {
                        ViewData["error"] = "";
                    }

                    return View(twoModels);
                }

                else
                {
                    return View();
                }
            }
        }


        private void buildSelectList(List<String> answerList, String name)
        {

            List<SelectListItem> ListItems = new List<SelectListItem>();

            ListItems.Add(new SelectListItem
            {
                Text = "",
                Value = null,
                Selected = true,
            });

            foreach (var answer in answerList)
            {
                ListItems.Add(new SelectListItem
                {
                    Text = answer,
                    Value = answer,
                });

            }

            ViewData[name] = ListItems;
        }
    }
}
