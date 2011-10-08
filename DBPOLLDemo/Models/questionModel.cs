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
        private POLL p = new POLL();
        private SESSION s = new SESSION();
        private ANSWER a = new ANSWER();
        public int questionid;
        public int questiontype;
        public String question;
        public int numberofresponses;
        public int chartstyle;
        public int shortanswertype;
        public int? questnum;
        public DateTime createdat;
        public DateTime modifiedat;
        public int pollid;
        public String pollname;

        // for the answer
        public String answer;
        public int answernum;

        //for session
        public int sessionid;
        public int sessionparticipants;

        public String sessionname;

        public int? participants;
        public int totalparticipants;

        public String Question { get { return question; } }
        public DateTime QuestionCreated { get { return createdat; } }
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

        public questionModel(String pname, String ques, int sid, String ans, int total)
        {
            q.QUESTION1 = this.question = ques;
            p.POLL_NAME= this.pollname = pname;
            s.SESSION_ID = this.sessionid = sid;
            a.ANSWER1 = this.answer = ans;
            this.totalparticipants = total;
        }


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
                            questnum = (int)q.NUM
                        };

            return query.ToList();
        }

        //public List<questionModel> displayQuestionsAnswer()
        //{
        //    var query = (from q in dbpollContext.QUESTIONS
        //                 from p in dbpollContext.POLLS
        //                 from s in dbpollContext.SESSIONS
        //                 where ((p.POLL_ID==q.POLL_ID) && (s.POLL_ID==p.POLL_ID))
        //                 orderby p.POLL_ID ascending
        //                 select new questionModel
        //                 {
        //                     //pollid = p.POLL_ID, 
                             
        //                     //pollname = p.POLL_NAME,
        //                     pollname = (String)(from p1 in dbpollContext.POLLS
        //                                         where (p1.POLL_ID == p.POLL_ID)
        //                                         select p1.POLL_NAME).Distinct().FirstOrDefault(),
        //                     questnum = (int)q.NUM,
        //                     //question = (String)(from q1 in dbpollContext.QUESTIONS
        //                     //                  where (q1.QUESTION_ID == q.QUESTION_ID)
        //                     //                  select q1.QUESTION1).Distinct().FirstOrDefault(),
        //                     question = q.QUESTION1,
        //                     answer = (String)(from a1 in dbpollContext.ANSWERS
        //                                       where (a1.QUESTION_ID == q.QUESTION_ID)
        //                                       select a1.ANSWER1).FirstOrDefault(),
        //                     //sessionid = (int)(from s2 in dbpollContext.SESSIONS
        //                     //                  where (s2.POLL_ID == p.POLL_ID
        //                     //                  select s2.SESSION_ID).Distinct().FirstOrDefault(),)
        //                     sessionid = s.SESSION_ID,
        //                     sessionparticipants = (int)(from s1 in dbpollContext.SESSIONS
        //                                                 from par in dbpollContext.PARTICIPANTS
        //                                                 where ((s1.POLL_ID == p.POLL_ID)&& (par.SESSION_ID==s1.SESSION_ID))
        //                                                 select par.USER_ID).Count(),  
        //                     //answer = a.NUM,
        //                 }
        //                );


        //    return query.ToList();
        //}

        public List<questionModel> displayQuestionsAnswer()
        {
            var query = (from q in dbpollContext.QUESTIONS
                         from p in dbpollContext.POLLS
                         from s in dbpollContext.SESSIONS
                         from a in dbpollContext.ANSWERS
                         where ((p.POLL_ID == q.POLL_ID) && (s.POLL_ID == p.POLL_ID) && a.QUESTION_ID==q.QUESTION_ID)
                         
                         orderby p.POLL_NAME ascending
                         select new questionModel
                         {
                             pollname = (String)(from p1 in dbpollContext.POLLS
                                                 where (p1.POLL_ID == p.POLL_ID)
                                                 select p1.POLL_NAME).Distinct().FirstOrDefault(),

                             question = (String)(from q1 in dbpollContext.QUESTIONS
                                                 where (q1.QUESTION_ID == q.QUESTION_ID)
                                                 //orderby q1.QUESTION_ID
                                                 select q1.QUESTION1).FirstOrDefault(),
                             sessionid = s.SESSION_ID,
                             sessionname = s.SESSION_NAME,
                             

                             //sessionid = (int)(from s2 in dbpollContext.SESSIONS
                             //                  where (s2.POLL_ID == p.POLL_ID)
                             //                  select s2.SESSION_ID).Distinct().FirstOrDefault(),

                             //answer = (String)(from a1 in dbpollContext.ANSWERS
                             //                  where (a1.QUESTION_ID == q.QUESTION_ID)
                             //                  select a1.ANSWER1).GroupBy(a.ANSWER_ID, a.ANSWER_ID),
                             
                             answer = a.ANSWER1,

                             //totalparticipants = (int)(from s1 in dbpollContext.SESSIONS
                             //                          from par in dbpollContext.PARTICIPANTS
                             //                          where ((s1.POLL_ID == p.POLL_ID) && (par.SESSION_ID == s1.SESSION_ID))
                             //                          select par.USER_ID).Count(),  

                             totalparticipants = (int)(from r in dbpollContext.RESPONSES
                                                       where (r.ANSWER_ID==a.ANSWER_ID && r.FEEDBACK==a.ANSWER1)
                                                       select r.USER_ID).Count(),
                         }

                ).Distinct().OrderBy(p => p.pollname).ThenBy(q => q.question).ThenBy(s => s.sessionname);

            return query.ToList();
        }

        

        public List<questionModel> displayAttendance()
        {
            var query = (from s in dbpollContext.SESSIONS
                         from p in dbpollContext.PARTICIPANTS
                         where (s.SESSION_ID == p.SESSION_ID)
                         orderby p.SESSION_ID ascending

                         select new questionModel
                         {
                             sessionname = s.SESSION_NAME,
                             sessionid = p.SESSION_ID,
                             pollname = (String)(from p1 in dbpollContext.POLLS
                                                     where (p1.POLL_ID == s.POLL_ID)
                                                     select p1.POLL_NAME).FirstOrDefault(),
                                                     
                             participants = p.USER_ID,
                             totalparticipants = (int)(from t in dbpollContext.PARTICIPANTS
                                           where (t.SESSION_ID == s.SESSION_ID)
                                           select t.USER_ID).Count(),  

                         }
                        );


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
                                questnum = (int)q.NUM
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
                                questnum = (int)q.NUM
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
                            questnum = (int)q.NUM,
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
                            questnum = (int)q.NUM, 
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

        public void createQuestion(int questiontype, String question, int chartstyle, int questnum, int pollid)
        {
            try
            {

                QUESTION create = new QUESTION();

                create.QUESTION_ID = getMaxID() + 1;
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

        public void updateQuestion(int questionid, int questiontype, String question, int chartstyle, int num, int pollid)
        {

            /* To Update.
             * 1. Find the object to update using query.
             * 2. pass in values to update from view to model
             * 3. replace values in object.
             * 4. call save on context.
             * 
             * easy as!
             */

            try
            {
                var questionList =
                from questions in dbpollContext.QUESTIONS
                where questions.QUESTION_ID == questionid
                select questions;

                QUESTION editobj = questionList.First<QUESTION>();

                editobj.QUESTION_TYPE = questiontype;
                editobj.QUESTION1 = question;
                editobj.CHART_STYLE = chartstyle;
                editobj.NUM = num;
                editobj.MODIFIED_AT = DateTime.Now;
                editobj.POLL_ID = pollid;

                dbpollContext.SaveChanges();
            } catch (Exception e) {
                throw(e);
            }
        }

        public void deleteQuestion()
        {

            /* To Delete
             * 1. query for object to delete.
             * 2. call delete object.
             * 3. save change.
             */

            try {
                var questionList =
                from questions in dbpollContext.QUESTIONS
                where questions.QUESTION_ID == this.questionid
                select questions;

                QUESTION q = questionList.First<QUESTION>();
                dbpollContext.DeleteObject(q);

                dbpollContext.SaveChanges();
            }
            catch (Exception e) {
                throw (e);
            }
        }

        public void createDefaultObjects(int pollid, int questionid) {
            pollObjectModel po = new pollObjectModel();
            questionObjectModel qo = new questionObjectModel();
            List<pollObjectModel> list = po.indexObjects(pollid);

            foreach (pollObjectModel p in list) {
                qo.createObject(p.obid, p.attribute, questionid);
            }
        }
    }
}
