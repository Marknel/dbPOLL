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
    public class pollObjectModel : System.Web.UI.Page
    {

        private POLL_OBJECTS_DFLT ob = new POLL_OBJECTS_DFLT();
        public int obid;
        public int pollid;
        public String attribute;

        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        public pollObjectModel(int obid, String attribute)
        {
            ob.O_ID = this.obid = obid;
            ob.ATTRIBUTE = this.attribute = attribute;
        }

        /// <summary>
        /// Empty Constructor for object model. Used to call methods.
        /// </summary>
        public pollObjectModel() { }

        public pollObjectModel(int obid)
        {
            ob.O_ID = this.obid = obid;
        }

        public pollObjectModel(String attribute)
        {
            ob.ATTRIBUTE = this.attribute = attribute;
        }

        public List<pollObjectModel> indexObjects(int pollid)
        {
            var query = from o in dbpollContext.POLL_OBJECTS_DFLT
                        where o.P_ID == pollid
                        orderby o.O_ID ascending
                        select new pollObjectModel
                        {
                            obid = o.O_ID,
                            attribute = o.ATTRIBUTE,
                            pollid = o.P_ID
                        };

            return query.ToList();
        }
        /**
        public questionObjectModel getObject(int id)
        {
            var query = from o in dbpollContext.QUESTION_OBJECTS
                        where o.O_ID == id
                        select new questionObjectModel
                        {
                            obid = o.OBJ_ID,
                            attribute = o.ATTRIBUTE
                        };

            return query.First();
        }
        **/

        public int getMaxID()
        {
            int query = (from o
                         in dbpollContext.POLL_OBJECTS_DFLT
                         select o.O_ID).Max();
            return query;
        }

        public void createObject(int obtype, String attribute, int pollid)
        {
            try
            {
                POLL_OBJECTS_DFLT po = new POLL_OBJECTS_DFLT();

                po.O_ID = obtype;
                po.ATTRIBUTE = attribute;
                po.P_ID = pollid;

                dbpollContext.AddToPOLL_OBJECTS_DFLT(po);

                int status = dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        /**
        public void updateObject(int obid, int obtype, String attribute)
        {
            try
            {
                var oList =
                from o in dbpollContext.QUESTION_OBJECTS
                where o.O_ID == obid
                select o;

                QUESTION_OBJECTS ob = oList.First<QUESTION_OBJECTS>();
                

                ob.O_ID = getMaxID() + 1;
                ob.ATTRIBUTE = attribute;

                dbpollContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
        **/
        public void deleteObject()
        {
            try
            {
                var oList =
                from o in dbpollContext.POLL_OBJECTS_DFLT
                where o.O_ID == obid
                select o;

                POLL_OBJECTS_DFLT ob = oList.First<POLL_OBJECTS_DFLT>();

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
