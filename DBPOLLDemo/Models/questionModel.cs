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

        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context

        /// <summary>
        /// Empty Question Constructor
        /// </summary>
        public questionModel()
        {
        }

        public questionModel(int qid)
        {
            q.QUESTION_ID = this.questionid = qid;
        }

        // empty constructer for utility
        //public questionModel()
      // {
           
       //}
        //Constructor for fetched QUESTIONS
        public questionModel(int qid, int questiontype, String question, int numberofresponses,  int chartstyle, int shortanswertype, int questnum, DateTime createdat, DateTime modifiedat, int pollid)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            q.QUESTION_ID = this.questionid = qid;
            q.QUESTION_TYPE = this.questiontype = questiontype;
            q.QUESTION1 = this.question = question;
            q.NUMBER_OF_RESPONSES = this.numberofresponses = numberofresponses;
            q.CHART_STYLE = this.chartstyle = chartstyle;
            q.SHORT_ANSWER_TYPE = this.shortanswertype = shortanswertype;
            q.NUM = this.questnum = questnum;
            q.CREATED_AT = this.createdat = createdat;
            q.MODIFIED_AT = this.modifiedat = modifiedat;
            q.POLL_ID = this.pollid = pollid;

        }

        //Question contructor for short answer QUESTIONS! WILL NOT WORK FOR MULTIPLE CHOICE!!
        public questionModel(int qid, int questiontype, int questnum, String question, int chartstyle, DateTime createdat, int pollid)
        {
            q.QUESTION_ID = this.questionid = qid;
            q.QUESTION_TYPE = this.questiontype = questiontype;
            q.QUESTION1 = this.question = question;
            q.CHART_STYLE = this.chartstyle = chartstyle;
            // IF question is short answer we will need to set type. BORK BORK BORK
            if (questiontype <= 2)
            {
                q.SHORT_ANSWER_TYPE = this.shortanswertype = questiontype;
            }
            q.NUM = this.questnum = questnum;
            q.CREATED_AT = this.createdat = createdat;
            q.POLL_ID = this.pollid = pollid;

        }

        public questionModel(int pollid, int qid, String question, int questiontype, DateTime createdat, int questnum) {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            q.QUESTION_ID = this.questionid = qid;
            q.QUESTION1 = this.question = question;
            q.POLL_ID = this.pollid = pollid;
            q.QUESTION_TYPE = this.questiontype = questiontype;
            q.CREATED_AT =  this.createdat = createdat;
            q.NUM = this.questnum = questnum;
        }

        public questionModel(int qid, int questiontype, String question,  DateTime createdat, int pollid)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            q.QUESTION_ID = this.questionid = qid;
            q.QUESTION1 = this.question = question;
            q.POLL_ID = this.pollid = pollid;
            q.QUESTION_TYPE = this.questiontype = questiontype;
            q.CREATED_AT = this.createdat = createdat;
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


            q.QUESTION_ID = this.questionid = qid;
            q.QUESTION_TYPE = this.questiontype = questiontype;
            q.QUESTION1 = this.question = question;

            try { chart = int.Parse(chartstyle); }
            catch { chart = 0; };
            q.CHART_STYLE = this.chartstyle = chart;

            try { qnum = int.Parse(questnum); }
            catch { qnum = 0; };
            q.NUM = this.questnum = qnum;

            q.CREATED_AT = this.createdat = createdat;
            q.POLL_ID = this.pollid = pollid;
        }

        public questionModel(int qid, int questiontype, String question, int chartstyle, int questnum, DateTime createdat, DateTime editedat, int pollid)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;
           
            q.QUESTION_ID = this.questionid = qid;
            q.QUESTION_TYPE = this.questiontype = questiontype;
            q.QUESTION1 = this.question = question;
            q.CHART_STYLE = this.chartstyle = chartstyle;
            q.NUM = this.questnum = questnum;
            q.MODIFIED_AT = this.modifiedat = editedat;
            q.CREATED_AT = this.createdat = createdat;
            q.POLL_ID = this.pollid = pollid;
        }


        // Retrieves Question relating to a specified poll
        public List<questionModel> displayQuestions(int poll)
        {
            var query = from q in dbpollContext.QUESTIONS
                        where q.POLL_ID == poll
                        orderby q.CREATED_AT ascending
                        select new questionModel
                        {
                            pollid = q.POLL_ID,
                            questionid = q.QUESTION_ID,
                            question = q.QUESTION1, 
                            questiontype = q.QUESTION_TYPE, 
                            createdat = q.CREATED_AT, 
                            questnum = q.NUM
                        };

            return query.ToList();
        }

        public List<questionModel> displayQuestions(int poll, DateTime start, DateTime end)
        {
            
            if (poll == 0)
            {
                var query = from q in dbpollContext.QUESTIONS
                            where q.CREATED_AT >= start && q.CREATED_AT <= end
                            orderby q.CREATED_AT ascending
                            select new questionModel
                            {
                                pollid = q.POLL_ID,
                                questionid = q.QUESTION_ID,
                                question = q.QUESTION1, 
                                questiontype = q.QUESTION_TYPE, 
                                createdat = q.CREATED_AT, 
                                questnum = q.NUM
                            };
                return query.ToList();

            }
            else
            {
                var query = from q in dbpollContext.QUESTIONS
                            where q.POLL_ID == poll && q.CREATED_AT >= start && q.CREATED_AT <= end
                            orderby q.CREATED_AT ascending
                            select new questionModel
                            {
                                pollid = q.POLL_ID,
                                questionid = q.QUESTION_ID,
                                question = q.QUESTION1,
                                questiontype = q.QUESTION_TYPE,
                                createdat = q.CREATED_AT,
                                questnum = q.NUM
                            };
                return query.ToList();
            }
        }

        public List<questionModel> displayAllQuestions()
        {
            int sessionID = (int)Session["uid"];
            var query = from q in dbpollContext.QUESTIONS
                        join p in dbpollContext.POLLS on q.POLL_ID equals p.POLL_ID
                        where p.CREATED_BY == sessionID
                        orderby q.CREATED_AT ascending
                        select new questionModel
                        {
                            pollid = q.POLL_ID,
                            questionid = q.QUESTION_ID,
                            question = q.QUESTION1,
                            questiontype = q.QUESTION_TYPE,
                            createdat = q.CREATED_AT,
                            questnum = q.NUM
                        };

            return query.ToList();
        }

        public questionModel getQuestion(int quid)
        {
            var query = from q in dbpollContext.QUESTIONS
                        where q.QUESTION_ID == quid
                        select new questionModel
                        {
                            questionid = q.QUESTION_ID, 
                            questiontype = q.QUESTION_TYPE, 
                            question = q.QUESTION1, 
                            chartstyle = (int)q.CHART_STYLE, 
                            questnum = q.NUM, 
                            createdat = q.CREATED_AT, 
                            pollid = q.POLL_ID
                        };

            return query.First();
        }

        public int getMaxID()
        {
            int query = (from q 
                         in dbpollContext.QUESTIONS
                         select q.QUESTION_ID).Max();

            return query;
        }

        public void createQuestion(int qid, int questiontype, String question, int chartstyle, int questnum, int pollid)
        {
            try
            {

                QUESTION create = new QUESTION();

                create.QUESTION_ID = qid;
                create.QUESTION_TYPE = questiontype;
                create.NUM = questnum;
                create.QUESTION1 = question;
                create.CHART_STYLE = chartstyle;
                create.CREATED_AT = DateTime.Now;
                create.MODIFIED_AT = DateTime.Now;
                create.POLL_ID = pollid;

                dbpollContext.AddToQUESTIONS(create);

                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void updateQuestion(int questionid, int questiontype, String question, int chartstyle, int num, DateTime modifiedat, int pollid)
        {

            /* To Update.
             * 1. Find the object to update using query.
             * 2. pass in values to update from view to model
             * 3. replace values in object.
             * 4. call save on context.
             * 
             * easy as!
             */


            var questionList =
            from questions in dbpollContext.QUESTIONS
            where questions.QUESTION_ID == this.questionid
            select questions;

            QUESTION editobj = questionList.First<QUESTION>();

            editobj.QUESTION_TYPE = questiontype;
            editobj.QUESTION1 = question;
            editobj.CHART_STYLE = chartstyle;
            editobj.NUM = num;
            editobj.MODIFIED_AT = modifiedat;
            editobj.POLL_ID = pollid;

            dbpollContext.SaveChanges();
        }

        public void deleteQuestion()
        {

            /* To Delete
             * 1. query for object to delete.
             * 2. call delete object.
             * 3. save change.
             */

            var questionList =
            from questions in dbpollContext.QUESTIONS
            where questions.QUESTION_ID == this.questionid
            select questions;

            foreach (var question in questionList)
            {
                dbpollContext.DeleteObject(question);
            }

            dbpollContext.SaveChanges();


           //dbpollContext.QUESTIONS.Attach(q);
            //dbpollContext.QUESTIONS.DeleteOnSubmit(q);
            //dbpollContext.SubmitChanges();
        }
    }
}
