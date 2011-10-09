using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Security;
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;

namespace DBPOLLDemo.Models
{
    public class userModel : System.Web.UI.Page
    {
        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.


        public string username;//changed to public

        private USER user = new USER();

        private string password;
        private string Salt;
        public string Reset_Password_Key;

        public String name;
        public int usertype;
        public DateTime createdat;
        public DateTime modifiedat;
        public String createdby;
        public string sysAdmin;

        public DateTime expiredat;
        public int UserID;
        public int UserType;
        public string Name;
        public DateTime Expires_At;
        public int monthsLeft;

        public int Created_By;
        public DateTime Created_At;
        public DateTime Modified_At;
        public int SysAdmin_ID;


        public userModel()
        {
        }

        public userModel(int UserID)
        {
            this.UserID = UserID;
        }

        /// <summary>
        /// Returns a set of all poll admin users in the database
        /// </summary>
        /// <returns></returns>
        public List<userModel> displayPollAdminUsers()
        {
            var query = from q in dbpollContext.USERS
                        where q.USER_TYPE == User_Type.POLL_ADMINISTRATOR
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
                            SysAdmin_ID = (int)q.SYSADMIN_ID,
                            Created_By = (int)q.CREATED_BY,
                            Expires_At = (DateTime)q.EXPIRES_AT,
                            Reset_Password_Key = q.RESET_PASSWORD_KEY
                        };
            userModel user = query.ToList()[0];
            //only Poll Administrators have expiries, 
            //these expiries can only be modified by System Administrators
            if (user.UserType == 4)
            {
                int total = (user.Expires_At - DateTime.Now).Days;
                user.monthsLeft = total / 30;
            }
            return user;
        }

        /// <summary>
        /// Takes a username and password, checks the user exists in the system and returns
        /// the user's userid code.
        /// </summary>
        /// <param name="username">username of user to verify</param>
        /// <param name="password">password of user to verify</param>
        /// <returns>USERID that corresponds to user
        /// or returns 0 if they do not exist</returns>
        public int verify(string username, string password)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;
            string Salt = "";
            string hshdpwd = "";

            userModel user = null;
            try
            {
                var userList = from u in dbpollContext.USERS
                               where (u.USERNAME == username)
                               where u.USER_TYPE != -1
                               select new userModel
                               {
                                   UserID = u.USER_ID,
                                   Salt = u.SALT,
                                   password = u.PASSWORD,
                                   Reset_Password_Key = u.RESET_PASSWORD_KEY
                               };

                user = userList.ToList()[0];
            }
            catch (Exception e)
            {
                return 0;
            }
            Salt = user.Salt;
            hshdpwd = CreatePasswordHash(password, Salt);

