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
using DBPOLLDemo.Models;
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

        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        public objectModel(int obid, int obtype, String attribute, int questionid)
        {
            ob.OBJ_ID = this.obid = obid;
            ob.OBJ_TYPE = this.obtype = obtype;
            ob.ATTRIBUTE = this.attribute = attribute;
            ob.QUESTION_ID = this.questionid = questionid;
        }

        /// <summary>
        /// Empty Constructor for object model. Used to call methods.
        /// </summary>
        public objectModel(){}

        public objectModel(int obid)
        {
            ob.OBJ_ID = this.obid = obid;
        }

        public objectModel(int obtype, String attribute, int questionid)
        {
            ob.OBJ_TYPE = this.obtype = obtype;
            ob.ATTRIBUTE = this.attribute = attribute;
            ob.QUESTION_ID = this.questionid = questionid;
        }

        public List<objectModel> indexObjects(int questionid)
        {
            var query = from o in dbpollContext.OBJECTS
                        where o.QUESTION_ID == questionid
                        orderby o.OBJ_ID ascending
                        select new objectModel
                        {
                            obid = o.OBJ_ID,
                            obtype = o.OBJ_TYPE,
                            attribute = o.ATTRIBUTE,
                            questionid = o.QUESTION_ID
                        };

            return query.ToList();
        }

        public int getMaxID()
        {
            int query = (from o 
                         in dbpollContext.OBJECTS
                         select o.OBJ_ID).Max();

            return query;
        }

        public void createObject()
        {
            //dbpollContext.OBJECTS.InsertOnSubmit(ob);
            //dbpollContext.SubmitChanges();
        }

        public void deleteObject()
        {
            //dbpollContext.OBJECTS.Attach(ob);
           // dbpollContext.OBJECTS.DeleteOnSubmit(ob);
           //dbpollContext.SubmitChanges();
        }
    }
}
