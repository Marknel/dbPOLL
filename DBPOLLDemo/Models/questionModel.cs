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
using System.Threading;
using System.Globalization;

namespace DBPOLLDemo.Models
{

    public class questionModel : System.Web.UI.Page
    {
        private QUESTION q = new QUESTION();
        public int questionid;
        public int questiontype;
        public String question;
        public int numberofresponses;
        public int chartstyle;
        public int shortanswertype;
        public int questnum;
        public DateTime createdat;
        public DateTime modifiedat;
        public int pollid;

        public String Question { get { return question; } }
        public DateTime QuestionCreated { get { return createdat; } }
        public int QuestionNumber { get { return questnum; } }
        public int QuestionID { get { return questionid; } }
        public int QuestionType { get { return questiontype; } }

        private DBPOLLDataContext db = new DBPOLLDataContext();

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
        public questionModel(int qid, int questiontype, String question, int numberofresponses,  int chartstyle, int shortanswertype, int questnum, DateTime createdat, DateTime modifiedat, int pollid)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            q.QUESTIONID = this.questionid = qid;
            q.QUESTIONTYPE = this.questiontype = questiontype;
            q.QUESTION1 = this.question = question;
            q.NUMBEROFRESPONSES = this.numberofresponses = numberofresponses;
            q.CHARTSTYLE = this.chartstyle = chartstyle;
            q.SHORTANSWERTYPE = this.shortanswertype = shortanswertype;
            q.NUM = this.questnum = questnum;
            q.CREATEDAT = this.createdat = createdat;
            q.MODIFIEDAT = this.modifiedat = modifiedat;
            q.POLLID = this.pollid = pollid;

        }

        //Question contructor for short answer questions! WILL NOT WORK FOR MULTIPLE CHOICE!!
        public questionModel(int qid, int questiontype, int questnum, String question, int chartstyle, DateTime createdat, int pollid)
        {
            q.QUESTIONID = this.questionid = qid;
            q.QUESTIONTYPE = this.questiontype = questiontype;
            q.QUESTION1 = this.question = question;
            q.CHARTSTYLE = this.chartstyle = chartstyle;
            // IF question is short answer we will need to set type. BORK BORK BORK
            if (questiontype <= 2)
            {
                q.SHORTANSWERTYPE = this.shortanswertype = questiontype;
            }
            q.NUM = this.questnum = questnum;
            q.CREATEDAT = this.createdat = createdat;
            q.POLLID = this.pollid = pollid;

        }

        public questionModel(int pollid, int qid, String question, int questiontype, DateTime createdat, int questnum) {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            q.QUESTIONID = this.questionid = qid;
            q.QUESTION1 = this.question = question;
            q.POLLID = this.pollid = pollid;
            q.QUESTIONTYPE = this.questiontype = questiontype;
            q.CREATEDAT =  this.createdat = createdat;
            q.NUM = this.questnum = questnum;
        }

        public questionModel(int qid, int questiontype, String question,  DateTime createdat, int pollid)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            q.QUESTIONID = this.questionid = qid;
            q.QUESTION1 = this.question = question;
            q.POLLID = this.pollid = pollid;
            q.QUESTIONTYPE = this.questiontype = questiontype;
            q.CREATEDAT = this.createdat = createdat;
            //q.NUM = this.questnum = questnum;
        }
        

        //Used for getting question for editing
        public questionModel(int qid, int questiontype, String question, String chartstyle, String questnum, DateTime createdat, int pollid)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;
            int chart = 0;
            int qnum = 0;


            q.QUESTIONID = this.questionid = qid;
            q.QUESTIONTYPE = this.questiontype = questiontype;
            q.QUESTION1 = this.question = question;

            try { chart = int.Parse(chartstyle); }
            catch { chart = 0; };
            q.CHARTSTYLE = this.chartstyle = chart;

            try { qnum = int.Parse(questnum); }
            catch { qnum = 0; };
            q.NUM = this.questnum = qnum;

            q.CREATEDAT = this.createdat = createdat;
            q.POLLID = this.pollid = pollid;
        }

        public questionModel(int qid, int questiontype, String question, int chartstyle, int questnum, DateTime createdat, DateTime editedat, int pollid)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;
           


            q.QUESTIONID = this.questionid = qid;
            q.QUESTIONTYPE = this.questiontype = questiontype;
            q.QUESTION1 = this.question = question;
            q.CHARTSTYLE = this.chartstyle = chartstyle;
            q.NUM = this.questnum = questnum;
            q.MODIFIEDAT = this.modifiedat = editedat;
            q.CREATEDAT = this.createdat = createdat;
            q.POLLID = this.pollid = pollid;
        }



        // Retrieves Question relating to a specified poll
        public List<questionModel> displayQuestions(int poll)
        {
            var query = from q in db.QUESTIONs
                        where q.POLLID == poll
                        select new questionModel(q.POLLID, q.QUESTIONID, q.QUESTION1, q.QUESTIONTYPE, q.CREATEDAT, q.NUM);

            return query.ToList();
        }

        public List<questionModel> displayQuestions(int poll, DateTime start, DateTime end)
        {
            var query = from q in db.QUESTIONs
                        where q.POLLID == poll && q.CREATEDAT >= start && q.CREATEDAT <= end
                        select new questionModel(q.POLLID, q.QUESTIONID, q.QUESTION1, q.QUESTIONTYPE, q.CREATEDAT, q.NUM);

            return query.ToList();
        }

        public List<questionModel> displayAllQuestions()
        {
            int sessionID = (int)Session["uid"];
            var query = from q in db.QUESTIONs
                        join p in db.POLLs on q.POLLID equals p.POLLID
                        where p.CREATEDBY == sessionID
                        select new questionModel(q.POLLID, q.QUESTIONID, q.QUESTION1, q.QUESTIONTYPE, q.CREATEDAT, q.NUM);

            return query.ToList();
        }




        public questionModel getQuestion(int quid)
        {
            var query = from q in db.QUESTIONs
                         where q.QUESTIONID == quid
                                  select new questionModel(q.QUESTIONID, q.QUESTIONTYPE, q.QUESTION1, q.CHARTSTYLE.ToString(), q.NUM.ToString(), q.CREATEDAT, q.POLLID);

            return query.First();
        }

        public int getMaxID()
        {
            int query = (from q in db.QUESTIONs
                         select q.QUESTIONID).Max();

            return query;
        }

        public void createQuestion() {
            try
            {
                db.QUESTIONs.InsertOnSubmit(q);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
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
