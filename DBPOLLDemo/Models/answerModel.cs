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
        private int answerid;
	    private String answer;
	    private int correct;
	    private int weight;
	    private int ansnum;
	    private int updatedto;
        private DateTime createdat;
        private DBPOLLDataContext db = new DBPOLLDataContext();
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

        public answerModel(int answerid, Nullable<int> ansnum, String answer)
        {
            this.answerid = answerid;
            if (ansnum != null)
            {
                this.ansnum = ansnum.Value;
            }
            this.answer = answer;

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
                        select new answerModel(a.ANSWERID, a.NUM, a.ANSWER1);
            
            return query.ToList();
        }

        public void createAnswer(POLL poll)
        {
            db.ANSWERs.InsertOnSubmit(a);
            db.SubmitChanges();
        }

        public void updateAnswer() {
            db.SubmitChanges();
        }

        public void deleteAnswer() {
            db.ANSWERs.Attach(a);
            db.ANSWERs.DeleteOnSubmit(a);
            db.SubmitChanges();
        }
    }
}
