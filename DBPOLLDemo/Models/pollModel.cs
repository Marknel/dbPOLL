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
using DBPOLLDemo.Models;

namespace DBPOLL.Models
{
    public class pollModel : System.Web.UI.Page
    {
        private POLL poll = new POLL();
        public int pollid;
        public String pollname;
        public DateTime modifiedat;
        public int createdby;
        public DateTime createdAt;
        public DateTime expiresat;
        public decimal longitude;
        public decimal latitude;

        //Properties for getters/setters
        public String Name { get { return pollname; } }
        public DateTime CreateDate { get { return createdAt; } }
        public int pollID { get { return (int)pollid; } }

        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        public pollModel(int pollid, String pollName, decimal longitude, decimal latitude, int createdBy, Nullable<DateTime> expiresat, DateTime createdAt, Nullable<DateTime> modifiedat)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;
            
            poll.POLL_ID = this.pollid = pollid;
            poll.POLL_NAME = this.pollname = pollName;
            poll.LATITUDE = this.latitude = latitude;
            poll.LONGITUDE = this.longitude = longitude;
            poll.CREATED_AT = this.createdAt = createdAt;
            if (expiresat != null)
            {
                poll.EXPIRES_AT = this.expiresat = expiresat.Value;
            }
            poll.CREATED_BY = this.createdby = createdBy;
            if (modifiedat != null)
            {
                poll.MODIFIED_AT = this.modifiedat = modifiedat.Value;
            }
        }

        public pollModel(int pollid)
        {
            poll.POLL_ID = this.pollid = pollid;
        }
        
        public pollModel(int pollid, String name)
        {

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            poll.POLL_ID = this.pollid = pollid;
            poll.POLL_NAME = this.pollname = name;
        }
        
        public pollModel()
        {

        }

        //pollModel(356672, "TEST", (decimal)76.54, (decimal)2.54, 1, DateTime.Now);
        
        public pollModel(int pollid, String pollName, decimal longitude, decimal latitude, int createdBy, DateTime createdAt)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            poll.POLL_ID = this.pollid = pollid;
            poll.POLL_NAME = this.pollname = pollName;
            poll.LATITUDE = this.latitude = latitude;
            poll.LONGITUDE = this.longitude = longitude;
            poll.CREATED_AT = this.createdAt = createdAt;
            poll.CREATED_BY = this.createdby = createdBy;

        }

        public pollModel(int pollId, String pollName, DateTime createdAt)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;
            
            this.pollid = pollId;
            this.pollname = pollName;
            this.createdAt = createdAt;
        }

        /// <summary>
        /// Returns all polls in the database which have been created by the searching user.
        /// </summary>
        /// <returns>List of polls associated with the user</returns>
        public List<pollModel> displayPolls()
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            int sessionID = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from p in dbpollContext.POLLS
                        where p.CREATED_BY == sessionID
                        select new pollModel
                        {
                            pollid = p.POLL_ID, 
                            pollname = p.POLL_NAME, 
                            longitude = p.LONGITUDE, 
                            latitude = p.LATITUDE, 
                            createdby = p.CREATED_BY, 
                            expiresat = (DateTime)p.EXPIRES_AT,
                            createdAt = p.CREATED_AT,
                            modifiedat = (DateTime)p.MODIFIED_AT
                        };


            return query.ToList();
        }

        /// <summary>
        /// Returns poll data of poll with given id.
        /// </summary>
        /// <param name="pollid">Id of the poll</param>
        /// <returns>Poll associated with the given id.</returns>
        public pollModel displayPolls(int pollid)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            int sessionID = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from p in dbpollContext.POLLS
                        where p.CREATED_BY == sessionID && p.POLL_ID == pollid
                        select new pollModel
                        {
                            pollid = p.POLL_ID, 
                            pollname = p.POLL_NAME, 
                            longitude = p.LONGITUDE, 
                            latitude = p.LATITUDE, 
                            createdby = p.CREATED_BY, 
                            expiresat = (DateTime)p.EXPIRES_AT,
                            createdAt = p.CREATED_AT,
                            modifiedat = (DateTime)p.MODIFIED_AT
                        };
            return query.First();
        }

        /// <summary>
        /// Returns polls between two dates.
        /// </summary>
        /// <param name="start">Date to search from</param>
        /// <param name="end">Date to finish search at</param>
        /// <returns>List of polls between given dates</returns>
        public List<pollModel> displayPolls(DateTime start, DateTime end)
        {

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            int sessionID = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from p in dbpollContext.POLLS
                        where p.CREATED_BY == sessionID && p.CREATED_AT >= start && p.CREATED_AT <= end
                        select new pollModel
                        {
                        pollid = p.POLL_ID, 
                        pollname = p.POLL_NAME, 
                        longitude = p.LONGITUDE, 
                        latitude = p.LATITUDE, 
                        createdby = p.CREATED_BY, 
                        expiresat = (DateTime)p.EXPIRES_AT,
                        createdAt = p.CREATED_AT,
                        modifiedat = (DateTime)p.MODIFIED_AT  
                        };
            return query.ToList();
        }

        public void createPoll()
        {
            /*
            dbpollContext.POLLS.Attach(poll);
            dbpollContext.POLLS.InsertOnSubmit(poll);
            dbpollContext.SubmitChanges();
            */
        }

        public void updatePoll()
        {
            try
            {
                //pollModel poll1 = new pollModel(pollid, pollname).displayPolls(pollid);
                //poll1.POLLNAME = "CHANGE";
                //db.POLLS.Attach(poll);
                //poll.POLL_NAME = "HELLO";
                /*
                dbpollContext.SubmitChanges();
                 */
            }
            catch(Exception e)
            {
                throw (e);
            }
        }
        
        public void deletePoll()
        {
            /*
            dbpollContext.POLLS.Attach(poll);
            dbpollContext.POLLS.DeleteOnSubmit(poll);
            dbpollContext.SubmitChanges();
             */
        }
    }
}
