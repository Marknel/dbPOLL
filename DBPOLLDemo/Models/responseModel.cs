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

        public responseModel(int responseid, DateTime createdat, DateTime modifiedat, int userid, int answerid)
        {
            resp.RESPONSE_ID = this.responseid = responseid;
            resp.CREATED_AT = this.createdat = createdat;
            resp.MODIFIED_AT = this.modifiedat = modifiedat;
            resp.USER_ID = this.userid = userid;
            resp.ANSWER_ID = this.answerid = answerid;
        }

        public void createResponse(int responseid, int userid, int answerid)
        {
            try
            {
                RESPONS response = new RESPONS();

                response.RESPONSE_ID = responseid;
                response.USER_ID = userid;
                response.ANSWER_ID = answerid;
                response.CREATED_AT = DateTime.Now;
                response.MODIFIED_AT = null;

                dbpollContext.AddToRESPONSES(response);
                dbpollContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void updateResponse(int responseid, int userid, int answerid)
        {
            try
            {
                var getResponse = from r in dbpollContext.RESPONSES
                                  where r.RESPONSE_ID == responseid
                                  select r;

                RESPONS response = new RESPONS();

                response.FEEDBACK = feedback;
                response.USER_ID = userid;

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

    }
}