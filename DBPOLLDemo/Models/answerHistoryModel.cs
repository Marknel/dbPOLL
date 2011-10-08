using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;
using DBPOLLDemo.Models;

namespace DBPOLLDemo.Models
{
    public class answerHistoryModel : System.Web.UI.Page
    {
        public int answerhid;
        public String answer;
        public int correct;
        public int weight;
        public int ansnum;
        public int updatedto;
        public int aid;
        public DateTime createdat;
        public DateTime modifiedat;
        public int questionid;
        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        ANSWER_HISTORY a = new ANSWER_HISTORY();

        public answerHistoryModel(int answerhid, String answer, int correct, int weight, int ansnum, int aid)
        {
            a.ANSWER_ID = this.answerhid = answerhid;
            a.ANSWER = this.answer = answer;
            a.CORRECT = this.correct = correct;
            a.WEIGHT = this.weight = weight;
            a.NUM = this.ansnum = ansnum;
            a.CREATED_AT = DateTime.Now;
            a.MODIFIED_AT = DateTime.Now;
            a.ANSWER_ID = this.aid = aid;

        }

        public answerHistoryModel(int answerhid)
        {
            a.ANSWERH_ID = this.answerhid = answerhid;
        }

        public answerHistoryModel()
        {
 
        }

        public List<answerHistoryModel> displayAnswerHistory(int answerid)
        {
            var query = from a in dbpollContext.ANSWER_HISTORY
                        where a.ANSWER_ID == answerid
                        orderby a.CREATED_AT descending

                        select new answerHistoryModel
                        {
                            answerhid = a.ANSWERH_ID,
                            answer = a.ANSWER,
                            correct = (int)a.CORRECT,
                            weight = (int)a.WEIGHT,
                            ansnum = (int)a.NUM,
                            aid = (int)a.ANSWER_ID
                        };

            return query.ToList();
        }

        public answerHistoryModel getAnswer(int answerhid)
        {
            var query = from a in dbpollContext.ANSWER_HISTORY
                        where a.ANSWER_ID == answerhid
                        orderby a.CREATED_AT descending

                        select new answerHistoryModel
                        {
                            answerhid = a.ANSWERH_ID,
                            answer = a.ANSWER,
                            correct = (int)a.CORRECT,
                            weight = (int)a.WEIGHT,
                            ansnum = (int)a.NUM
                        };

            return query.First();
        }

        public int getMaxID()
        {
            int query = (from a in dbpollContext.ANSWER_HISTORY
                         select a.ANSWERH_ID).Max();

            return query;
        }

        public void createAnswerHistory(int answerid, String answer, int correct, int weight, int ansnum)
        {
            try
            {
                ANSWER_HISTORY a = new ANSWER_HISTORY();


                a.ANSWERH_ID = getMaxID() + 1;
                a.ANSWER = answer;
                a.CORRECT = correct;
                a.WEIGHT = weight;
                a.NUM = ansnum;
                a.CREATED_AT = DateTime.Now;
                a.MODIFIED_AT = DateTime.Now;
                a.ANSWER_ID = answerid;

                dbpollContext.AddToANSWER_HISTORY(a);
                int status = dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void deleteAnswerHistory()
        {
            try
            {
                var answerList =
                from answers in dbpollContext.ANSWER_HISTORY
                where answers.ANSWERH_ID == this.answerhid
                select answers;

                ANSWER_HISTORY ah = answerList.FirstOrDefault<ANSWER_HISTORY>();
                
                dbpollContext.DeleteObject(ah);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

    }
}