using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DBPOLLContext;
using DBPOLL.Models;
using System.Threading;
using System.Globalization;

namespace DBPOLLDemo.Models
{
    public class objectModel : System.Web.UI.Page
    {
        private OBJECT ob = new OBJECT();
        public int obid;
        public int obtype;
        public String attribute;
        public int questionid;

        private DBPOLLDataContext db = new DBPOLLDataContext();

        public objectModel(int obid, int obtype, String attribute, int questionid)
        {
            ob.OBJID = this.obid = obid;
            ob.OBJTYPE = this.obtype = obtype;
            ob.ATTRIBUTE = this.attribute = attribute;
            ob.QUESTIONID = this.questionid = questionid;
        }

        public objectModel()
        {
            
        }

        public objectModel(int obid)
        {
            ob.OBJID = this.obid = obid;
        }

        public objectModel(int obtype, String attribute, int questionid)
        {
            ob.OBJTYPE = this.obtype = obtype;
            ob.ATTRIBUTE = this.attribute = attribute;
            ob.QUESTIONID = this.questionid = questionid;
        }

        public List<objectModel> indexObjects(int questionid)
        {
            var query = from o in db.OBJECTs
                        where o.QUESTIONID == questionid
                        orderby o.OBJID ascending
                        select new objectModel(o.OBJID, o.OBJTYPE, o.ATTRIBUTE, o.QUESTIONID);
            return query.ToList();
        }

        public int getMaxID()
        {
            int query = (from o in db.OBJECTs
                         select o.OBJID).Max();

            return query;
        }

        public void createObject()
        {
            db.OBJECTs.InsertOnSubmit(ob);
            db.SubmitChanges();
        }

        public void deleteObject()
        {
            db.OBJECTs.Attach(ob);
            db.OBJECTs.DeleteOnSubmit(ob);
            db.SubmitChanges();
        }
    }
}
