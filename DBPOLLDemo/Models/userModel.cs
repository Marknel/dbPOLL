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

namespace DBPOLL.Models
{

    public class userModel : System.Web.UI.Page
    {
        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        /// <summary>
        /// Constructor for userModel Object.
        /// </summary>
        /// <param name="username">Username of user </param>
        /// <param name="password">Password of user</param>
        public userModel() {
        }
        
        public int verify(string username, string password) {
            var query = from u in dbpollContext.USERS 
                           where (u.USERNAME == username && u.PASSWORD == password) 
                           select u;

            if ( query.ToArray().Length == 1 ) {
                return query.ToArray()[0].USER_ID;
            } else {
                return 0;
            }
        }

        public USER get_details (int uid)
        {
            var query = from u in dbpollContext.USERS
                        where u.USER_ID == uid
                        select u;
            USER user = query.First();
            return user;
        }
    }
}
