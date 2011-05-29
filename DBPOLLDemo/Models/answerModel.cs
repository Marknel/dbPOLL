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
using DBPOLLContext;

namespace DBPOLLDemo.Models
{
    public class answerModel
    {
        public int answerid;
        public String answer;
	    public int correct;
        public int weight;
        public int ansnum;
        public int updatedto;
        public DateTime createdat;
        public DateTime modifiedat;
        public int questionid;
        public DBPOLLDataContext db = new DBPOLLDataContext();
        ANSWER a = new ANSWER();

        //Properties for getters/setters
        public String Answer { get { return answer; } }
        public int AnswerNumber { get { return ansnum; } }
        public int AnswerID { get { return answerid; } }
        //public int pollID { get { return pollid; } }

        public answerModel(int answerid, String answer, int correct, int weight, int ansnum, int updatedto, DateTime createdat)
        {
            a.ANSWERID = this.answerid = answerid;
            a.ANSWER1 = this.answer = answer;
            a.CORRECT = this.correct = correct;
            a.WEIGHT = this.weight = weight;
            a.NUM = this.ansnum = ansnum;
            a.UPDATEDTO = this.updatedto;
            a.CREATEDAT = this.createdat;
           
        }

        public answerModel(int answerid, String ansnum, String answer)
        {
            int ansnumber = 0;
            a.ANSWERID = this.answerid = answerid;



            try { ansnumber = int.Parse(ansnum); }
            catch { ansnumber = 0; };
            a.NUM = this.ansnum = ansnumber;

            a.ANSWER1 = this.answer = answer;

        }

        public answerModel(int answerid, String ansnum, String answer, String correct,String weight, String updatedto, DateTime createdat)
        {
            int ansnumber = 0;
            int correctnumber = 0;
            int updatedtonum = 0;
            int weightnum = 0;

            a.ANSWERID = this.answerid = answerid;

            try { ansnumber = int.Parse(ansnum); }
            catch { ansnumber = 0; };
            a.NUM = this.ansnum = ansnumber;

            a.ANSWER1 = this.answer = answer;

            try { correctnumber = int.Parse(correct); }
            catch { correctnumber = 0; };
            a.CORRECT = this.correct = correctnumber;

            try { weightnum = int.Parse(weight); }
            catch { weightnum = 0; };
            a.WEIGHT = this.weight = weightnum;

            try { updatedtonum = int.Parse(updatedto); }
            catch { updatedtonum = 0; };
            a.UPDATEDTO = this.updatedto = updatedtonum;

            a.CREATEDAT = this.createdat = createdat;

        }

        public answerModel(int answerid, String answer, int correct, int weight, DateTime createdat, DateTime modifiedat, int questionid)
        {
            a.ANSWERID = this.answerid = answerid;
            a.ANSWER1 = this.answer = answer;
            a.CORRECT = this.correct = correct;
            a.CREATEDAT = this.createdat = createdat;
            a.MODIFIEDAT = this.modifiedat = modifiedat;
            a.WEIGHT = this.weight = weight;
            a.QUESTIONID = this.questionid = questionid;
        }

        public answerModel(int answerid, String answer, int correct, int weight, DateTime createdat, int questionid)
        {
            a.ANSWERID = this.answerid = answerid;
            a.ANSWER1 = this.answer = answer;
            a.CORRECT = this.correct = correct;
            a.CREATEDAT = this.createdat = createdat;
            a.MODIFIEDAT = this.modifiedat = modifiedat;
            a.WEIGHT = this.weight = weight;
            a.QUESTIONID = this.questionid = questionid;
        }


        public answerModel(int answerid)
        {
            a.ANSWERID = this.answerid = answerid;
        }

        public answerModel()
        {
        }

        // Retrieves Answers relating to a specified Question
        public List<answerModel> displayAnswers(int questId)
        {
            var query = from a in db.ANSWERs
                        where a.QUESTIONID == questId
                        orderby a.QUESTIONID descending
                        select new answerModel(a.ANSWERID, a.NUM.ToString(), a.ANSWER1, a.CORRECT.ToString(),a.WEIGHT.ToString(), a.UPDATEDTO.ToString(), a.CREATEDAT);
            
            return query.ToList();
        }

        public answerModel getAnswer(int answerid)
        {
            var query = from a in db.ANSWERs
                        where a.ANSWERID == answerid
                        select new answerModel(a.ANSWERID, a.NUM.ToString(), a.ANSWER1, a.CORRECT.ToString(), a.WEIGHT.ToString(), a.UPDATEDTO.ToString(), a.CREATEDAT);

            return query.First();
        }

        public int getMaxID()
        {
            int query = (from a in db.ANSWERs
                         select a.ANSWERID).Max();

            return query;
        }

        public void createAnswer()
        {
            db.ANSWERs.InsertOnSubmit(a);
            db.SubmitChanges();
        }

        public void updateAnswer() {
            try
            {
                db.ANSWERs.InsertOnSubmit(a);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public void deleteAnswer() {
            db.ANSWERs.Attach(a);
            db.ANSWERs.DeleteOnSubmit(a);
            db.SubmitChanges();
        }
    }
}
