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
using System.Globalization;
using System.Threading;
using System.Collections.Generic;

namespace DBPOLLDemo.Models
{

    public class userModel : System.Web.UI.Page
    {
        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        public string username;//changed to public
        private string password;


        private USER user = new USER();
        public String name;
        public int usertype;
        public DateTime createdat;
        public DateTime modifiedat;
        public String createdby;
        public string sysAdmin;

        public DateTime expiredat;

        /// <summary>
        /// Constructor for userModel Object.
        /// </summary>
        /// <param name="username">Username of user </param>
        /// <param name="password">Password of user</param>
        public userModel()
        {
        }

        public userModel(String userName, int userType, DateTime createdAt, Nullable<DateTime> modifiedAt, int createdBy, Nullable<DateTime> expiredAt)
        {

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            user.NAME = this.name = userName;
            user.USER_TYPE = this.usertype = userType;
            user.CREATED_AT = this.createdat = createdAt;

            if (expiredAt.HasValue)
            {
                user.EXPIRES_AT = this.expiredat = expiredAt.Value;
            }


            if (modifiedAt.HasValue)
            {
                user.MODIFIED_AT = this.modifiedat = modifiedAt.Value;
            }
        }

        public List<userModel> displayAllUsers()
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;
            List<USER> userList = new List<USER>();
            var query = from u in dbpollContext.USERS
                        where (u.USER_TYPE >= 0)
                        orderby u.USER_TYPE
                        select new userModel
                        {
                            name = u.NAME,
                            usertype = u.USER_TYPE,
                            createdat = u.CREATED_AT,
                            modifiedat = (DateTime)u.MODIFIED_AT,
                            createdby = (String)(from u1 in dbpollContext.USERS
                                                 where (u1.USER_ID == u.CREATED_BY)
                                                 select u1.NAME).FirstOrDefault(),
                            sysAdmin = (String)(from s1 in dbpollContext.SYSADMINS
                                                where (s1.SYSADMINS_ID == u.SYSADMIN_ID)
                                                select s1.NAME).FirstOrDefault(),
                            expiredat = (DateTime)u.EXPIRES_AT,

                        };


            return query.ToList();
        }


        /// <summary>
        /// Takes a username and password, checks the user exists in the system and returns
        /// the user's userid code.
        /// </summary>
        /// <param name="username">username of user to verify</param>
        /// <param name="password">password of user to verify</param>
        /// <returns>USERID that corresponds to user level in system (ie. 1 == pollUser)
        /// 
        /// Or Returns 0 if user is a system admin</returns>
        public int verify(string username, string password)
        {
            var query = from u in dbpollContext.USERS
                        where (u.USERNAME == username && u.PASSWORD == password)
                        select u;

            if (query.ToArray().Length == 1)
            {
                return query.ToArray()[0].USER_ID;
            }
            else
            {
                return 0;
            }
        }

        public int verify_as_sys_admin(string username, string password)
        {
            var query = from u in dbpollContext.SYSADMINS
                        where (u.USERNAME == username && u.PASSWORD == password)
                        select u;

            if (query.ToArray().Length == 1)
            {
                return query.ToArray()[0].SYSADMINS_ID;
            }
            else
            {
                return 0;
            }
        }


        //Chris added
        private string Salt;
        private string Reset_Password_Key;

        public int UserID;
        public int UserType;
        public string Name;
        public DateTime? Expires_At;//? means it can be null
        public int? Created_By;
        public DateTime Created_At;
        public DateTime Modified_At;
        public int? SysAdmin_ID;

        public userModel(int UserID)
        {
            this.UserID = UserID;
        }
        public List<userModel> displayPollAdminUsers()
        {
            var query = from q in dbpollContext.USERS
                        where q.USER_TYPE == 4
                        orderby q.CREATED_AT ascending
                        select new userModel
                        {
                            UserID = q.USER_ID,
                            UserType = q.USER_TYPE,
                            username = q.USERNAME,
                            Name = q.NAME
                            //SysAdmin_ID = q.SYSADMIN_ID,
                            //Created_By = q.CREATED_BY,
                            //Expires_At = q.EXPIRES_AT
                        };

            return query.ToList();
        }

