using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBPOLLDemo.Models
{
    public class responseModel : System.Web.UI.Page
    {
        public int responseid;
        public String feedback;
        public DateTime createdat;
        public DateTime modifiedat;
        public int userid;
        public int answerid;
        public int sessionid;
        private DBPOLLEntities dbpollContext = new DBPOLLEntities();

        RESPONS resp = new RESPONS();

        // empty constructor
        public responseModel()
        { 
        
        }

        // full constructor
        public responseModel(int responseid, String feedback, DateTime createdat, DateTime modifiedat, int userid, int answerid)
        {
            resp.RESPONSE_ID = this.responseid = responseid;
            resp.FEEDBACK = this.feedback = feedback;
            resp.CREATED_AT = this.createdat = createdat;
            resp.MODIFIED_AT = this.modifiedat = modifiedat;
            resp.USER_ID = this.userid = userid;
            resp.ANSWER_ID = this.answerid = answerid;
        }

        //public responseModel(int responseid, DateTime createdat, DateTime modifiedat, int userid, int answerid)
        //{
        //    resp.RESPONSE_ID = this.responseid = responseid;
        //    resp.CREATED_AT = this.createdat = createdat;
        //    resp.MODIFIED_AT = this.modifiedat = modifiedat;
        //    resp.USER_ID = this.userid = userid;
        //    resp.ANSWER_ID = this.answerid = answerid;
        //}

        public int getMaxResponseID()
        {
            int query = (from r in dbpollContext.RESPONSES
                         select r.RESPONSE_ID).Max();

            return query;
        }


        public void createMCQResponse(int userid, int answerid, int sessionid)
        {
            try
            {
                RESPONS response = new RESPONS();

                response.FEEDBACK = new answerModel().getFeedback(answerid);
                response.RESPONSE_ID = getMaxResponseID() + 1;
                response.USER_ID = userid;
                response.ANSWER_ID = answerid;
                response.CREATED_AT = DateTime.Now;
                response.MODIFIED_AT = null;
                response.SESSION_ID = sessionid;
                response.PREFERENCE_NUMBER = null;

                dbpollContext.AddToRESPONSES(response);
                dbpollContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw (e);
            }
            }

        public void updateMCQResponse(int responseid, int answerid)
        {
            try
            {
                var getResponse = from r in dbpollContext.RESPONSES
                                  where r.RESPONSE_ID == responseid
                                  select r;


                RESPONS response = getResponse.First<RESPONS>();

                response.RESPONSE_ID = responseid;
                response.ANSWER_ID = answerid;
                response.FEEDBACK = new answerModel().getFeedback(answerid);
                response.MODIFIED_AT = DateTime.Now;
                dbpollContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void createRankingResponse(int userid, int answerid, int sessionid, int preferencenumber)
        {
            try
            {
                RESPONS response = new RESPONS();

                response.FEEDBACK = new answerModel().getFeedback(answerid);
                response.RESPONSE_ID = getMaxResponseID() + 1;
                response.USER_ID = userid;
                response.ANSWER_ID = answerid;
                response.CREATED_AT = DateTime.Now;
                response.MODIFIED_AT = null;
                response.SESSION_ID = sessionid;
                response.PREFERENCE_NUMBER = preferencenumber;
                

                dbpollContext.AddToRESPONSES(response);
                dbpollContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void updateRankingResponse(int responseid, int answerid, int preferencenumber)
        {
            try
            {
                var getResponse = from r in dbpollContext.RESPONSES
                                  where r.RESPONSE_ID == responseid
                                  select r;


                RESPONS response = getResponse.First<RESPONS>();

                response.RESPONSE_ID = responseid;
                response.ANSWER_ID = answerid;
                response.FEEDBACK = new answerModel().getFeedback(answerid);
                response.MODIFIED_AT = DateTime.Now;
                response.PREFERENCE_NUMBER = preferencenumber;
                dbpollContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void createShortAnswerResponse(String feedback, int userid, int sessionid, int questionid)
        {
            try
            {
                RESPONS response = new RESPONS();

                response.FEEDBACK = feedback;
                response.RESPONSE_ID = getMaxResponseID() + 1;
                response.USER_ID = userid;
                response.CREATED_AT = DateTime.Now;
                response.MODIFIED_AT = null;
                response.SESSION_ID = sessionid;
                response.QUESTION_ID = questionid;

                dbpollContext.AddToRESPONSES(response);
                dbpollContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void updateShortAnswerResponse(int responseid, String feedback)
        {
            try
            {
                var getResponse = from r in dbpollContext.RESPONSES
                                  where r.RESPONSE_ID == responseid
                                  select r;


                RESPONS response = getResponse.First<RESPONS>();

                response.RESPONSE_ID = responseid;
                response.FEEDBACK = feedback;
                response.MODIFIED_AT = DateTime.Now;
                dbpollContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public int getResponseId(int sessionid, int userid, String feedback)
        {
            var query = (from r in dbpollContext.RESPONSES
                         where
                            r.SESSION_ID == sessionid &&
                            r.USER_ID == userid &&
                            r.FEEDBACK == feedback
                         select r.RESPONSE_ID
                        );

            return query.First();
        }

        // Get the selected answers from response table (ranking type question)
        public List<int?> getRankingAnswerIds(int sessionid, int userid, int questionid)
        {
            var query = (from r in dbpollContext.RESPONSES
                         from a in dbpollContext.ANSWERS
                         from q in dbpollContext.QUESTIONS
                         where
                            r.ANSWER_ID == a.ANSWER_ID &&
                            a.QUESTION_ID == q.QUESTION_ID &&
                            r.SESSION_ID == sessionid &&
                            r.USER_ID == userid &&
                            r.PREFERENCE_NUMBER != null &&
                            q.QUESTION_ID == questionid
                         orderby r.PREFERENCE_NUMBER ascending
                         select r.ANSWER_ID
                        );

            return query.ToList();
        }

        public int countResponse(int answerid)
        { 
            var query = (from r in dbpollContext.RESPONSES
                         where r.ANSWER_ID == answerid
                         select r.USER_ID).Count();

            return query;
        }
    }
}