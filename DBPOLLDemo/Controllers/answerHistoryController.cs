using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DBPOLLDemo.Models;

using System.Threading;
using System.Globalization;

namespace DBPOLLDemo.Controllers
{
    public class answerHistoryController : Controller
    {
        //
        // GET: /answerHistory/

        public ActionResult Index(int id, String name)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["name"] = name;
            return View(new answerHistoryModel().displayAnswerHistory(id));
        }


        public ActionResult Revert(int answerid, String answer, int correct, String weight, string ansnum)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            answerModel a = new answerModel();
            a = a.getAnswer(answerid);

            /* Create a record of the old answer in the Answer History table */
            new answerHistoryModel(answerid).createAnswerHistory(a.answerid, a.answer, a.correct, a.weight, a.ansnum);

            /* Update the answer*/
            a.updateAnswer(answerid, answer, correct, int.Parse(weight), int.Parse(ansnum));
            a = a.getAnswer(answerid);
            return RedirectToAction("Index", "answerHistory", new { id = a.answerid, name = a.answer } );
        }

        public ActionResult Delete(int aid, int ahid) 
        {
            new answerHistoryModel(ahid).deleteAnswerHistory();

            answerModel a = new answerModel();
            a = a.getAnswer(aid);
            return RedirectToAction("Index", "answerHistory", new { id = a.answerid, name = a.answer });
        }
    }
  
}
