using System;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DBPOLLDemo.Models;
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;
using Microsoft.VisualBasic;

namespace DBPOLLDemo.Models
{
    public class messageModel : System.Web.UI.Page
    {
        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        public int MessageID;
        public string Message;
        public DateTime Created_at;
        public DateTime Modified_at;
        public int sender_UID;
        public string senderName;
        public string recieverName;
        public int poll_ID;
        public int question_ID;



        public messageModel()
        {
        }
        public int getNewID()
        {
            int query = (from q
                         in dbpollContext.MESSAGES
                         select q.MESSAGE_ID).Max();
            return query + 1;
        }

        public bool sendMessage(string message, int sender, int pollID, int reciever)
        {
            try
            {
                MESSAGE msg = new MESSAGE();

                msg.MESSAGE_ID = getNewID();
                msg.MESSAGE1 = message;
                msg.CREATED_AT = DateTime.Now;
                msg.Sender_UID = sender;
                msg.POLL_ID = pollID;
                msg.Reciever_UID = reciever;

                dbpollContext.AddToMESSAGES(msg);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
            return true;
        }

        public bool sendFeedback(string message, int sender, int pollID, int question_ID)
        {
            //do query for the assigned poll master(s) using the pollID
            var query = from q in dbpollContext.ASSIGNEDPOLLS
                        where q.POLL_ID == poll_ID
                        select new userModel
                        {
                            UserID = (int)q.USER_ID
                        };
            List<userModel> userList = query.ToList();


            foreach (userModel usr in userList)
            {
                try
                {
                    MESSAGE msg = new MESSAGE();

                    msg.MESSAGE_ID = getNewID();
                    msg.MESSAGE1 = message;
                    msg.CREATED_AT = DateTime.Now;
                    msg.Sender_UID = sender;
                    msg.Reciever_UID = usr.UserID;
                    msg.POLL_ID = pollID;
                    msg.QUESTION_ID = question_ID;


                    dbpollContext.AddToMESSAGES(msg);
                    dbpollContext.SaveChanges();
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
            return true;
        }

        public bool sendPublicMessage(string message, int sender, int pollID, int question_ID)
        {
            try
            {
                MESSAGE msg = new MESSAGE();

                msg.MESSAGE_ID = getNewID();
                msg.MESSAGE1 = message;
                msg.CREATED_AT = DateTime.Now;
                msg.Sender_UID = sender;
                msg.Reciever_UID = 0;
                msg.POLL_ID = pollID;
                msg.QUESTION_ID = question_ID;


                dbpollContext.AddToMESSAGES(msg);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }

            return true;
        }


        public List<messageModel> sentMessages(int userID)
        {
            var query = from q in dbpollContext.MESSAGES
                        where q.Sender_UID == userID
                        orderby q.CREATED_AT ascending
                        select new messageModel
                        {
                            MessageID = q.MESSAGE_ID,
                            Message = q.MESSAGE1,
                            Created_at = q.CREATED_AT,
                            Modified_at = (DateTime)q.MODIFIED_AT,
                            sender_UID = q.sender.USER_ID,
                            poll_ID = q.POLL_ID,
                            senderName = q.sender.NAME,
                            recieverName = q.reciever.NAME
                        };

            return query.ToList();
        }

        public List<messageModel> recievedMessages(int userID)
        {
            var query = from q in dbpollContext.MESSAGES
                        where q.Reciever_UID == userID
                        orderby q.CREATED_AT ascending
                        select new messageModel
                        {
                            MessageID = q.MESSAGE_ID,
                            Message = q.MESSAGE1,
                            Created_at = q.CREATED_AT,
                            Modified_at = (DateTime)q.MODIFIED_AT,
                            sender_UID = q.sender.USER_ID,
                            poll_ID = q.POLL_ID,
                            senderName = q.sender.NAME,
                            recieverName = q.reciever.NAME
                        };

            return query.ToList();
        }

        public List<messageModel> publicMessages()
        {
            var query = from q in dbpollContext.MESSAGES
                        where q.Reciever_UID == 0
                        orderby q.CREATED_AT ascending
                        select new messageModel
                        {
                            MessageID = q.MESSAGE_ID,
                            Message = q.MESSAGE1,
                            Created_at = q.CREATED_AT,
                            Modified_at = (DateTime)q.MODIFIED_AT,
                            sender_UID = q.sender.USER_ID,
                            poll_ID = q.POLL_ID,
                            senderName = q.sender.NAME,
                            recieverName = q.reciever.NAME
                        };

            return query.ToList();
        }












    }
}