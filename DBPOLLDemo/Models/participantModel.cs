using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public List<userModel> displayUnassignedParticipants(int sessionid)
        {
            var assignedUsers = from part in dbpollContext.PARTICIPANTS
                                where part.SESSION_ID == sessionid
                                select part.USER_ID;

            var unassignedUsers = from user in dbpollContext.USERS
                                  where !assignedUsers.Contains(user.USER_ID) && (user.USER_TYPE == User_Type.POLL_USER || user.USER_TYPE == User_Type.KEYPAD_USER)
                                  select new userModel
                                  {
                                      UserID = user.USER_ID,
                                      UserType = user.USER_TYPE,
                                      username = user.USERNAME,
                                      Name = user.NAME
                                  };

            return unassignedUsers.ToList();
        }

        /// <summary>
        ///  Seperates and creates participants based on a collection of values collected
        ///  from the Modify.aspx page.
        /// </summary>
        /// <param name="collection">Collection of userid values and sessionid.</param>
        public void createParticipantsFromCollection(FormCollection collection){

            //Turn the FormCollection csv string into a list of ints.
            List<int> useridList = new List<int>();

            foreach (string item in collection["selectedObjects"].Split(','))
            {
                useridList.Add(int.Parse(item));
            }

            foreach (int userid in useridList)
            {
                new participantModel().createParticipant(userid, int.Parse(collection["sessionid"]));
            }
        }

        /// <summary>
        /// Create Participant and try to migrate some details from user table.
        /// </summary>
        public void createParticipant(int userid,int sessionid)
        {
            try
            {
                USER userData = (from user in dbpollContext.USERS
                                where user.USER_ID == userid
                                select user).First<USER>();


                PARTICIPANT part = new PARTICIPANT();
                part.USER_ID = userid;
                part.SESSION_ID = sessionid;
                part.NAME = userData.NAME;

                dbpollContext.AddToPARTICIPANTS(part);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        /// <summary>
        /// Returns null if the string is Empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns>null if the given string is empty else the string.</returns>
        private String nullCheck(String s){

            if (s == String.Empty)
            {
                return null;
            }
            return s;
        }

        public void editParticipantDataFromCollection(FormCollection collection)
        {
            int position = 0;
            int sessionID = int.Parse(collection["sessionid"]);
            int id = 0;
            int? nweight = null;
            int? npostcode = null;
            int? nfax = null;
            int? nphone = null;


            String[] UserID = collection["userid"].Split(',');
            String[] Title = collection["titletxt"].Split(',');
            String[] Name = collection["nametxt"].Split(',');
            String[] Email = collection["emailtxt"].Split(',');
            String[] Fax = collection["faxtxt"].Split(',');
            String[] Phone = collection["phonetxt"].Split(',');
            String[] Address = collection["addresstxt"].Split(',');
            String[] City = collection["citytxt"].Split(',');
            String[] Postcode = collection["postcodetxt"].Split(',');
            String[] State = collection["statetxt"].Split(',');
            String[] Country = collection["countrytxt"].Split(',');
            String[] Department = collection["departmenttxt"].Split(',');
            String[] Company = collection["companytxt"].Split(',');
            String[] Weight = collection["weight"].Split(',');

            foreach(String userid in UserID)
            {
                id = int.Parse(userid);
                nweight = null; 
                npostcode = null; 
                nfax = null; 
                nphone = null; 

                // Check if the int values are nulls. if not we can convert to proper int?.
                if(Weight[position] != String.Empty){nweight = int.Parse(Weight[position]);}
                if(Postcode[position] != String.Empty){npostcode = int.Parse(Postcode[position]);}
                if(Fax[position] != String.Empty){nfax = int.Parse(Fax[position]);}
                if(Phone[position] != String.Empty){nphone = int.Parse(Phone[position]);}


                // Need to check each text field as we don't want to store empty strings in the db when there should be NULLs
                editParticipant(id, sessionID, nweight, nullCheck(Name[position]), nullCheck(Address[position]), nullCheck(City[position]), npostcode, nullCheck(State[position]),
                    nullCheck(Country[position]), nullCheck(Department[position]), nullCheck(Company[position]), nullCheck(Email[position]), nfax, nphone, nullCheck(Title[position]));

                position++;
            }

        }

        public void editParticipant(int userid, int sessionid, int? userweight, String name, String address, String city, int? postcode, 
            String state, String country, String department, String company, String email, int? fax, int? phone, String title)
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

        /// <summary>
        /// Removes participant allocation from database
        /// </summary>
        /// <param name="userid">userid of participant to remove</param>
        /// <param name="sessionid">removes participant from particular allocation</param>
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