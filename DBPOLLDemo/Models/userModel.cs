using System;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Configuration;
using System.Linq;
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

namespace DBPOLL.Models
{

    public class userModel : System.Web.UI.Page
    {
        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.
        
        private string username;
        private string password;

        private USER user = new USER();
        public String name;
        public int usertype;
        public DateTime createdat;
        public DateTime modifiedat;
        public String createdby;
  
        public DateTime expiredat;



        /// <summary>
        /// Constructor for userModel Object.
        /// </summary>
        /// <param name="username">Username of user </param>
        /// <param name="password">Password of user</param>
        public userModel(string username, string password) {
            this.username = username;
            this.password = password;
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

        public userModel()
        {
            // TODO: Complete member initialization
        }

        public List<userModel> displayAllUsers()
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            ci = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentCulture = ci;            
            List<USER> userList = new List<USER>();
            var query = from u in dbpollContext.USERS
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
                            expiredat = (DateTime)u.EXPIRES_AT,
                        };

            return query.ToList();
        }
        
        public bool verify() {

            var query = from u in dbpollContext.USERS 
                           where (u.USERNAME == this.username && u.PASSWORD == this.password) 
                           select u;

            //var query = from u in db.USERs where (u.USERNAME == this.username && u.PASSWORD == this.password) select u; << OLD LINQ QUERY

            if (query.ToArray().Length == 1)
            {
                Session["uid"] = query.ToArray()[0].USER_ID;
                return true;
            }
            else {
                return false;
            }
        }
    }
}
