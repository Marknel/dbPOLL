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

        //Properties for getters/setters
        public String Answer { get { return answer; } }
        public int AnswerNumber { get { return ansnum; } }
        //public int pollID { get { return pollid; } }

        public answerModel(int answerid, String answer, int correct, int weight, int questnum, int updatedto, DateTime createdat)
        {
            this.answerid = answerid;
            this.answer = answer;
            this.correct = correct;
            this.weight = weight;
            this.ansnum = questnum;
            this.updatedto = updatedto;
            this.createdat = createdat;

        }

        public answerModel(int ansnum, String answer)
        {
            this.ansnum = ansnum;
            this.answer = answer;

        }

        public answerModel()
        {
        }

        // Retrieves Answers relating to a specified Question
        public List<answerModel> displayAnswers(int questId)
        {
            var query = from a in db.ANSWERs
                        where a.QUESTIONID == questId
                        select new answerModel((int)a.ANSWERID, a.ANSWER1);
            
            return query.ToList();
        }

        public void createAnswer(POLL poll)
        {
            ANSWER answer = new ANSWER();
            answer.ANSWERID = this.answerid;
            //answer.ANSWER1 = this.answer;
            answer.CORRECT = this.correct;
            answer.WEIGHT = this.weight;
            answer.NUM = this.ansnum;
            answer.UPDATEDTO = this.updatedto;
            answer.CREATEDAT = this.createdat;


            db.ANSWERs.InsertOnSubmit(answer);
            db.SubmitChanges();
 
        }



    }
}
