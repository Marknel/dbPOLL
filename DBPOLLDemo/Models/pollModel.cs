using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace DBPOLLDemo.Models
{
    public class pollModel : System.Web.UI.Page
    {
        private POLL poll = new POLL();
        private SESSION session = new SESSION();
        public int pollid;
        public String pollname;
        public DateTime modifiedat;
        public int createdby;
        public DateTime createdAt;
        public DateTime? expiresat;
        public decimal longitude;
        public decimal latitude;
        public String createdmaster;
        public String createdcreator1;
        public String sessionName;
        public String syncType;
        public int currentquestion;
        public bool sessionParticipantList;
        public int total;

        public int sessionid;
        public DateTime time;

        public String sessionname;

        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        public pollModel(int pollid, String pollName, String sessionName, decimal longitude, decimal latitude, int createdBy, Nullable<DateTime> expiresat, DateTime createdAt, Nullable<DateTime> modifiedat)
        {
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            this.sessionName = sessionName;
            poll.POLL_ID = this.pollid = pollid;
            poll.POLL_NAME = this.pollname = pollName;
            session.LATITUDE = this.latitude = latitude;
            session.LONGITUDE = this.longitude = longitude;
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
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            poll.POLL_ID = this.pollid = pollid;
            poll.POLL_NAME = this.pollname = name;
        }

        public pollModel(){}

        //pollModel(356672, "TEST", (decimal)76.54, (decimal)2.54, 1, DateTime.Now);

        public pollModel(int pollid, String pollName, decimal longitude, decimal latitude, int createdBy, DateTime createdAt)
        {
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            poll.POLL_ID = this.pollid = pollid;
            poll.POLL_NAME = this.pollname = pollName;
            session.LATITUDE = this.latitude = latitude;
            session.LONGITUDE = this.longitude = longitude;
            poll.CREATED_AT = this.createdAt = createdAt;
            poll.CREATED_BY = this.createdby = createdBy;


        }

        public pollModel(int pollId, String pollName, DateTime createdAt)
        {
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            this.pollid = pollId;
            this.pollname = pollName;
            this.createdAt = createdAt;
        }

        public pollModel(int pollId, int sessionId, String pollName, String sessionName,  decimal longitude, decimal latitude, DateTime time)
        {
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

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
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            int userID = (int)Session["uid"];

            List<POLL> pollList = new List<POLL>();
            var query = from poll in dbpollContext.POLLS
                        join session in dbpollContext.SESSIONS on poll.POLL_ID equals session.POLL_ID
                        join assign in dbpollContext.ASSIGNEDPOLLS on poll.POLL_ID equals assign.POLL_ID
                        where assign.USER_ID == userID
                        orderby poll.POLL_NAME
                        select new pollModel
                        {
                            pollid = poll.POLL_ID,
                            sessionid = session.SESSION_ID,
                            pollname = poll.POLL_NAME,
                            sessionName = session.SESSION_NAME,
                            time = session.SESSION_TIME,
                            longitude = session.LONGITUDE,
                            latitude = session.LATITUDE,
                            sessionParticipantList = false
                        };

           List<pollModel> pollModels = query.ToList();

            foreach(pollModel pollmodel in pollModels){
                pollmodel.sessionParticipantList = new participantModel().AssignedList(pollmodel.sessionid);
            }

            return pollModels;
        }

        public List<pollModel> displayPolls()
        {
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            int userID = (int)Session["uid"];
            
            

            List<POLL> pollList = new List<POLL>();
            if (Int32.Parse(Session["user_type"].ToString()) >= User_Type.POLL_CREATOR)
            {


                var query = from p in dbpollContext.POLLS

                        where p.CREATED_BY == userID //|| ((a.USER_ID == userID) && ((a.POLL_ID == p.POLL_ID) && (p.POLL_ID == s.POLL_ID))))
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
            else if (Int32.Parse(Session["user_type"].ToString()) >= User_Type.POLL_MASTER)
            {
                var q2 = from a in dbpollContext.ASSIGNEDPOLLS
                         where a.USER_ID == userID
                         select a.POLL_ID;

                var query = from p in dbpollContext.POLLS
                            where q2.Contains(p.POLL_ID) //|| ((a.USER_ID == userID) && ((a.POLL_ID == p.POLL_ID) && (p.POLL_ID == s.POLL_ID))))
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
            else
            {
               return new List<pollModel>();
            }
        }

        public List<pollModel> displayAllPolls()
        {
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            int userID = (int)Session["uid"];
            //List<POLL> pollList = new List<POLL>();
            var query = (from p in dbpollContext.POLLS
                         from s in dbpollContext.SESSIONS
                         from it in dbpollContext.USERS
                         where ((p.CREATED_BY == it.USER_ID) && (p.POLL_ID == s.POLL_ID))
                         select new pollModel
                         {
                             pollid = p.POLL_ID,
                             pollname = p.POLL_NAME,
                             longitude = s.LONGITUDE,
                             latitude = s.LATITUDE,
                             createdby = p.CREATED_BY,
                             expiresat = (DateTime)p.EXPIRES_AT,
                             createdAt = p.CREATED_AT,
                             modifiedat = (DateTime)s.SESSION_TIME,
                             createdmaster = it.NAME,
                             sessionname = s.SESSION_NAME,
                             createdcreator1 = (String)(from g in dbpollContext.USERS
                                                        where (g.USER_ID == it.CREATED_BY)
                                                        select g.NAME).FirstOrDefault(),

                             total = (int)(from par in dbpollContext.PARTICIPANTS
                                           where (par.SESSION_ID== s.SESSION_ID)
                                           select par.USER_ID).Count(),            
                         }).OrderBy(p => p.pollname).ThenBy(p => p.createdAt);

        
            return query.ToList(); 
        }

        /// <summary>
        /// Returns poll data of poll with given id.
        /// </summary>
        /// <param name="pollid">Id of the poll</param>
        /// <returns>Poll associated with the given id.</returns>
        public pollModel displayPolls(int pollid)
        {
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            int sessionID = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from p in dbpollContext.POLLS
                        from s in dbpollContext.SESSIONS
                        where (p.CREATED_BY == sessionID && p.POLL_ID == pollid && p.POLL_ID == s.POLL_ID)
                        select new pollModel
                        {
                            pollid = p.POLL_ID,
                            pollname = p.POLL_NAME,
                            longitude = s.LONGITUDE,
                            latitude = s.LATITUDE,
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
            string format = "dd/M/yyyy h:mm tt";
            CultureInfo culture = new CultureInfo("en-AU");
            culture.DateTimeFormat.ShortDatePattern = "d/M/yyyy";
            culture.DateTimeFormat.ShortTimePattern = string.Empty;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            int sessionID = (int)Session["uid"];
            List<POLL> pollList = new List<POLL>();
            var query = from p in dbpollContext.POLLS
                        from s in dbpollContext.SESSIONS
                        where (p.CREATED_BY == sessionID && p.CREATED_AT >= start && p.CREATED_AT <= end && s.POLL_ID==p.POLL_ID)
                        select new pollModel
                        {
                            pollid = p.POLL_ID,
                            pollname = p.POLL_NAME,
                            longitude = s.LONGITUDE,
                            latitude = s.LATITUDE,
                            createdby = p.CREATED_BY,
                            expiresat = (DateTime)p.EXPIRES_AT,
                            createdAt = p.CREATED_AT,
                            modifiedat = (DateTime)p.MODIFIED_AT
                        };


            return query.ToList();
        }

        public int getMaxID()
        {
            int query = (from poll in dbpollContext.POLLS
                         select poll.POLL_ID).Max();
            return query;
        }

        public int getMaxSessionID()
        {
            int query = (from session
                         in dbpollContext.SESSIONS
                         select session.SESSION_ID).Max();

            return query;
        }

        public int getMaxAssignID()
        {
            int query = (from assign
                         in dbpollContext.ASSIGNEDPOLLS
                         select assign.ASSIGNED_ID).Max();

            return query;
        }

        public void createSession(int pollid, String name, decimal latitude, decimal longitude, DateTime time)
        {
            try
            {
                SESSION newSession = new SESSION();

                newSession.SESSION_ID = getMaxSessionID() + 1;
                newSession.SESSION_NAME = name;
                newSession.LATITUDE = latitude;
                newSession.LONGITUDE = longitude;
                newSession.SESSION_TIME = time;
                newSession.POLL_ID = pollid;

                dbpollContext.AddToSESSIONS(newSession);
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

                SESSION editSession = SessionList.First<SESSION>();

                editSession.SESSION_NAME = sessionname;
                editSession.LATITUDE = latitude;
                editSession.LONGITUDE = longitude;
                editSession.SESSION_TIME = parsedDate;

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

                SESSION removeSession = SessionList.First<SESSION>();
                dbpollContext.DeleteObject(removeSession);

                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        /// <summary>
        /// Call to assign a poll to a particualar poll master.
        /// </summary>
        /// <param name="pollid"></param>
        /// <param name="userid"></param>
        public void assignPoll(int pollid, int[] pollMasterId)
        {
            try
            {
                foreach (int id in pollMasterId)
            {

                ASSIGNEDPOLL assignment = new ASSIGNEDPOLL();
                assignment.ASSIGNED_ID = getMaxAssignID() + 1;
                assignment.POLL_ID = pollid;
                assignment.USER_ID = id;

                dbpollContext.AddToASSIGNEDPOLLS(assignment);
                dbpollContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void unassignPollMaster(int pollid, int userid){

            try
            {
                var query =
                from assign in dbpollContext.ASSIGNEDPOLLS
                where assign.POLL_ID == pollid && assign.USER_ID == userid
                select assign;

                ASSIGNEDPOLL a = query.First<ASSIGNEDPOLL>();
                dbpollContext.DeleteObject(a);

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
                p.EXPIRES_AT = expiresat;
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

        public List<pollModel> displayPollsThatContainSessions()
        {
            var query = (from p in dbpollContext.POLLS
                         from s in dbpollContext.SESSIONS

                         where (s.POLL_ID == p.POLL_ID)

                         orderby p.POLL_NAME ascending
                         select new pollModel
                         {
                             pollid = p.POLL_ID,
                             sessionid = s.SESSION_ID,
                             sessionname = s.SESSION_NAME,
                             pollname = p.POLL_NAME,

                             createdAt = p.CREATED_AT,
                         }

                ).Distinct();

            return query.ToList();
        }

        public List<pollModel> displayAssignedSessions(int userid)
        {
            var query = (from p in dbpollContext.PARTICIPANTS
                         from s in dbpollContext.SESSIONS
                         from poll in dbpollContext.POLLS
                         where (
                         p.USER_ID == userid &&
                         p.SESSION_ID == s.SESSION_ID &&
                         s.POLL_ID == poll.POLL_ID
                         )
                         orderby poll.POLL_NAME ascending
                         select new pollModel
                         {
                             pollid = poll.POLL_ID,
                             sessionid = s.SESSION_ID,
                             sessionname = s.SESSION_NAME,
                             pollname = poll.POLL_NAME,
                             expiresat = poll.EXPIRES_AT,
                             currentquestion = s.NEXT_QUESTION,
                             syncType = s.SYNC_TYPE,
                         }

                ).Distinct();

            return query.ToList();
        }

        public List<pollModel> displaySessionDetails(int sessionid)
        {
            var query = (from s in dbpollContext.SESSIONS
                         from p in dbpollContext.POLLS
                         where (
                         s.POLL_ID == p.POLL_ID &&
                         s.SESSION_ID == sessionid
                         )
                         select new pollModel
                         {
                             pollid = p.POLL_ID,
                             pollname = p.POLL_NAME,
                             sessionid = s.SESSION_ID,
                             sessionname = s.SESSION_NAME,
                             expiresat = p.EXPIRES_AT,
                             currentquestion = s.NEXT_QUESTION,
                         }

                ).Distinct();
            
            return query.ToList();
        }

        public Boolean isOpen(int sessionid)
        {
            var query = (from s in dbpollContext.SESSIONS
                         where s.SESSION_ID == sessionid &&
                         s.NEXT_QUESTION != 0
                         select s.SESSION_ID
                        );

            if (query.FirstOrDefault() == null)
            {
                return false;
            }
            return true;
        }
        
    }
}
