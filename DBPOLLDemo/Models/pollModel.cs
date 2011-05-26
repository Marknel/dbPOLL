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
        private int pollid;
        private String pollname;

        private int createdby;
        private DateTime createdAt;
        private DateTime expiresat;
        private decimal longitude;
        private decimal latitude;

        //Properties for getters/setters
        public String Name { get { return pollname; } }
        public DateTime CreateDate { get { return createdAt; } }
        public int pollID { get { return (int)pollid; } }




        private DBPOLLDataContext db = new DBPOLLDataContext();



        public pollModel(int pollid, String pollName, decimal longitude, decimal latitude, int createdBy, DateTime createdAt)
        {
            this.pollid = pollid;
            this.pollname = pollName;
            this.longitude = longitude;
            this.latitude = latitude;
            this.createdby = createdBy;
            this.expiresat = DateTime.Now;
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

        /*public List indexPoll()
        {
            return _db.POLLs.ToList();
        }*/

        //public pollModel 

        public List<pollModel> displayPolls(userModel user)
        {
            int sesh = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from u in db.POLLs
                        where u.CREATEDBY == sesh
                        select new pollModel(u.POLLID, u.POLLNAME, u.CREATEDAT);

            /*var query = from u in db.USERs 
                        join m in db.MANAGEMENTs on u.USERID equals m.USERID 
                        join p in db.POLLs on m.POLLID equals p.POLLID
                        select new pollModel(p.POLLNAME, p.CREATEDAT);*/

            // foreach (POLL poll in query)
            //    pollList.Add(poll);



            return query.ToList();

        }

        public void createPoll()
        {
            POLL poll = new POLL();
            poll.POLLID = this.pollid;
            poll.POLLNAME = this.pollname;
            poll.LATITUDE = this.latitude;
            poll.LONGITUDE = this.longitude;
            poll.CREATEDAT = this.createdAt;
            poll.CREATEDBY = this.createdby;

            db.POLLs.InsertOnSubmit(poll);
            db.SubmitChanges();
        }

        public void updatePoll()
        {
            POLL poll = new POLL();
            poll.POLLID = this.pollid;
            poll.POLLNAME = this.pollname;
            poll.LATITUDE = this.latitude;
            poll.LONGITUDE = this.longitude;
            poll.CREATEDAT = this.createdAt;
            poll.CREATEDBY = this.createdby;

            db.SubmitChanges();
        }
        
        public void deletePoll()
        {
            POLL poll = new POLL();
            poll.POLLID = this.pollid;
            poll.POLLNAME = this.pollname;
            poll.LATITUDE = this.latitude;
            poll.LONGITUDE = this.longitude;
            poll.CREATEDAT = this.createdAt;
            poll.CREATEDBY = this.createdby;

            db.POLLs.DeleteOnSubmit(poll);
            db.SubmitChanges();
        }



        public void destroyPoll(POLL poll)
        {


        }

    }
}
