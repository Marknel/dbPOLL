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

        public objectModel getObject(int id)
        {
            var query = from o in dbpollContext.OBJECTS
                        where o.OBJ_ID== id
                        select new objectModel
                        {
                            obid = o.OBJ_ID,
                            obtype = o.OBJ_ID,
                            attribute = o.ATTRIBUTE
                        };

            return query.First();
        }

        public int getMaxID()
        {
            int query = (from o 
                         in dbpollContext.OBJECTS
                         select o.OBJ_ID).Max();

            return query;
        }

        public void createObject(int obtype, String attribute, int questionid)
        {
            try
            {

                OBJECT o = new OBJECT();

                o.OBJ_ID = getMaxID() + 1;
                o.OBJ_TYPE = obtype;
                o.ATTRIBUTE = attribute;

                dbpollContext.AddToOBJECTS(o);

                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void updateObject(int obid, int obtype, String attribute)
        {
            try
            {
                var oList =
                from o in dbpollContext.OBJECTS
                where o.OBJ_ID == obid
                select o;

                OBJECT ob = oList.First<OBJECT>();

                ob.OBJ_ID = getMaxID() + 1;
                ob.OBJ_TYPE = obtype;
                ob.ATTRIBUTE = attribute;

                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
            
        }

        public void deleteObject()
        {
            try
            {
                var oList =
                from o in dbpollContext.OBJECTS
                where o.OBJ_ID == obid
                select o;

                OBJECT ob = oList.First<OBJECT>();

                dbpollContext.DeleteObject(ob);

                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
