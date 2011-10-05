using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBPOLLDemo.Models
{
    public class participantModel
    {
        private PARTICIPANT part = new PARTICIPANT();

        public int userid;
        public int sessionid;
        public DateTime createdat;
        public DateTime modifiedat;
        public int? userweight;
        public String name;
        public String address;
        public String city;
        public int? postcode;
        public String state;
        public String country;
        public String department;
        public String company;
        public String email;
        public int? fax;
        public int? phone;
        public String title;

        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        /// <summary>
        /// Empty constructor to call methods
        /// </summary>
        public participantModel() { }

        public participantModel(int userid, int sessionid, DateTime createdat, DateTime modifiedat, int? userweight,
            String name, String address, String city, int? postcode, String state, String country, String department, String company,
                String email, int? fax, int? phone, String title) 
        {
            part.USER_ID = this.userid = userid;
            part.SESSION_ID = this.sessionid = sessionid;
            part.CREATED_AT = this.createdat = createdat;
            part.USER_WEIGHT = this.userweight =  userweight;
            part.NAME = this.name =  name;
            part.ADDRESS = this.address = address;
            part.CITY = this.city = city;
            part.POSTCODE = this.postcode = postcode;
            part.STATE = this.state = state;
            part.COUNTRY = this.country = country;
            part.DEPARTMENT = this.department = department;
            part.COMPANY = this.department = company;
            part.EMAIL = this.email = email;
            part.FAX = this.fax = fax;
            part.PHONE = this.phone = phone;
            part.TITLE = this.title = title;
        

        }

        /// <summary>
        /// Returns a list of participants for a given session.
        /// </summary>
        /// <param name="sessionID">Session to get participants for</param>
        /// <returns>A List of participants for the given session</returns>
        public List<participantModel> displayParticipants(int sessionID){

            var participants = from part in dbpollContext.PARTICIPANTS
                               where part.SESSION_ID == sessionID
                               select new participantModel
                               {
                                   userid = part.USER_ID,
                                   sessionid = part.SESSION_ID,
                                   createdat = part.CREATED_AT,
                                   userweight = (int)part.USER_WEIGHT,
                                   name = part.NAME,
                                   address = part.ADDRESS,
                                   city = part.CITY,
                                   postcode = part.POSTCODE,
                                   state = part.STATE,
                                   country = part.COUNTRY,
                                   department = part.DEPARTMENT,
                                   company = part.COMPANY,
                                   email = part.EMAIL,
                                   fax = part.FAX,
                                   phone = part.PHONE,
                                   title = part.TITLE

                               };
            return participants.ToList();
        }

        /// <summary>
        /// Returns a list of unassigned participants for a given session.
        /// </summary>
        /// <param name="sessionID">Session to get participants for</param>
        /// <returns>A List of participants for the given session</returns>
       /* public List<participantModel> displayUnassignedParticipants(int sessionID)
        {
            var users = from parti in dbpollContext.PARTICIPANTS
                        join us in dbpollContext.USERS on parti.USER_ID equals us.USER_ID
                        where parti.USER_ID == us.USER_ID
                        select parti;

            var part = from user in dbpollContext.USERS
                       join p in dbpollContext.PARTICIPANTS on user.USER_ID equals p.USER_ID
                       where

            var participants = from part in dbpollContext.PARTICIPANTS
                               join us in dbpollContext.USERS on part.USER_ID equals us.USER_ID 
                               where part.SESSION_ID == sessionID && part.USER_ID != us.USER_ID
                               select new participantModel
                               {
                                   userid = part.USER_ID,
                                   sessionid = part.SESSION_ID,
                                   createdat = part.CREATED_AT,
                                   userweight = (int)part.USER_WEIGHT,
                                   name = part.NAME,
                                   address = part.ADDRESS,
                                   city = part.CITY,
                                   postcode = part.POSTCODE.GetValueOrDefault(0),
                                   state = part.STATE,
                                   country = part.COUNTRY,
                                   department = part.DEPARTMENT,
                                   company = part.COMPANY,
                                   email = part.EMAIL,
                                   fax = part.FAX.GetValueOrDefault(0),
                                   phone = part.PHONE.GetValueOrDefault(0),
                                   title = part.TITLE

                               };
            return participants.ToList();
        }*/

        /// <summary>
        /// Due to the Number of nullable and not required params. Create and populate a Participant FIRST.
        /// Then run this method.
        /// </summary>
        public void createParticipant()
        {
            try
            {
                dbpollContext.AddToPARTICIPANTS(this.part);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void editParticipant(int userid, int sessionid, DateTime createdat, DateTime modifiedat, int userweight,
            String name, String address, String city, int postcode, String state, String country, String department, String company,
                String email, int? fax, int? phone, String title)
        {
            try
            {
                var ParticipantList =
                    from participant in dbpollContext.PARTICIPANTS
                    where participant.SESSION_ID == sessionid && participant.USER_ID == userid
                    select participant;

                PARTICIPANT part = ParticipantList.First<PARTICIPANT>();

                part.USER_WEIGHT = userweight;
                part.NAME = name;
                part.ADDRESS = address;
                part.CITY = city;
                part.POSTCODE = postcode;
                part.STATE = state;
                part.COUNTRY = country;
                part.DEPARTMENT = department;
                part.COMPANY = company;
                part.EMAIL = email;
                part.FAX = fax;
                part.PHONE = phone;
                part.TITLE = title;

                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }

        }

        public void deleteParticipant(int userid, int sessionid)
        {
            try
            {
                var ParticipantList =
                        from participant in dbpollContext.PARTICIPANTS
                        where participant.SESSION_ID == sessionid && participant.USER_ID == userid
                        select participant;

                PARTICIPANT part = ParticipantList.First<PARTICIPANT>();

                dbpollContext.DeleteObject(part);
                dbpollContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw (e);
            }
        }


    }
}