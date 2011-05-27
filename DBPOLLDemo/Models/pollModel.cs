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
        public int pollid;
        public String pollname;
        public DateTime modifiedat;
        public int createdby;
        public DateTime createdAt;
        public DateTime expiresat;
        public float longitude;
        public float latitude;

        //Properties for getters/setters
        public String Name { get { return pollname; } }
        public DateTime CreateDate { get { return createdAt; } }
        public int pollID { get { return (int)pollid; } }

        private DBPOLLDataContext db = new DBPOLLDataContext();

        public pollModel(int pollid, String pollName, float longitude, float latitude, int createdBy, Nullable<DateTime> expiresat, DateTime createdAt, Nullable<DateTime> modifiedat)
        {
            poll.POLLID = this.pollid = pollid;
            poll.POLLNAME = this.pollname = pollName;
            poll.LATITUDE = this.latitude = latitude;
            poll.LONGITUDE = this.longitude = longitude;
            poll.CREATEDAT = this.createdAt = createdAt;
            if (expiresat != null)
            {
                poll.EXPIRESAT = this.expiresat = expiresat.Value;
            }
            poll.CREATEDBY = this.createdby = createdBy;
            if (expiresat != null){
                poll.MODIFIEDAT = this.modifiedat = modifiedat.Value;
            }
        }

        public pollModel(int pollid)
        {
            poll.POLLID = this.pollid = pollid;
        }
        
        public pollModel(int pollid, String name)
        {
            poll.POLLID = this.pollid = pollid;
            poll.POLLNAME = this.pollname = name;
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
            if (Session["uid"] == null)
            {

            }
            int sessionID = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from u in db.POLLs
                        where u.CREATEDBY == sessionID
                        select new pollModel(u.POLLID, u.POLLNAME, u.LONGITUDE, u.LATITUDE, u.CREATEDBY, u.EXPIRESAT, u.CREATEDAT, u.MODIFIEDAT);
            return query.ToList();
        }

        public List<pollModel> displayPolls(DateTime start, DateTime end)
        {
            int sessionID = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from u in db.POLLs
                        where u.CREATEDBY == sessionID && u.CREATEDAT >= start && u.CREATEDAT <= end
                        select new pollModel(u.POLLID, u.POLLNAME, u.LONGITUDE, u.LATITUDE, u.CREATEDBY, u.EXPIRESAT, u.CREATEDAT, u.MODIFIEDAT);
            return query.ToList();
        }

        public void createPoll()
        {
            db.POLLs.Attach(poll);
            db.POLLs.InsertOnSubmit(poll);
            db.SubmitChanges();
        }

        public void updatePoll()
        {
            try
            {
                db.POLLs.Attach(poll);
                db.SubmitChanges();
            }
            catch(Exception e)
            {
                throw (e);
            }
        }
        
        public void deletePoll()
        {
            db.POLLs.Attach(poll);
            db.POLLs.DeleteOnSubmit(poll);
            db.SubmitChanges();
        }
    }
}
