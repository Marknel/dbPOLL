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
using DBPOLL.Models;

namespace DBPOLL.Models
{
    public class pollModel : System.Web.UI.Page
    {
        POLL poll = new POLL();
        private int pollid;
        private String pollname;

        private int createdby;
        private DateTime createdAt;
        private DateTime expiresat;
        private float longitude;
        private float latitude;

        //Properties for getters/setters
        public String Name { get { return pollname; } }
        public DateTime CreateDate { get { return createdAt; } }
        public int pollID { get { return (int)pollid; } }

        private DBPOLLDataContext db = new DBPOLLDataContext();

        public pollModel(int pollid, String pollName, float longitude, float latitude, int createdBy, DateTime createdAt)
        {
            poll.POLLID = this.pollid = pollid;
            poll.POLLNAME = this.pollname = pollName;
            poll.LATITUDE = this.latitude = latitude;
            poll.LONGITUDE = this.longitude =longitude;
            poll.CREATEDAT = this.createdAt = createdAt;
            poll.CREATEDBY = this.createdby = createdBy;
            poll.EXPIRESAT = DateTime.Today;
        }

        public pollModel(int pollid)
        {
            poll.POLLID = this.pollid = pollid;
        }
        
        public pollModel()
        {

        }

        public pollModel(int pollId, String pollName, DateTime createdAt)
        {
            this.pollid = pollId;
            this.pollname = pollName;
            this.createdAt = createdAt;
        }

        public List<pollModel> displayPolls()
        {
            int sessionID = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from u in db.POLLs
                        where u.CREATEDBY == sessionID
                        select new pollModel(u.POLLID, u.POLLNAME, u.CREATEDAT);
            return query.ToList();
        }

        public void createPoll()
        {
            db.POLLs.InsertOnSubmit(poll);
            db.SubmitChanges();
        }

        public void updatePoll()
        {
            db.SubmitChanges();
        }
        
        public void deletePoll()
        {
            db.POLLs.Attach(poll);
            db.POLLs.DeleteOnSubmit(poll);
            db.SubmitChanges();
        }

    }
}
