using System;
using System.Collections.Generic;
using System.Linq;

namespace DBPOLLDemo.Models
{
    public class questionObjectModel : System.Web.UI.Page
    {

        private QUESTION_OBJECTS ob = new QUESTION_OBJECTS();
        public int obid;
        public int questionid;
        public String attribute;

        private DBPOLLEntities dbpollContext = new DBPOLLEntities(); // ADO.NET data Context.

        public questionObjectModel(int obid, String attribute)
        {
            ob.O_ID = this.obid = obid;
            ob.ATTRIBUTE = this.attribute = attribute;
        }

        /// <summary>
        /// Empty Constructor for object model. Used to call methods.
        /// </summary>
        public questionObjectModel() { }

        public questionObjectModel(int obid)
        {
            ob.O_ID = this.obid = obid;
        }

        public questionObjectModel(String attribute)
        {
            ob.ATTRIBUTE = this.attribute = attribute;
        }

        public List<questionObjectModel> indexObjects(int questionid)
        {
            var query = from o in dbpollContext.QUESTION_OBJECTS
                        where o.Q_ID == questionid
                        orderby o.O_ID ascending
                        select new questionObjectModel
                        {
                            obid = o.O_ID,
                            attribute = o.ATTRIBUTE,
                            questionid = o.Q_ID
                        };

            return query.ToList();
        }

        public questionObjectModel getObject(int id, int questionid)
        {
            var query = from o in dbpollContext.QUESTION_OBJECTS
                        where o.O_ID == id && o.Q_ID == questionid
                        select new questionObjectModel
                        {
                            obid = o.O_ID,
                            attribute = o.ATTRIBUTE
                        };
            if (query.Count() == 0) {
                return new questionObjectModel(-1);
            }
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
                QUESTION_OBJECTS qo = new QUESTION_OBJECTS();

                qo.O_ID = obtype;
                qo.ATTRIBUTE = attribute;
                qo.Q_ID = questionid;

                dbpollContext.AddToQUESTION_OBJECTS(qo);

                int status = dbpollContext.SaveChanges();
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

        public void deleteObject()
        {
            try
            {
                var oList =
                from o in dbpollContext.QUESTION_OBJECTS
                where o.O_ID == obid
                select o;

                QUESTION_OBJECTS ob = oList.First<QUESTION_OBJECTS>();

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