            //compared hashed password from input, against password from db
            if (hshdpwd.Equals(user.password))
                return user.UserID;
            else
                return 0;
        }

        public int verify_as_sys_admin(string username, string password)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;
            string Salt = "";
            string hshdpwd = "";

            userModel user = null;
            try
            {
                var userList = from u in dbpollContext.SYSADMINS
                               where (u.USERNAME == username)
                               select new userModel
                               {
                                   UserID = u.SYSADMINS_ID,
                                   Salt = u.SALT,
                                   password = u.PASSWORD
                               };

                user = userList.ToList()[0];
            }
            catch (Exception e)
            {
                return 0;
            }
            Salt = user.Salt;
            hshdpwd = CreatePasswordHash(password, Salt);

            //compared hashed password from input, against password from db
            if (hshdpwd.Equals(user.password))
                return user.UserID;
            else
                return 0;


            //var query = from u in dbpollContext.SYSADMINS
            //            where (u.USERNAME == username && u.PASSWORD == password)
            //            select u;

            //if (query.ToArray().Length == 1)
            //{
            //    return query.ToArray()[0].SYSADMINS_ID;
            //}
            //else
            //{
            //    return 0;
            //}
        }

        /// <summary>
        /// Takes a username and checks the user exists in the system and returns
        /// the user's userid code.
        /// </summary>
        /// <param name="username">username of user to verify</param>
        /// <returns>USERID that corresponds to user
        /// or returns 0 if they do not exist</returns>
        public int verify(string username)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            userModel user = null;
            try
            {
                var query = from u in dbpollContext.USERS
                            where (u.USERNAME == username)
                            select new userModel
                            {
                                UserID = u.USER_ID
                            };

                user = query.ToList()[0];
                return user.UserID;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public void updateUser(int UserID, DateTime Expires_At, string Name, string username)
        {
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

        public void updateUser(int UserID, string Name, string username)
        {
            var userList =
            from USERS in dbpollContext.USERS
            where USERS.USER_ID == UserID
            select USERS;


            USER editobj = userList.First<USER>();

            editobj.NAME = Name;
            editobj.USERNAME = username;
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
        /// Returns a set of all poll masters in database.
        /// </summary>
        /// <returns></returns>
        public List<userModel> displayPollMasterUsers()
        {
            var query = from q in dbpollContext.USERS
                        where q.USER_TYPE == User_Type.POLL_MASTER
                        orderby q.CREATED_AT ascending
                        select new userModel
                        {
                            UserID = q.USER_ID,
                            UserType = q.USER_TYPE,
                            username = q.USERNAME,
                            Name = q.NAME
                        };

            return query.ToList();
        }

        /// <summary>
        /// Returns a list of poll masters who have been assigned 
        /// to a given poll.
        /// List generic is userModel
        /// </summary>
        /// <param name="pollid"></param>
        /// <returns></returns>
        public List<userModel> displayAssignedUsers(int pollid, int usertype)
        {
            var query2 = from a in dbpollContext.ASSIGNEDPOLLS
                         where a.POLL_ID == pollid
                         select a.USER_ID;


            var query = from q in dbpollContext.USERS
                        where q.USER_TYPE == usertype && query2.Contains(q.USER_ID)
                        orderby q.CREATED_AT ascending
                        select new userModel
                        {
                            UserID = q.USER_ID,
                            UserType = q.USER_TYPE,
                            username = q.USERNAME,
                            Name = q.NAME
                        };

            return query.ToList();
        }

        /// <summary>
        /// Returns a list of poll masters who have not been assigned
        /// to a given poll.
        /// List generic is userModel
        /// </summary>
        /// <param name="pollid"></param>
        /// <returns></returns>
        public List<userModel> displayUnassignedUsers(int pollid, int usertype)
        {
            var assignedUsers = from a in dbpollContext.ASSIGNEDPOLLS
                         where a.POLL_ID == pollid
                         select a.USER_ID;


            var unassignedUsers = from user in dbpollContext.USERS
                                  where user.USER_TYPE == usertype && !assignedUsers.Contains(user.USER_ID)
                        orderby user.CREATED_AT ascending
                        select new userModel
                        {
                            UserID = user.USER_ID,
                            UserType = user.USER_TYPE,
                            username = user.USERNAME,
                            Name = user.NAME
                        };

            return unassignedUsers.ToList();
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




        public void changePassword(int UserID, string newPass)
        {
            string Salt = CreateSalt(10);
            string hshdpwd = CreatePasswordHash(newPass, Salt);


            var userList =
            from USERS in dbpollContext.USERS
            where USERS.USER_ID == UserID
            select USERS;

            USER editobj = userList.First<USER>();

            editobj.PASSWORD = hshdpwd;
            editobj.SALT = Salt;
            editobj.MODIFIED_AT = DateTime.Now;
            editobj.RESET_PASSWORD_KEY = null;

            dbpollContext.SaveChanges();
        }


        public string Password_Generator()
        {
            Random rand = new Random();
            string s = "";
            for (int i = 0; i < 8; i++)
            {
                s += rand.Next(10);
            }

            string pass = s.ToString();
            return pass;
        }

        //Returns a salt of the given size
        public string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        //Returns the hashed password
        public string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
             FormsAuthentication.HashPasswordForStoringInConfigFile(
             saltAndPwd, "sha1");

            return hashedPwd;
        }

        private bool userExist(string email)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            var query = from u in dbpollContext.USERS
                        where (u.USERNAME == email)
                        select u;

            if (query.ToArray().Length >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool sysAdminExist(string email)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;

            var query = from u in dbpollContext.SYSADMINS
                        where (u.USERNAME == email)
                        select u;

            if (query.ToArray().Length >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public bool createUser(int UserID, int UserType, string password, string name, string email, DateTime Expires_At, int SysAdmin_ID)
        {
            if (sysAdminExist(email))
                return false;
            string Salt = CreateSalt(10);
            string hshdpwd = CreatePasswordHash(password, Salt);

            try
            {
                USER create = new USER();

                create.USER_ID = UserID;
                create.USER_TYPE = UserType;
                create.PASSWORD = hshdpwd;
                create.SALT = Salt;
                create.USERNAME = email;
                create.NAME = name;
                create.EXPIRES_AT = Expires_At;
                create.CREATED_AT = DateTime.Now;
                create.MODIFIED_AT = DateTime.Now;
                create.SYSADMIN_ID = SysAdmin_ID;
                create.RESET_PASSWORD_KEY = "Created";

                dbpollContext.AddToUSERS(create);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
            return true;
        }

        public bool createUser(int UserID, int UserType, string password, string name, string email, int created_by)
        {
            if (userExist(email))
                return false;
            string Salt = CreateSalt(10);
            string hshdpwd = CreatePasswordHash(password, Salt);

            try
            {
                USER create = new USER();

                create.USER_ID = UserID;
                create.USER_TYPE = UserType;
                create.PASSWORD = hshdpwd;
                create.SALT = Salt;
                create.USERNAME = email;
                create.NAME = name;
                create.CREATED_AT = DateTime.Now;
                create.MODIFIED_AT = DateTime.Now;
                create.CREATED_BY = created_by;
                create.RESET_PASSWORD_KEY = "Created";

                dbpollContext.AddToUSERS(create);
                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
            return true;
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



        public List<userModel> getUserList()
        {
            var query = from u in dbpollContext.USERS
                        orderby u.NAME ascending
                        select new userModel
                        {
                            name = u.NAME + ":\t" + u.USERNAME,
                            UserID = u.USER_ID
                        };

            return query.ToList();
        }




    }
}
