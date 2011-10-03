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
    public class answerModel : System.Web.UI.Page
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
        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        ANSWER ans = new ANSWER();

        public answerModel(int answerid, String answer, int correct, int weight, int ansnum, int updatedto, DateTime createdat)
        {
            ans.ANSWER_ID = this.answerid = answerid;
            ans.ANSWER1 = this.answer = answer;
            ans.CORRECT = this.correct = correct;
            ans.WEIGHT = this.weight = weight;
            ans.NUM = this.ansnum = ansnum;
            //ans.UPDATED_TO = this.updatedto;
            ans.CREATED_AT = this.createdat;
           
        }

        public answerModel(int answerid, String ansnum, String answer)
        {
            int ansnumber = 0;
            ans.ANSWER_ID = this.answerid = answerid;

            try { ansnumber = int.Parse(ansnum); }
            catch { ansnumber = 0; };
            ans.NUM = this.ansnum = ansnumber;

            ans.ANSWER1 = this.answer = answer;

        }

        public answerModel(int answerid, string ansnum, String answer, String correct,String weight, String updatedto, DateTime createdat)
        {
            int ansnumber = 0;
            int correctnumber = 0;
            int updatedtonum = 0;
            int weightnum = 0;

            ans.ANSWER_ID = this.answerid = answerid;
            try { ansnumber = int.Parse(ansnum); }
            catch { ansnumber = 0; };
            ans.NUM = this.ansnum = ansnumber;

            ans.ANSWER1 = this.answer = answer;

            try { correctnumber = int.Parse(correct); }
            catch { correctnumber = 0; };
            ans.CORRECT = this.correct = correctnumber;

            try { weightnum = int.Parse(weight); }
            catch { weightnum = 0; };
            ans.WEIGHT = this.weight = weightnum;

            try { updatedtonum = int.Parse(updatedto); }
            catch { updatedtonum = 0; };
            //ans.UPDATED_TO = this.updatedto = updatedtonum;

            ans.CREATED_AT = this.createdat = createdat;
        }

        /// <summary>
        /// Full Constructor for answerModel
        /// </summary>
        /// <param name="answerid"></param>
        /// <param name="answer"></param>
        /// <param name="correct"></param>
        /// <param name="weight"></param>
        /// <param name="createdat"></param>
        /// <param name="modifiedat"></param>
        /// <param name="questionid"></param>
        public answerModel(int answerid, String answer, int correct, int weight, DateTime createdat, DateTime modifiedat, int questionid)
        {
            ans.ANSWER_ID = this.answerid = answerid;
            ans.ANSWER1 = this.answer = answer;
            ans.CORRECT = this.correct = correct;
            ans.CREATED_AT = this.createdat = createdat;
            ans.MODIFIED_AT = this.modifiedat = modifiedat;
            ans.WEIGHT = this.weight = weight;
            ans.QUESTION_ID = this.questionid = questionid;
        }

        /// <summary>
        /// Constructor for 5 Argument answer
        /// </summary>
        /// <param name="answerid"></param>
        /// <param name="answer"></param>
        /// <param name="correct"></param>
        /// <param name="weight"></param>
        /// <param name="createdat"></param>
        /// <param name="questionid"></param>
        public answerModel(int answerid, String answer, int correct, int weight, DateTime createdat, int questionid)
        {
            ans.ANSWER_ID = this.answerid = answerid;
            ans.ANSWER1 = this.answer = answer;
            ans.CORRECT = this.correct = correct;
            ans.CREATED_AT = this.createdat = createdat;
            ans.WEIGHT = this.weight = weight;
            ans.QUESTION_ID = this.questionid = questionid;
        }


        public answerModel(int answerid)
        {
            ans.ANSWER_ID = this.answerid = answerid;
        }

        public answerModel()
        {
        }

        /// <summary>
        /// Retrieves ANSWERS relating to a specified Question
        /// </summary>
        /// <param name="questId">ID of the question</param>
        /// <returns>A List of Answers</returns>
        public List<answerModel> displayAnswers(int questId)
        {
            var query = from a in dbpollContext.ANSWERS
                        where a.QUESTION_ID == questId && a.ANSWER_ID == 1
                        //a.UPDATED_TO   FIX ME ANDREW OMG. I AM BROKEN BECAUSE OF YOU! WHAT HAVE YOU DONE!   
                        orderby a.NUM ascending

                        select new answerModel
                        {
                            answerid = a.ANSWER_ID,
                            ansnum = (int)a.NUM, 
                            answer = a.ANSWER1, 
                            correct = (int)a.CORRECT,
                            weight = (int)a.WEIGHT, 
                            updatedto = 1,//(int)a.UPDATED_TO,a.UPDATED_TO   FIX ME ANDREW OMG. I AM BROKEN BECAUSE OF YOU! WHAT HAVE YOU DONE!   
                            createdat = a.CREATED_AT
                        };
            /**
            List<answerModel> answers = query.ToList();

            foreach (answerModel answer in answers) 
            {
                if (answer.AnswerID != answer.updatedto) 
                {
                    answers.Remove(answer);
                }
            }

            return answers;
            **/
            return query.ToList();
        }

        /// <summary>
        /// Rereives detailes about a specific answer
        /// </summary>
        /// <param name="answerid">ID of the requested answer</param>
        /// <returns>AnswerModel containing all relevent data</returns>
        public answerModel getAnswer(int answerid)
        {
            var query = from a in dbpollContext.ANSWERS
                        where a.ANSWER_ID == answerid
                        select new answerModel
                            {
                                answerid = a.ANSWER_ID,
                                ansnum = (int)a.NUM, 
                                answer = a.ANSWER1, 
                                correct = (int)a.CORRECT,
                                weight = (int)a.WEIGHT,
                                updatedto =  1,//(int)a.UPDATED_TO,a.UPDATED_TO   FIX ME ANDREW OMG. I AM BROKEN BECAUSE OF YOU! WHAT HAVE YOU DONE!   
                                createdat = a.CREATED_AT
                            };

            return query.First();
        }

        public int getMaxID()
        {
            int query = (from a in dbpollContext.ANSWERS
                         select a.ANSWER_ID).Max();

            return query;
        }

        public void createAnswer(String answer, int correct, int weight, int ansnum, int qid)
        {
            try
            {
                ANSWER ans = new ANSWER();

                ans.ANSWER_ID = getMaxID() + 1;
                ans.ANSWER1 = answer;
                ans.CORRECT = correct;
                ans.WEIGHT = weight;
                ans.NUM = ansnum;
                //ans.UPDATED_TO = ans.ANSWER_ID; //Every answer created will be the latest version of that answer
                ans.CREATED_AT = DateTime.Now;
                ans.MODIFIED_AT = DateTime.Now;
                ans.QUESTION_ID = qid;

                dbpollContext.AddToANSWERS(ans);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void updateAnswer(int answerid, String answer, int correct, int weight, int ansnum)
        {
            try
            {
                var answerList =
                from answers in dbpollContext.ANSWERS   
                where answers.ANSWER_ID == answerid
                select answers;


                /* If an answer is updated, it is archived and points to a new answer with the same properties and updated answer field, 
                 * pointed by the field updatedto in the archived record
                 */
                ANSWER ans = answerList.First<ANSWER>();
                /**
                if (!(a.ANSWER1.Equals(answer)))
                {
                    
                    answerModel newanswer = new answerModel();
                    newanswer.createAnswer(answer, correct, weight, ansnum, questionid);
                    a.UPDATED_TO = newanswer.AnswerID; 
                }
                else {
                    a.CORRECT = correct;
                    a.WEIGHT = weight;
                    a.NUM = ansnum;
                    a.UPDATED_TO = getMaxID() + 1;
                    a.MODIFIED_AT = DateTime.Now;
                }
                **/
                ans.ANSWER1 = answer;
                ans.CORRECT = correct;
                ans.WEIGHT = weight;
                ans.NUM = ansnum;
                //ans.UPDATED_TO = getMaxID() + 1;
                ans.MODIFIED_AT = DateTime.Now;
                dbpollContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void deleteAnswer() {
            try
            {
                var answerList =
                from answers in dbpollContext.ANSWERS
                where answers.ANSWER_ID == answerid
                select answers;

                ANSWER a = answerList.First<ANSWER>();
                dbpollContext.DeleteObject(a);

                dbpollContext.SaveChanges();
            }
            catch (Exception e) {
                throw (e);
            }
        }
    }
}
