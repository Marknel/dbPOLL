using System;
using System.Data;
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

using DBPOLLContext;



namespace DBPOLL.Models
{

    public class userModel : System.Web.UI.Page
    {
        private DBPOLLDataContext db = new DBPOLLDataContext();
        
        private string username;
        private string password;

        public userModel(string username, string password) {
            this.username = username;
            this.password = password;
        }
        
        public bool verify() {
        
            var query = from u in db.USERs where (u.USERNAME == this.username && u.PASSWORD == this.password) select u;

            if (query.ToArray().Length == 1)
            {
                Session["uid"] = query.ToArray()[0].USERID;
                return true;
            }
            else {
                return false;
            }
        }
    }
}