        public void deleteUser()
        {
            /* To Delete
             * 1. query for object to delete.
             * 2. set User_Type = -1
             * 3. save change.
             */

            var userList =
                from users in dbpollContext.USERS
                where users.USER_ID == this.UserID
                select users;
            USER editobj = userList.First<USER>();

            editobj.USER_TYPE = -1;

            dbpollContext.SaveChanges();
        }

        public userModel getUser(int UserID)
        {
            var query = from q in dbpollContext.USERS
                        where q.USER_ID == UserID
                        select new userModel
                        {
                            UserID = q.USER_ID,
                            UserType = q.USER_TYPE,
                            username = q.USERNAME,
                            Name = q.NAME,
                            SysAdmin_ID = q.SYSADMIN_ID,
                            Created_By = q.CREATED_BY,
                            Expires_At = q.EXPIRES_AT
                        };

            return query.First();
        }

        public void updateUser(int UserID, DateTime Expires_At, string Name, string username)
        {

            /* To Update.
             * 1. Find the object to update using query.
             * 2. pass in values to update from view to model
             * 3. replace values in object.
             * 4. call save on context.
             * 
             * easy as!
             */

            var userList =
            from USERS in dbpollContext.USERS
            where USERS.USER_ID == UserID
            select USERS;

            USER editobj = userList.First<USER>();

            editobj.EXPIRES_AT = Expires_At;
            editobj.NAME = Name;
            editobj.USERNAME = username;
            editobj.USER_ID = UserID;
            editobj.MODIFIED_AT = DateTime.Now;

            dbpollContext.SaveChanges();
        }

        public int getNewID()
        {
            int query = (from q
                         in dbpollContext.USERS
                         select q.USER_ID).Max();
            return query + 1;
        }

        /// <summary>
        /// Returns the user type code for a specified userid. (i.e 1 for poll user)
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int getUserType(int uid)
        {
            var query = -1;
            try
            {
                query = (from q
                             in dbpollContext.USERS
                             where q.USER_ID == uid
                             select q.USER_TYPE).First();
            }
            catch (Exception)
            {
                return query;
            }
            return query;
        }

        public void createUser(int UserID, int UserType, string password, string name, string username, DateTime Expires_At, int SysAdmin_ID)
        {
            try
            {
                USER create = new USER();

                create.USER_ID = UserID;
                create.USER_TYPE = UserType;
                create.PASSWORD = password;
                create.USERNAME = username;
                create.NAME = name;
                create.EXPIRES_AT = Expires_At;
                create.CREATED_AT = DateTime.Now;
                create.MODIFIED_AT = DateTime.Now;
                create.SYSADMIN_ID = SysAdmin_ID;

                dbpollContext.AddToUSERS(create);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void createUser(int UserID, int UserType, string password, string name, string username, int created_by)
        {
            try
            {
                USER create = new USER();

                create.USER_ID = UserID;
                create.USER_TYPE = UserType;
                create.PASSWORD = password;
                create.USERNAME = username;
                create.NAME = name;
                create.CREATED_AT = DateTime.Now;
                create.MODIFIED_AT = DateTime.Now;
                create.CREATED_BY = created_by;

                dbpollContext.AddToUSERS(create);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public USER get_details(int uid)
        {
            var query = from u in dbpollContext.USERS
                        where u.USER_ID == uid
                        select u;
            USER user = query.First();
            return user;
        }

        public SYSADMIN get_sys_admin_details(int uid)
        {
            var query = from u in dbpollContext.SYSADMINS
                        where u.SYSADMINS_ID == uid
                        select u;
            SYSADMIN sysadmin = query.First();
            return sysadmin;
        }
    }
}
