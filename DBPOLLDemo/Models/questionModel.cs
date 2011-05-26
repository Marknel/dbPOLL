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

    public class questionModel
    {
        private QUESTION q = new QUESTION();
        private int questionid;
        private int questiontype;
        private String question;
        private int numberofresponses;
        private int chartstyle;
        private int shortanswertype;
        private int questnum;
        private DateTime createdat;
        private DateTime modifiedat;
        private int pollid;

        public String Question { get { return question; } }
        public DateTime QuestionCreated { get { return createdat; } }
        public int QuestionNumber { get { return questnum; } }
        public int QuestionID { get { return questionid; } }
        public int QuestionType { get { return questiontype; } }



        private static DBPOLLDataContext db = new DBPOLLDataContext();

        // question main constructor
        public questionModel()
        {
        }

        public questionModel(int qid)
        {
            q.QUESTIONID = this.questionid = qid;
        }

        // empty constructer for utility
        //public questionModel()
      // {
           
       //}
        //Constructor for fetched questions
        public questionModel(int qid, int questiontype, String question, int pollid, int questnum, DateTime createdat, DateTime modifiedat, int numberofresponses, int chartstyle)
        {
            q.QUESTIONID = this.questionid = qid;
            q.QUESTIONTYPE = this.questiontype = questiontype;
            q.QUESTION1 = this.question = question;
            q.NUMBEROFRESPONSES = this.numberofresponses = numberofresponses;
            q.CREATEDAT = this.createdat = createdat;
            q.POLLID = this.pollid = pollid;
            q.CHARTSTYLE = this.chartstyle = chartstyle;
            q.SHORTANSWERTYPE = this.shortanswertype;
            q.NUM = this.questnum = questnum;
            q.MODIFIEDAT = this.modifiedat;
        }

        public questionModel(int pollid, int qid, String question, int questiontype, DateTime createdat, int questnum) {
            this.questionid = qid;
            this.question = question;
            this.pollid = pollid;
            this.questiontype = questiontype;
            this.createdat = createdat;
            this.questnum = questnum;
        }

        // Retrieves Question relating to a specified poll
        public List<questionModel> displayQuestions(int poll)
        {
            var query = from q in db.QUESTIONs
                        where q.POLLID == poll
                        select new questionModel(q.POLLID, q.QUESTIONID, q.QUESTION1, q.QUESTIONTYPE, q.CREATEDAT, q.NUM);

            return query.ToList();
        }

        public void createQuestion() {
            db.QUESTIONs.InsertOnSubmit(q);
            db.SubmitChanges();
        }

        public void updateQuestion()
        {
            db.SubmitChanges();
        }

        public void deleteQuestion()
        {
            db.QUESTIONs.Attach(q);
            db.QUESTIONs.DeleteOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
