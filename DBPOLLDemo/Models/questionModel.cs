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
        public int QuestionType { get { return questiontype; } }



        private static DBPOLLDataContext db = new DBPOLLDataContext();

        // question main constructor
        public questionModel()
        {
        }

        // empty constructer for utility
        //public questionModel()
      // {
           
       //}
        //Constructor for fetched questions
        public questionModel(int pollid, int questnum, String question, int questiontype, DateTime createdate)
        {
            this.pollid = pollid;
            this.questnum = questnum;
            this.question = question;
            this.questiontype = questiontype;
            this.createdat = createdate;

        }

        // Retrieves Question relating to a specified poll
        public List<questionModel> displayAnswers(int poll)
        {
            var query = from q in db.QUESTIONs
                        where q.POLLID == poll
                        select new questionModel(q.POLLID, q.QUESTIONID, q.QUESTION1, q.QUESTIONTYPE, q.CREATEDAT);

            return query.ToList();
        }



    }
}
