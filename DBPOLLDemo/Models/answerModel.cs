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
using DBPOLLDemo.Models;

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
        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        ANSWER a = new ANSWER();

        //Properties for getters/setters
        public String Answer { get { return answer; } }
        public int AnswerNumber { get { return ansnum; } }
        public int AnswerID { get { return answerid; } }
        //public int pollID { get { return pollid; } }

        public answerModel(int answerid, String answer, int correct, int weight, int ansnum, int updatedto, DateTime createdat)
        {
            a.ANSWER_ID = this.answerid = answerid;
            a.ANSWER1 = this.answer = answer;
            a.CORRECT = this.correct = correct;
            a.WEIGHT = this.weight = weight;
            a.NUM = this.ansnum = ansnum;
            a.UPDATED_TO = this.updatedto;
            a.CREATED_AT = this.createdat;
           
        }

        public answerModel(int answerid, String ansnum, String answer)
        {
            int ansnumber = 0;
            a.ANSWER_ID = this.answerid = answerid;

            try { ansnumber = int.Parse(ansnum); }
            catch { ansnumber = 0; };
            a.NUM = this.ansnum = ansnumber;

            a.ANSWER1 = this.answer = answer;

        }

        public answerModel(int answerid, string ansnum, String answer, String correct,String weight, String updatedto, DateTime createdat)
        {
            int ansnumber = 0;
            int correctnumber = 0;
            int updatedtonum = 0;
            int weightnum = 0;

            a.ANSWER_ID = this.answerid = answerid;
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
            a.UPDATED_TO = this.updatedto = updatedtonum;

            a.CREATED_AT = this.createdat = createdat;
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
            a.ANSWER_ID = this.answerid = answerid;
            a.ANSWER1 = this.answer = answer;
            a.CORRECT = this.correct = correct;
            a.CREATED_AT = this.createdat = createdat;
            a.MODIFIED_AT = this.modifiedat = modifiedat;
            a.WEIGHT = this.weight = weight;
            a.QUESTION_ID = this.questionid = questionid;
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
            a.ANSWER_ID = this.answerid = answerid;
            a.ANSWER1 = this.answer = answer;
            a.CORRECT = this.correct = correct;
            a.CREATED_AT = this.createdat = createdat;
            a.WEIGHT = this.weight = weight;
            a.QUESTION_ID = this.questionid = questionid;
        }


        public answerModel(int answerid)
        {
            a.ANSWER_ID = this.answerid = answerid;
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
                        where a.QUESTION_ID == questId && a.ANSWER_ID== a.UPDATED_TO
                        orderby a.NUM ascending
                        select new answerModel
                        {
                            answerid = a.ANSWER_ID,
                            ansnum = (int)a.NUM, 
                            answer = a.ANSWER1, 
                            correct = (int)a.CORRECT,
                            weight = (int)a.WEIGHT, 
                            updatedto = (int)a.UPDATED_TO, 
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
                                updatedto = (int)a.UPDATED_TO, 
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
                ANSWER a = new ANSWER();

                a.ANSWER_ID = getMaxID() + 1;
                a.ANSWER1 = answer;
                a.CORRECT = correct;
                a.WEIGHT = weight;
                a.NUM = ansnum;
                a.UPDATED_TO = a.ANSWER_ID; //Every answer created will be the latest version of that answer
                a.CREATED_AT = DateTime.Now;
                a.MODIFIED_AT = DateTime.Now;
                a.QUESTION_ID = qid;

                dbpollContext.AddToANSWERS(a);
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
                ANSWER a = answerList.First<ANSWER>();
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
                a.ANSWER1 = answer;
                a.CORRECT = correct;
                a.WEIGHT = weight;
                a.NUM = ansnum;
                a.UPDATED_TO = getMaxID() + 1;
                a.MODIFIED_AT = DateTime.Now;
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
