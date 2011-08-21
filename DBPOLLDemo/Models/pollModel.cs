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

namespace DBPOLLDemo.Models
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
        public decimal? longitude;
        public decimal? latitude;
        public String createdmaster;
        public String createdcreator1;
        public String sessionName;
        public int total;
        public int sessionid;
        public DateTime time;
        

        //Properties for getters/setters
        public String Name { get { return pollname; } }
        public DateTime CreateDate { get { return createdAt; } }
        public int pollID { get { return (int)pollid; } }

        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        public pollModel(int pollid, String pollName, String sessionName, decimal longitude, decimal latitude, int createdBy, Nullable<DateTime> expiresat, DateTime createdAt, Nullable<DateTime> modifiedat)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;
            this.sessionName = sessionName;
            poll.POLL_ID = this.pollid = pollid;
            poll.POLL_NAME = this.pollname = pollName;
            poll.LATITUDE = this.latitude = latitude;
            poll.LONGITUDE = this.longitude = longitude;
            poll.CREATED_AT = this.createdAt = createdAt;
            //this.createdmaster = dbpollContext.



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

        public pollModel(int sessionid, int y)
        {
            this.sessionid = sessionid;
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

        public pollModel(int pollId, int sessionId, String pollName, String sessionName,  decimal longitude, decimal latitude, DateTime time)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            this.pollid = pollId;
            this.sessionid = sessionId;
            this.pollname = pollName;
            this.latitude = latitude;
            this.sessionName = sessionName;
            this.longitude = longitude;
            this.time = time;

        }

        /// <summary>
        /// Returns all polls in the database which have been created by the searching user.
        /// </summary>
        /// <returns>List of polls associated with the user</returns>
        /// 

        public List<pollModel> displayPollSessions()
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            int sessionID = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from p in dbpollContext.POLLS
                        from s in dbpollContext.SESSIONS
                        where (p.POLL_ID == s.POLL_ID) && p.CREATED_BY == sessionID
                        select new pollModel
                        {
                            pollid = p.POLL_ID,
                            sessionid = s.SESSION_ID,
                            pollname = p.POLL_NAME,
                            sessionName = s.SESSION_NAME,
                            time = s.SESSION_TIME,
                            longitude = s.LONGITUDE,
                            latitude = s.LATITUDE,
                        };


            return query.ToList();
        }

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
                            createdby = p.CREATED_BY,
                            expiresat = (DateTime)p.EXPIRES_AT,
                            createdAt = p.CREATED_AT,
                            modifiedat = (DateTime)p.MODIFIED_AT
                        };


            return query.ToList();
        }

        public List<pollModel> displayAllPolls()
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;


            int sessionID = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from p in dbpollContext.POLLS
                        from it in dbpollContext.USERS
                        where (p.CREATED_BY == it.USER_ID)
                        select new pollModel
                        {
                            pollid = p.POLL_ID,
                            pollname = p.POLL_NAME,
                            longitude = p.LONGITUDE,
                            latitude = p.LATITUDE,
                            createdby = p.CREATED_BY,
                            expiresat = (DateTime)p.EXPIRES_AT,
                            createdAt = p.CREATED_AT,
                            modifiedat = (DateTime)p.MODIFIED_AT,
                            createdmaster = it.NAME,
                            createdcreator1 = (String)(from g in dbpollContext.USERS
                                                       where (g.USER_ID == it.CREATED_BY)
                                                       select g.NAME).FirstOrDefault(),
                            //total = (int)(from par in dbpollContext.PARTICIPANTS
                                         // where (par.POLL_ID==p.POLL_ID)
                                       //   select par.USER_ID).Count(),            
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

        public int getMaxID()
        {
            int query = (from p
                         in dbpollContext.POLLS
                         select p.POLL_ID).Max();

            return query;
        }

        public int getMaxSessionID()
        {
            int query = (from s
                         in dbpollContext.SESSIONS
                         select s.SESSION_ID).Max();

            return query;
        }

        public void createSession(int pollid, String name, decimal latitude, decimal longitude, DateTime time)
        {
            try
            {
                SESSION s = new SESSION();

                s.SESSION_ID = getMaxSessionID() + 1;
                s.SESSION_NAME = name;
                s.LATITUDE = latitude;
                s.LONGITUDE = longitude;
                s.SESSION_TIME = time;
                s.POLL_ID = pollid;

                dbpollContext.AddToSESSIONS(s);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void editSession(String sessionname, int sessionid, decimal latitude, decimal longitude, DateTime parsedDate)
        {
            try
            {
                var SessionList =
                from Session in dbpollContext.SESSIONS
                where Session.SESSION_ID == sessionid
                select Session;

                SESSION s = SessionList.First<SESSION>();

                s.SESSION_NAME = sessionname;
                s.LATITUDE = latitude;
                s.LONGITUDE = longitude;
                s.SESSION_TIME = parsedDate;

                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void deleteSession()
        {
            try
            {
                var SessionList =
                from Session in dbpollContext.SESSIONS
                where Session.SESSION_ID == sessionid
                select Session;

                SESSION s = SessionList.First<SESSION>();
                dbpollContext.DeleteObject(s);

                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void createPoll(String pollName, int createdBy, Nullable<DateTime> expiresat)
        {
            try
            {
                POLL p = new POLL();

                p.POLL_ID = getMaxID() + 1;
                p.POLL_NAME = pollName;
                p.CREATED_BY = createdBy;
                p.CREATED_AT = DateTime.Now;

                dbpollContext.AddToPOLLS(p);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void updatePoll(int pollid, String pollName)
        {
            try
            {
                var pollList =
                from polls in dbpollContext.POLLS
                where polls.POLL_ID == pollid
                select polls;

                POLL p = pollList.First<POLL>();

                p.POLL_NAME = pollName;
                p.MODIFIED_AT = DateTime.Now;

                dbpollContext.SaveChanges();
            }
            catch (Exception e) {
                throw (e);
            }
        }

        public void deletePoll()
        {
            try
            {
                var pollList =
                from polls in dbpollContext.POLLS
                where polls.POLL_ID == pollid
                select polls;

                POLL p = pollList.First<POLL>();
                dbpollContext.DeleteObject(p);

                dbpollContext.SaveChanges();
            }
            catch (Exception e) { 
                throw (e);
            }
        }
    }
}
